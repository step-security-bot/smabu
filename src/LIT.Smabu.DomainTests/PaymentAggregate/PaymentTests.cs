using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.PaymentAggregate;

namespace LIT.Smabu.DomainTests.PaymentAggregate
{
    [TestClass]
    public class PaymentTests
    {
        [TestMethod]
        public void CreateIncoming_ShouldInitializeCorrectly()
        {
            // Arrange
            var id = new PaymentId(Guid.NewGuid());
            var details = "Test Details";
            var payer = "Test Payer";
            var payee = "Test Payee";
            var customerId = new CustomerId(Guid.NewGuid());
            var invoiceId = new InvoiceId(Guid.NewGuid());
            var documentNr = "12345";
            var documentDate = DateTime.Now;
            var accountingDate = DateTime.Now;
            var amountDue = 100m;

            // Act
            var payment = Payment.CreateIncoming(id, details, payer, payee, customerId, invoiceId, documentNr, documentDate, accountingDate, amountDue);

            // Assert
            Assert.AreEqual(id, payment.Id);
            Assert.AreEqual(PaymentDirection.Incoming, payment.Direction);
            Assert.AreEqual(details, payment.Details);
            Assert.AreEqual(payer, payment.Payer);
            Assert.AreEqual(payee, payment.Payee);
            Assert.AreEqual(customerId, payment.CustomerId);
            Assert.AreEqual(invoiceId, payment.InvoiceId);
            Assert.AreEqual(documentNr, payment.ReferenceNr);
            Assert.AreEqual(documentDate, payment.ReferenceDate);
            Assert.AreEqual(accountingDate, payment.AccountingDate);
            Assert.AreEqual(amountDue, payment.AmountDue);
            Assert.AreEqual(0, payment.AmountPaid);
            Assert.AreEqual(Currency.EUR, payment.Currency);
            Assert.AreEqual(PaymentStatus.Pending, payment.Status);
        }

        [TestMethod]
        public void CreateOutgoing_ShouldInitializeCorrectly()
        {
            // Arrange
            var id = new PaymentId(Guid.NewGuid());
            var details = "Test Details";
            var payer = "Test Payer";
            var payee = "Test Payee";
            var documentNr = "12345";
            var documentDate = DateTime.Now;
            var accountingDate = DateTime.Now;
            var amountDue = 100m;

            // Act
            var payment = Payment.CreateOutgoing(id, details, payer, payee, documentNr, documentDate, accountingDate, amountDue);

            // Assert
            Assert.AreEqual(id, payment.Id);
            Assert.AreEqual(PaymentDirection.Outgoing, payment.Direction);
            Assert.AreEqual(details, payment.Details);
            Assert.AreEqual(payer, payment.Payer);
            Assert.AreEqual(payee, payment.Payee);
            Assert.AreEqual(documentNr, payment.ReferenceNr);
            Assert.AreEqual(documentDate, payment.ReferenceDate);
            Assert.AreEqual(accountingDate, payment.AccountingDate);
            Assert.AreEqual(amountDue, payment.AmountDue);
            Assert.AreEqual(0, payment.AmountPaid);
            Assert.AreEqual(Currency.EUR, payment.Currency);
            Assert.AreEqual(PaymentStatus.Pending, payment.Status);
        }

        [TestMethod]
        public void Update_ShouldUpdatePropertiesCorrectly()
        {
            // Arrange
            var id = new PaymentId(Guid.NewGuid());
            var customerId = new CustomerId(Guid.NewGuid());
            var invoiceId = new InvoiceId(Guid.NewGuid());
            var payment = Payment.CreateIncoming(id, "Initial Details", "Initial Payer", "Initial Payee", customerId, invoiceId, "12345", DateTime.Now, DateTime.Now, 100m);
            var newDetails = "Updated Details";
            var newPayer = "Updated Payer";
            var newPayee = "Updated Payee";
            var newDocumentNr = "67890";
            var newDocumentDate = DateTime.Now.AddDays(1);
            var newAmountDue = 200m;
            var newStatus = PaymentStatus.Partial;

            // Act
            var result = payment.Update(newDetails, newPayer, newPayee, newDocumentNr, newDocumentDate, newAmountDue, newStatus);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(newDetails, payment.Details);
            Assert.AreEqual(newPayer, payment.Payer);
            Assert.AreEqual(newPayee, payment.Payee);
            Assert.AreEqual(newDocumentNr, payment.ReferenceNr);
            Assert.AreEqual(newDocumentDate, payment.ReferenceDate);
            Assert.AreEqual(newAmountDue, payment.AmountDue);
            Assert.AreEqual(newStatus, payment.Status);
        }

        [TestMethod]
        public void Complete_ShouldSetStatusToPaid()
        {
            // Arrange
            var id = new PaymentId(Guid.NewGuid());
            var customerId = new CustomerId(Guid.NewGuid());
            var invoiceId = new InvoiceId(Guid.NewGuid());
            var payment = Payment.CreateIncoming(id, "Details", "Payer", "Payee", customerId, invoiceId, "12345", DateTime.Now, DateTime.Now, 100m);
            var amountPaid = 100m;
            var paidAt = DateTime.Now;

            // Act
            var result = payment.Complete(amountPaid, paidAt);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(amountPaid, payment.AmountPaid);
            Assert.AreEqual(paidAt, payment.PaidAt);
            Assert.AreEqual(PaymentStatus.Paid, payment.Status);
        }
    }
}
