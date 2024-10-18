using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LIT.Smabu.DomainTests.OfferAggregate
{
    [TestClass]
    public class OfferTests
    {
        [TestMethod]
        public void Create_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var id = new OfferId(Guid.NewGuid());
            var customerId = new CustomerId(Guid.NewGuid());
            var number = new OfferNumber(1);
            var customerAddress = new Address("Name1", "Name2", "Street", "123", "12345", "City", "Country");
            var currency = Currency.EUR;
            var taxRate = TaxRate.Default;

            // Act
            var testee = Offer.Create(id, customerId, number, customerAddress, currency, taxRate);

            // Assert
            Assert.AreEqual(id, testee.Id);
            Assert.AreEqual(customerId, testee.CustomerId);
            Assert.AreEqual(number, testee.Number);
            Assert.AreEqual(customerAddress, testee.CustomerAddress);
            Assert.AreEqual(currency, testee.Currency);
            Assert.AreEqual(taxRate, testee.TaxRate);
            Assert.AreEqual(0, testee.Items.Count);
        }

        [TestMethod]
        public void Update_ShouldUpdatePropertiesCorrectly()
        {
            // Arrange
            var id = new OfferId(Guid.NewGuid());
            var customerId = new CustomerId(Guid.NewGuid());
            var number = new OfferNumber(1);
            var customerAddress = new Address("Name1", "Name2", "Street", "123", "12345", "City", "Country");
            var currency = Currency.EUR;
            var taxRate = TaxRate.Default;
            var testee = Offer.Create(id, customerId, number, customerAddress, currency, taxRate);

            var newTaxRate = new TaxRate("New Tax Rate", 0.2m, "New Tax Rate Details");
            var newOfferDate = new DateOnly(2022, 1, 1);
            var newExpiresOn = new DateOnly(2022, 1, 15);

            // Act
            var result = testee.Update(newTaxRate, newOfferDate, newExpiresOn);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(newTaxRate, testee.TaxRate);
            Assert.AreEqual(newOfferDate, testee.OfferDate);
            Assert.AreEqual(newExpiresOn, testee.ExpiresOn);
        }

        [TestMethod]
        public void AddItem_ShouldAddItemToList()
        {
            // Arrange
            var id = new OfferId(Guid.NewGuid());
            var customerId = new CustomerId(Guid.NewGuid());
            var number = new OfferNumber(1);
            var customerAddress = new Address("Name1", "Name2", "Street", "123", "12345", "City", "Country");
            var currency = Currency.EUR;
            var taxRate = TaxRate.Default;
            var testee = Offer.Create(id, customerId, number, customerAddress, currency, taxRate);

            var itemId = new OfferItemId(Guid.NewGuid());
            var details = "Item Details";
            var quantity = new Quantity(1, "Unit");
            var unitPrice = 10m;

            // Act
            var result = testee.AddItem(itemId, details, quantity, unitPrice);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, testee.Items.Count);
            Assert.AreEqual(itemId, testee.Items[0].Id);
            Assert.AreEqual(details, testee.Items[0].Details);
            Assert.AreEqual(quantity, testee.Items[0].Quantity);
            Assert.AreEqual(unitPrice, testee.Items[0].UnitPrice);
        }

        [TestMethod]
        public void UpdateItem_ShouldUpdateItemProperties()
        {
            // Arrange
            var id = new OfferId(Guid.NewGuid());
            var customerId = new CustomerId(Guid.NewGuid());
            var number = new OfferNumber(1);
            var customerAddress = new Address("Name1", "Name2", "Street", "123", "12345", "City", "Country");
            var currency = Currency.EUR;
            var taxRate = TaxRate.Default;
            var testee = Offer.Create(id, customerId, number, customerAddress, currency, taxRate);

            var itemId = new OfferItemId(Guid.NewGuid());
            var details = "Item Details";
            var quantity = new Quantity(1, "Unit");
            var unitPrice = 10m;
            testee.AddItem(itemId, details, quantity, unitPrice);

            var newDetails = "New Item Details";
            var newQuantity = new Quantity(2, "Unit");
            var newUnitPrice = 20m;

            // Act
            var result = testee.UpdateItem(itemId, newDetails, newQuantity, newUnitPrice);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(newDetails, testee.Items[0].Details);
            Assert.AreEqual(newQuantity, testee.Items[0].Quantity);
            Assert.AreEqual(newUnitPrice, testee.Items[0].UnitPrice);
        }

        [TestMethod]
        public void RemoveItem_ShouldRemoveItemFromList()
        {
            // Arrange
            var id = new OfferId(Guid.NewGuid());
            var customerId = new CustomerId(Guid.NewGuid());
            var number = new OfferNumber(1);
            var customerAddress = new Address("Name1", "Name2", "Street", "123", "12345", "City", "Country");
            var currency = Currency.EUR;
            var taxRate = TaxRate.Default;
            var testee = Offer.Create(id, customerId, number, customerAddress, currency, taxRate);

            var itemId = new OfferItemId(Guid.NewGuid());
            var details = "Item Details";
            var quantity = new Quantity(1, "Unit");
            var unitPrice = 10m;
            testee.AddItem(itemId, details, quantity, unitPrice);

            // Act
            var result = testee.RemoveItem(itemId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(0, testee.Items.Count);
        }


        [TestMethod()]
        public void Delete_Offer_Succeeds()
        {
            // Arrange
            var id = new OfferId(Guid.NewGuid());
            var customerId = new CustomerId(Guid.NewGuid());
            var number = new OfferNumber(1);
            var customerAddress = new Address("Name1", "Name2", "Street", "123", "12345", "City", "Country");
            var currency = Currency.EUR;
            var taxRate = TaxRate.Default;
            var testee = Offer.Create(id, customerId, number, customerAddress, currency, taxRate);

            // Act
            var result = testee.Delete();

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }
    }
}