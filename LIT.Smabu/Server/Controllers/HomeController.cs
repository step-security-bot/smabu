using LIT.Smabu.Service.Business;
using LIT.Smabu.Shared.Domain.Common;
using LIT.Smabu.Shared.Domain.InvoiceAggregate;
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
        private readonly CustomerService customerService;
        private readonly InvoiceService invoiceService;

        public HomeController(ILogger<HomeController> logger, CustomerService customerService, InvoiceService invoiceService)
        {
            _logger = logger;
            this.customerService = customerService;
            this.invoiceService = invoiceService;
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
                    try
                    {
                        foreach (var importKunde in importObject.Kunden)
                        {
                            var customer = await customerService.CreateAsync(importKunde.Name1);
                            await customerService.EditAsync(customer.Id, customer.Name, importKunde.Branche);
                            await customerService.EditMainAddressAsync(customer.Id, importKunde.Name1, (importKunde.Vorname + " " + importKunde.Nachname).Trim(),
                                importKunde.Strasse, importKunde.Hausnummer, importKunde.AdressZusatz, importKunde.Postleitzahl, importKunde.Ort, importKunde.Land);

                            var importRechnungen = importObject.Rechnungen.Where(x => x.KundeId == importKunde.Id).ToList();
                            foreach (var importRechnung in importRechnungen)
                            {
                                var invoiceNumber = InvoiceNumber.CreateLegacy((long)importRechnung.Rechnungsnummer);
                                var invoice = await invoiceService.CreateAsync(customer.Id,
                                    Period.CreateFrom(importRechnung.LeistungsdatumVon ?? importRechnung.LeistungsdatumBis.GetValueOrDefault(), importRechnung.LeistungsdatumBis.GetValueOrDefault()),
                                    0, "", null, null, invoiceNumber);

                                foreach (var importRechnungPosition in importRechnung.Positionen)
                                {
                                    await invoiceService.AddInvoiceLineAsync(invoice.Id, importRechnungPosition.Bemerkung,
                                        new Quantity(importRechnungPosition.Menge, importRechnungPosition.ProduktEinheit), importRechnungPosition.Preis);
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