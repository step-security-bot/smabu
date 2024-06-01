using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using Newtonsoft.Json;
using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.UseCases.Seed
{
    public class ImportLegacyData(IAggregateStore aggregateStore)
    {
        private const string TaxDetails = "Umsatzsteuer wird aufgrund der Befreiung für Kleinunternehmer gemäß § 19 Abs. 1 UStG nicht gesondert ausgewiesen.";
        private readonly IAggregateStore aggregateStore = aggregateStore;

        public async Task StartAsync()
        {
            var importDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Smabu", "Import");
            var jsonFile = Path.Combine(importDir, "Backup.json");
            if (File.Exists(jsonFile))
            {
                var jsonContent = File.ReadAllText(jsonFile);
                var importObject = JsonConvert.DeserializeObject<BackupObject>(jsonContent);
                if (importObject?.Kunden != null)
                {
                    try
                    {
                        foreach (var importKunde in importObject.Kunden)
                        {
                            var customerId = new CustomerId(Guid.NewGuid());

                            var customer = Customer.Create(customerId, CustomerNumber.CreateLegacy(importKunde.Id), importKunde.Name1, importKunde.Branche);
                            customer.Update(customer.Name, customer.IndustryBranch,
                                new Address(importKunde.Name1, (importKunde.Vorname + " " + importKunde.Nachname).Trim(),
                                importKunde.Strasse, importKunde.Hausnummer, importKunde.Postleitzahl, importKunde.Ort, importKunde.Land),
                                null);
                            await aggregateStore.CreateAsync(customer);

                            var importRechnungen = importObject.Rechnungen.Where(x => x.KundeId == importKunde.Id).ToList();
                            foreach (var importRechnung in importRechnungen)
                            {
                                var invoiceNumber = InvoiceNumber.CreateLegacy((long)importRechnung.Rechnungsnummer);
                                var invoiceId = new InvoiceId(Guid.NewGuid());
                                var invoice = Invoice.Create(invoiceId, customerId, importRechnung.Jahr,
                                    customer.MainAddress,
                                    DatePeriod.CreateFrom(importRechnung.LeistungsdatumVon ?? importRechnung.LeistungsdatumBis.GetValueOrDefault(), importRechnung.LeistungsdatumBis.GetValueOrDefault()),
                                    Currency.GetEuro(), 0, TaxDetails);

                                foreach (var importRechnungPosition in importRechnung.Positionen)
                                {
                                    var invoiceItem = invoice.AddItem(new InvoiceItemId(Guid.NewGuid()),
                                        string.IsNullOrWhiteSpace(importRechnungPosition.Bemerkung) ? "Keine weiteren Informationen" : importRechnungPosition.Bemerkung,
                                        new Quantity(importRechnungPosition.Menge, importRechnungPosition.ProduktEinheit),
                                        importRechnungPosition.Preis);
                                }

                                invoice.Release(invoice.Number, importRechnung.Rechnungsdatum);
                                await aggregateStore.CreateAsync(invoice);
                            }

                            var importAngebote = importObject.Angebote.Where(x => x.KundeId == importKunde.Id).ToList();
                            foreach (var importAngebot in importAngebote)
                            {
                                var offerNumber = OfferNumber.CreateLegacy(importAngebot.Id);
                                var offerId = new OfferId(Guid.NewGuid());
                                var offer = Offer.Create(offerId, customerId, offerNumber, customer.MainAddress, Currency.GetEuro(), 0, TaxDetails);
                                offer.Update(0, TaxDetails, DateOnly.FromDateTime(importAngebot.Angebotsdatum), DateOnly.FromDateTime(importAngebot.Angebotsdatum.AddDays(importAngebot.GueltigkeitTage)));

                                foreach (var importAngebotPosition in importAngebot.Positionen)
                                {
                                    var offerItem = offer.AddItem(new OfferItemId(Guid.NewGuid()), importAngebotPosition.Bemerkung,
                                        new Quantity(importAngebotPosition.Menge, importAngebotPosition.ProduktEinheit), importAngebotPosition.Preis);
                                }

                                await aggregateStore.CreateAsync(offer);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }

        public class BackupObject
        {
            public required List<Kunde> Kunden { get; set; }
            public required List<Rechnung> Rechnungen { get; set; }
            public required List<Angebot> Angebote { get; set; }

            public class Kunde
            {
                public int Id { get; set; }
                public string Name1 { get; set; } = default!;
                public string Name2 { get; set; } = default!;
                public string Branche { get; set; } = default!;
                public string Anrede { get; set; } = default!;
                public string Vorname { get; set; } = default!;
                public string Nachname { get; set; } = default!;
                public string Email { get; set; } = default!;
                public string Mobil { get; set; } = default!;
                public string Abteilung { get; set; } = default!;
                public string Strasse { get; set; } = default!;
                public string Postleitzahl { get; set; } = default!;
                public string Ort { get; set; } = default!;
                public string Land { get; set; } = default!;
                public string AdressZusatz { get; set; } = default!;
                public string Hausnummer { get; set; } = default!;
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
                public string Zahlungsbedingung { get; set; } = default!;
                public required List<Rechnungsposition> Positionen { get; set; }


                public class Rechnungsposition
                {
                    public int Id { get; set; }
                    public int Reihenfolge { get; set; }
                    public decimal Menge { get; set; }
                    public string Bemerkung { get; set; } = default!;
                    public DateTime CreationDate { get; set; }
                    public decimal Preis { get; set; }
                    public string ProduktCode { get; set; } = default!;
                    public string ProduktEinheit { get; set; } = default!;
                    public string ProduktKategorie { get; set; } = default!;
                    public string ProduktName { get; set; } = default!;
                    public decimal Summe { get; set; }

                }
            }
            public class Angebot
            {
                public required List<Angebotsposition> Positionen { get; set; }
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
                    public string Bemerkung { get; set; } = default!;
                    public DateTime CreationDate { get; set; }
                    public decimal Preis { get; set; }
                    public string ProduktCode { get; set; } = default!;
                    public string ProduktEinheit { get; set; } = default!;
                    public string ProduktKategorie { get; set; } = default!;
                    public string ProduktName { get; set; } = default!;
                    public decimal Summe { get; set; }

                }
            }
        }
    }
}
