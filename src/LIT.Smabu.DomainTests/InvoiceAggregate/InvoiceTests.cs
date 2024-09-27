using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.InvoiceAggregate;

namespace LIT.Smabu.DomainTests.InvoiceAggregate
{
    [TestClass]
    public class InvoiceTests
    {
        private readonly InvoiceId _invoiceId = new(Guid.NewGuid());
        private readonly CustomerId _customerId = new(Guid.NewGuid());
        private readonly Address _address = new("Name 1", "Name 2", "Street", "House number", "12345", "City", "Country");
        private readonly DatePeriod _datePeriod = DatePeriod.CreateFrom(DateTime.Now, DateTime.Now.AddDays(1));
        private readonly Currency _currency = Currency.GetEuro();

        [TestMethod]
        public void Create_ShouldReturnInvoice()
        {
            // Act
            var invoice = Invoice.Create(_invoiceId, _customerId, 2023, _address, _datePeriod, _currency, 19, "Tax Details");

            // Assert
            Assert.IsNotNull(invoice);
            Assert.AreEqual(_invoiceId, invoice.Id);
            Assert.AreEqual(_customerId, invoice.CustomerId);
            Assert.AreEqual(2023, invoice.FiscalYear);
            Assert.AreEqual(_address, invoice.CustomerAddress);
            Assert.AreEqual(_datePeriod, invoice.PerformancePeriod);
            Assert.AreEqual(_currency, invoice.Currency);
            Assert.AreEqual(19, invoice.Tax);
            Assert.AreEqual("Tax Details", invoice.TaxDetails);
            Assert.IsFalse(invoice.IsReleased);
            Assert.IsNull(invoice.ReleasedOn);
            Assert.IsNull(invoice.InvoiceDate);
            Assert.AreEqual(0, invoice.Items.Count);
        }

        [TestMethod]
        public void AddItem_ShouldAddItemToInvoice()
        {
            // Arrange
            var invoice = Invoice.Create(_invoiceId, _customerId, 2023, _address, _datePeriod, _currency, 19, "Tax Details");
            var itemId = new InvoiceItemId(Guid.NewGuid());
            var details = "Item Details";
            var quantity = new Quantity(1, "Stk");
            var unitPrice = 100m;

            // Act
            var result = invoice.AddItem(itemId, details, quantity, unitPrice);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, invoice.Items.Count);
            Assert.AreEqual(details, invoice.Items.First().Details);
        }

        [TestMethod]
        public void Update_ShouldUpdateInvoice()
        {
            // Arrange
            var invoice = Invoice.Create(_invoiceId, _customerId, 2023, _address, _datePeriod, _currency, 19, "Tax Details");
            var newDatePeriod = DatePeriod.CreateFrom(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            var newTax = 20m;
            var newTaxDetails = "New Tax Details";
            var newInvoiceDate = DateOnly.FromDateTime(DateTime.Now);

            // Act
            var result = invoice.Update(newDatePeriod, newTax, newTaxDetails, newInvoiceDate);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(newDatePeriod, invoice.PerformancePeriod);
            Assert.AreEqual(newTax, invoice.Tax);
            Assert.AreEqual(newTaxDetails, invoice.TaxDetails);
            Assert.AreEqual(newInvoiceDate, invoice.InvoiceDate);
        }

        [TestMethod]
        public void Release_ShouldReleaseInvoice()
        {
            // Arrange
            var invoice = Invoice.Create(_invoiceId, _customerId, 2023, _address, _datePeriod, _currency, 19, "Tax Details");
            var number = InvoiceNumber.CreateFirst(2023);
            var releasedOn = DateTime.Now;
            invoice.AddItem(new(Guid.NewGuid()), "Details", new(1, "STK"), 1);

            // Act
            var result = invoice.Release(number, releasedOn);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(invoice.IsReleased);
            Assert.AreEqual(number, invoice.Number);
            Assert.AreEqual(releasedOn, invoice.ReleasedOn);
        }

        [TestMethod]
        public void WithdrawRelease_ShouldWithdrawRelease()
        {
            // Arrange
            var invoice = Invoice.Create(_invoiceId, _customerId, 2023, _address, _datePeriod, _currency, 19, "Tax Details");
            var number = InvoiceNumber.CreateFirst(2023);
            invoice.AddItem(new(Guid.NewGuid()), "Details", new(1, "STK"), 1);
            invoice.Release(number, DateTime.Now);

            // Act
            var result = invoice.WithdrawRelease();

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(invoice.IsReleased);
        }
    }
}