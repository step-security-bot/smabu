using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.InvoiceAggregate;
using System.Collections;

namespace LIT.Smabu.DomainTests.CatalogAggregate
{
    [TestClass()]
    public class CatalogItemTests
    {
        [TestMethod]
        public void Create_ShouldCreateCatalogItemWithDefaultPrices()
        {
            // Arrange
            var id = new CatalogItemId(Guid.NewGuid());
            var number = new CatalogItemNumber(1);
            var catalogGroupId = new CatalogGroupId(Guid.NewGuid());
            var name = "Test Catalog Item";
            var description = "Test Description";
            var unit = Unit.Item;

            // Act
            var catalogItem = CatalogItem.Create(id, number, catalogGroupId, name, description, unit);

            // Assert
            Assert.IsNotNull(catalogItem);
            Assert.AreEqual(id, catalogItem.Id);
            Assert.AreEqual(number, catalogItem.Number);
            Assert.AreEqual(catalogGroupId, catalogItem.CatalogGroupId);
            Assert.IsTrue(catalogItem.IsActive);
            Assert.AreEqual(name, catalogItem.Name);
            Assert.AreEqual(description, catalogItem.Description);
            Assert.AreEqual(unit, catalogItem.Unit);
            Assert.IsNotNull(catalogItem.Prices);
            Assert.AreEqual(1, catalogItem.Prices.Count);
            Assert.IsNotNull(catalogItem.CustomerPrices);
            Assert.AreEqual(0, catalogItem.CustomerPrices.Count);
        }

        [TestMethod]
        public void Update_ShouldUpdateCatalogItemProperties()
        {
            // Arrange
            var id = new CatalogItemId(Guid.NewGuid());
            var number = new CatalogItemNumber(1);
            var catalogGroupId = new CatalogGroupId(Guid.NewGuid());
            var isActive = true;
            var name = "Test Catalog Item";
            var description = "Test Description";
            var unit = Unit.Item;
            var catalogItem = new CatalogItem(id, number, catalogGroupId, isActive, name, description, unit, null, null);

            // Act
            var result = catalogItem.Update(false, "Updated Catalog Item", "Updated Description", Unit.Hour);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(catalogItem.IsActive);
            Assert.AreEqual("Updated Catalog Item", catalogItem.Name);
            Assert.AreEqual("Updated Description", catalogItem.Description);
            Assert.AreEqual(Unit.Hour, catalogItem.Unit);
        }

        [TestMethod]
        public void Update_ShouldReturnFailureResult_WhenValidationFails()
        {
            // Arrange
            var id = new CatalogItemId(Guid.NewGuid());
            var number = new CatalogItemNumber(1);
            var catalogGroupId = new CatalogGroupId(Guid.NewGuid());
            var isActive = true;
            var name = "Test Catalog Item";
            var description = "Test Description";
            var unit = Unit.Item;
            var catalogItem = new CatalogItem(id, number, catalogGroupId, isActive, name, description, unit, null, null);

            // Act
            var result = catalogItem.Update(true, "", "Updated Description", unit);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            StringAssert.Contains(result.Error.ToString(), CatalogErrors.NameEmpty.Description);
        }

        [TestMethod]
        public void UpdatePrices_ShouldUpdateCatalogItemPrices()
        {
            // Arrange
            var id = new CatalogItemId(Guid.NewGuid());
            var number = new CatalogItemNumber(1);
            var catalogGroupId = new CatalogGroupId(Guid.NewGuid());
            var isActive = true;
            var name = "Test Catalog Item";
            var description = "Test Description";
            var unit = Unit.Item;
            var catalogItem = new CatalogItem(id, number, catalogGroupId, isActive, name, description, unit, null, null);
            var prices = new CatalogItemPrice[]
            {
                new(20, DateTime.Now),
                new(10, DateTime.Now.AddDays(-1))
            };

            // Act
            var result = catalogItem.UpdatePrices(prices);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(prices.Length, catalogItem.Prices.Count);
            CollectionAssert.AreEqual(prices, catalogItem.Prices as ICollection);
        }

        [TestMethod]
        public void UpdatePrices_ShouldReturnFailureResult_WhenNoValidPriceProvided()
        {
            // Arrange
            var id = new CatalogItemId(Guid.NewGuid());
            var number = new CatalogItemNumber(1);
            var catalogGroupId = new CatalogGroupId(Guid.NewGuid());
            var isActive = true;
            var name = "Test Catalog Item";
            var description = "Test Description";
            var unit = Unit.Item;
            var catalogItem = new CatalogItem(id, number, catalogGroupId, isActive, name, description, unit, null, null);
            var prices = new CatalogItemPrice[]
            {
                new(10, DateTime.Now.AddDays(2)),
                new(10, DateTime.Now.AddDays(1))
            };

            // Act
            var result = catalogItem.UpdatePrices(prices);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            StringAssert.Contains(result.Error.Code, CatalogErrors.NoValidPrice.Code);
        }
    }
}