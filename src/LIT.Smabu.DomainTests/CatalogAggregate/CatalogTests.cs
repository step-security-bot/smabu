using LIT.Smabu.Domain.CatalogAggregate;

namespace LIT.Smabu.DomainTests.CatalogAggregate
{
    [TestClass]
    public class CatalogTests
    {
        [TestMethod]
        public void Create_ShouldCreateCatalogWithDefaultName()
        {
            // Arrange
            var id = new CatalogId(Guid.NewGuid());
            var name = "Default Name";

            // Act
            var catalog = Catalog.Create(id, name);

            // Assert
            Assert.AreEqual(name, catalog.Name);
        }

        [TestMethod]
        public void Create_ShouldCreateCatalogWithProvidedName()
        {
            // Arrange
            var id = new CatalogId(Guid.NewGuid());
            var name = "Test Catalog";

            // Act
            var catalog = Catalog.Create(id, name);

            // Assert
            Assert.AreEqual(name, catalog.Name);
        }

        [TestMethod]
        public void AddGroup_ShouldReturnError_WhenGroupNameIsEmpty()
        {
            // Arrange
            var id = new CatalogId(Guid.NewGuid());
            var name = "Test Catalog";
            var catalog = Catalog.Create(id, name);
            var groupId = new CatalogGroupId(Guid.NewGuid());
            var groupName = "";

            // Act
            var result = catalog.AddGroup(groupId, groupName, "Group Description");

            // Assert
            Assert.AreEqual(CatalogErrors.NameEmpty, result.Error);
        }

        [TestMethod]
        public void AddGroup_ShouldReturnError_WhenGroupNameAlreadyExists()
        {
            // Arrange
            var id = new CatalogId(Guid.NewGuid());
            var name = "Test Catalog";
            var catalog = Catalog.Create(id, name);
            var groupId = new CatalogGroupId(Guid.NewGuid());
            var groupName = "Group 1";
            catalog.AddGroup(groupId, groupName, "Group Description");
            var duplicateGroupName = "Group 1";

            // Act
            var result = catalog.AddGroup(groupId, duplicateGroupName, "Group Description");

            // Assert
            Assert.AreEqual(CatalogErrors.NameAlreadyExists, result.Error);
        }

        [TestMethod]
        public void AddGroup_ShouldAddGroupToCatalog_WhenGroupNameIsValid()
        {
            // Arrange
            var id = new CatalogId(Guid.NewGuid());
            var name = "Test Catalog";
            var catalog = Catalog.Create(id, name);
            var groupId = new CatalogGroupId(Guid.NewGuid());
            var groupName = "Group 1";

            // Act
            var result = catalog.AddGroup(groupId, groupName, "Group Description");

            // Assert
            Assert.IsNotNull(result.Value);
            CollectionAssert.Contains(catalog.Groups.ToList(), result.Value);
        }
    }
}
