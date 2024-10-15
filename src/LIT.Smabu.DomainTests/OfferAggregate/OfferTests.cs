using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.OfferAggregate;

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
            var offer = Offer.Create(id, customerId, number, customerAddress, currency, taxRate);

            // Assert
            Assert.AreEqual(id, offer.Id);
            Assert.AreEqual(customerId, offer.CustomerId);
            Assert.AreEqual(number, offer.Number);
            Assert.AreEqual(customerAddress, offer.CustomerAddress);
            Assert.AreEqual(currency, offer.Currency);
            Assert.AreEqual(taxRate, offer.TaxRate);
            Assert.AreEqual(0, offer.Items.Count);
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
            var offer = Offer.Create(id, customerId, number, customerAddress, currency, taxRate);

            var newTaxRate = new TaxRate("New Tax Rate", 0.2m, "New Tax Rate Details");
            var newOfferDate = new DateOnly(2022, 1, 1);
            var newExpiresOn = new DateOnly(2022, 1, 15);

            // Act
            var result = offer.Update(newTaxRate, newOfferDate, newExpiresOn);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(newTaxRate, offer.TaxRate);
            Assert.AreEqual(newOfferDate, offer.OfferDate);
            Assert.AreEqual(newExpiresOn, offer.ExpiresOn);
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
            var offer = Offer.Create(id, customerId, number, customerAddress, currency, taxRate);

            var itemId = new OfferItemId(Guid.NewGuid());
            var details = "Item Details";
            var quantity = new Quantity(1, "Unit");
            var unitPrice = 10m;

            // Act
            var result = offer.AddItem(itemId, details, quantity, unitPrice);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, offer.Items.Count);
            Assert.AreEqual(itemId, offer.Items[0].Id);
            Assert.AreEqual(details, offer.Items[0].Details);
            Assert.AreEqual(quantity, offer.Items[0].Quantity);
            Assert.AreEqual(unitPrice, offer.Items[0].UnitPrice);
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
            var offer = Offer.Create(id, customerId, number, customerAddress, currency, taxRate);

            var itemId = new OfferItemId(Guid.NewGuid());
            var details = "Item Details";
            var quantity = new Quantity(1, "Unit");
            var unitPrice = 10m;
            offer.AddItem(itemId, details, quantity, unitPrice);

            var newDetails = "New Item Details";
            var newQuantity = new Quantity(2, "Unit");
            var newUnitPrice = 20m;

            // Act
            var result = offer.UpdateItem(itemId, newDetails, newQuantity, newUnitPrice);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(newDetails, offer.Items[0].Details);
            Assert.AreEqual(newQuantity, offer.Items[0].Quantity);
            Assert.AreEqual(newUnitPrice, offer.Items[0].UnitPrice);
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
            var offer = Offer.Create(id, customerId, number, customerAddress, currency, taxRate);

            var itemId = new OfferItemId(Guid.NewGuid());
            var details = "Item Details";
            var quantity = new Quantity(1, "Unit");
            var unitPrice = 10m;
            offer.AddItem(itemId, details, quantity, unitPrice);

            // Act
            var result = offer.RemoveItem(itemId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(0, offer.Items.Count);
        }
    }
}