using LIT.Smabu.Business.Service.Customers.Commands;
using LIT.Smabu.Business.Service.Dashboards;
using LIT.Smabu.Business.Service.Invoices.Commands;
using LIT.Smabu.Business.Service.Offers.Commands;
using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Domain.Shared.Offers;
using LIT.Smabu.Shared.Dashboards;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace LIT.Smabu.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISender sender;

        public HomeController(ILogger<HomeController> logger, ISender sender)
        {
            _logger = logger;
            this.sender = sender;
        }

        [HttpGet("dashboard")]
        public async Task<HomeDashboardDTO> GetDashboard()
        {
            return await this.sender.Send(new GetHomeDashboardQuery());
        }

        #region Import

        [HttpPost("import")]
        public async Task PostImport()
        {
            var importDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Smabu", "Import");
            var jsonFile = Path.Combine(importDir, "Backup.json");
            if (System.IO.File.Exists(jsonFile))
            {
                var jsonContent = System.IO.File.ReadAllText(jsonFile);
                var importObject = Newtonsoft.Json.JsonConvert.DeserializeObject<BackupObject>(jsonContent);
                if (importObject?.Kunden != null)
                {
                    long angebotNr = 0;
                    try
                    {
                        foreach (var importKunde in importObject.Kunden)
                        {
                            var customerId = await this.sender.Send(new CreateCustomerCommand()
                            {
                                Id = new CustomerId(Guid.NewGuid()),
                                Name = importKunde.Name1
                            });
                            var customer = await this.sender.Send(new EditCustomerCommand()
                            {
                                Id = customerId,
                                Name = importKunde.Name1,
                                IndustryBranch = importKunde.Branche,
                                MainAddress = new Address(importKunde.Name1, (importKunde.Vorname + " " + importKunde.Nachname).Trim(),
                                importKunde.Strasse, importKunde.Hausnummer, importKunde.Postleitzahl, importKunde.Ort, importKunde.Land)
                            });

                            var importRechnungen = importObject.Rechnungen.Where(x => x.KundeId == importKunde.Id).ToList();
                            foreach (var importRechnung in importRechnungen)
                            {
                                var invoiceNumber = InvoiceNumber.CreateLegacy((long)importRechnung.Rechnungsnummer);
                                var invoice = await this.sender.Send(new CreateInvoiceCommand()
                                {
                                    Id = new InvoiceId(Guid.NewGuid()),
                                    FiscalYear = importRechnung.Jahr,
                                    PerformancePeriod = DatePeriod.CreateFrom(importRechnung.LeistungsdatumVon ?? importRechnung.LeistungsdatumBis.GetValueOrDefault(), importRechnung.LeistungsdatumBis.GetValueOrDefault()),
                                    Tax = 0,
                                    TaxDetails = "",
                                    Currency = Currency.GetEuro(),
                                    CustomerId = customerId
                                });

                                foreach (var importRechnungPosition in importRechnung.Positionen)
                                {
                                    await this.sender.Send(new AddInvoiceItemCommand()
                                    {
                                        Id = new InvoiceItemId(Guid.NewGuid()),
                                        InvoiceId = invoice.Id,
                                        Details = importRechnungPosition.Bemerkung,
                                        Quantity = new Quantity(importRechnungPosition.Menge, importRechnungPosition.ProduktEinheit),
                                        UnitPrice = importRechnungPosition.Preis
                                    });
                                }

                                await this.sender.Send(new ReleaseInvoiceCommand { Id = invoice.Id, Number = invoiceNumber, ReleasedOn = importRechnung.Rechnungsdatum });
                            }

                            var importAngebote = importObject.Angebote.Where(x => x.KundeId == importKunde.Id).ToList();
                            foreach (var importAngebot in importAngebote)
                            {
                                var offerNumber = new OfferNumber(++angebotNr);
                                var offer = await this.sender.Send(new CreateOfferCommand()
                                {
                                    Id = new OfferId(Guid.NewGuid()),
                                    Number = offerNumber,
                                    Tax = 0,
                                    TaxDetails = "",
                                    Currency = Currency.GetEuro(),
                                    CustomerId = customerId
                                }); 
                                offer = await this.sender.Send(new EditOfferCommand()
                                {
                                    Id = offer.Id,
                                    Tax = 0,
                                    TaxDetails = "",
                                    OfferDate = DateOnly.FromDateTime(importAngebot.Angebotsdatum),
                                    ExpiresOn = DateOnly.FromDateTime(importAngebot.Angebotsdatum.AddDays(importAngebot.GueltigkeitTage)),
                                });

                                foreach (var importAngebotPosition in importAngebot.Positionen)
                                {
                                    await this.sender.Send(new AddOfferItemCommand()
                                    {
                                        Id = new OfferItemId(Guid.NewGuid()),
                                        OfferId = offer.Id,
                                        Details = importAngebotPosition.Bemerkung,
                                        Quantity = new Quantity(importAngebotPosition.Menge, importAngebotPosition.ProduktEinheit),
                                        UnitPrice = importAngebotPosition.Preis
                                    });
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }

        public class BackupObject
        {
            public List<Kunde> Kunden { get; set; }
            public List<Rechnung> Rechnungen { get; set; }
            public List<Angebot> Angebote { get; set; }

            public class Kunde
            {
                public int Id { get; set; }
                public string Name1 { get; set; }
                public string Name2 { get; set; }
                public string Branche { get; set; }
                public string Anrede { get; set; }
                public string Vorname { get; set; }
                public string Nachname { get; set; }
                public string Email { get; set; }
                public string Mobil { get; set; }
                public string Abteilung { get; set; }
                public string Strasse { get; set; }
                public string Postleitzahl { get; set; }
                public string Ort { get; set; }
                public string Land { get; set; }
                public string AdressZusatz { get; set; }
                public string Hausnummer { get; set; }
            }

            public class Rechnung
            {
                public int Id { get; set; }
                public int KundeId { get; set; }
                public int Jahr { get; set; }
                public decimal Rechnungsnummer { get; set; }
                public bool IsBeglichen { get; set; }
                public DateTime Rechnungsdatum { get; set; }
                public DateTime? LeistungsdatumVon { get; set; }
                public DateTime? LeistungsdatumBis { get; set; }
                public decimal Summe { get; set; }
                public string Zahlungsbedingung { get; set; }
                public List<Rechnungsposition> Positionen { get; set; }


                public class Rechnungsposition
                {
                    public int Id { get; set; }
                    public int Reihenfolge { get; set; }
                    public decimal Menge { get; set; }
                    public string Bemerkung { get; set; }
                    public DateTime CreationDate { get; set; }
                    public decimal Preis { get; set; }
                    public string ProduktCode { get; set; }
                    public string ProduktEinheit { get; set; }
                    public string ProduktKategorie { get; set; }
                    public string ProduktName { get; set; }
                    public decimal Summe { get; set; }

                }
            }
            public class Angebot
            {
                public List<Angebotsposition> Positionen { get; set; }
                public int Id { get; set; }
                public int KundeId { get; set; }
                public DateTime Angebotsdatum { get; set; }
                public int GueltigkeitTage { get; set; }
                public DateTime CreationDate { get; set; }

                public class Angebotsposition
                {
                    public int Id { get; set; }
                    public int Reihenfolge { get; set; }
                    public decimal Menge { get; set; }
                    public string Bemerkung { get; set; }
                    public DateTime CreationDate { get; set; }
                    public decimal Preis { get; set; }
                    public string ProduktCode { get; set; }
                    public string ProduktEinheit { get; set; }
                    public string ProduktKategorie { get; set; }
                    public string ProduktName { get; set; }
                    public decimal Summe { get; set; }

                }
            }
        }


        #endregion
    }
}