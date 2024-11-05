using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.InvoiceAggregate;

namespace LIT.Smabu.DomainTests.InvoiceAggregate
{
    [TestClass]
    public class InvoiceItemTests
    {
        [TestMethod]
        public void Edit_ShouldUpdateDetailsQuantityUnitPriceAndCatalogItemId()
        {
            // Arrange
            var id = new InvoiceItemId(Guid.NewGuid());
            var invoiceId = new InvoiceId(Guid.NewGuid());
            var position = 1;
            var details = "Sample details";
            var quantity = new Quantity(5, Unit.Item);
            var unitPrice = 10.5m;
            var catalogItemId = new CatalogItemId(Guid.NewGuid());
            var invoiceItem = new InvoiceItem(id, invoiceId, position, details, quantity, unitPrice, catalogItemId);

            var newDetails = "Updated details";
            var newQuantity = new Quantity(3, Unit.Item);
            var newUnitPrice = 15.75m;
            var newCatalogItemId = new CatalogItemId(Guid.NewGuid());

            // Act
            invoiceItem.Edit(newDetails, newQuantity, newUnitPrice, newCatalogItemId);

            // Assert
            Assert.AreEqual(newDetails, invoiceItem.Details);
            Assert.AreEqual(newQuantity, invoiceItem.Quantity);
            Assert.AreEqual(newUnitPrice, invoiceItem.UnitPrice);
            Assert.AreEqual(newCatalogItemId, invoiceItem.CatalogItemId);
        }

        [TestMethod]
        public void EditPosition_ShouldUpdatePosition()
        {
            // Arrange
            var id = new InvoiceItemId(Guid.NewGuid());
            var invoiceId = new InvoiceId(Guid.NewGuid());
            var position = 1;
            var details = "Sample details";
            var quantity = new Quantity(5, Unit.Item);
            var unitPrice = 10.5m;
            var catalogItemId = new CatalogItemId(Guid.NewGuid());
            var invoiceItem = new InvoiceItem(id, invoiceId, position, details, quantity, unitPrice, catalogItemId);

            var newPosition = 2;

            // Act
            invoiceItem.EditPosition(newPosition);

            // Assert
            Assert.AreEqual(newPosition, invoiceItem.Position);
        }
    }
}