using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;
using NetArchTest.Rules;
using System.Reflection;

namespace LIT.Smabu.ArchitectureTests
{
    [TestClass]
    public class DomainTests
    {
        private static readonly Assembly DomainAssembly = typeof(Domain.Common.Address).Assembly;

        [TestMethod]
        public void IdTypes_ShouldHaveDependencyToEntityId()
        {
            var result = Types.InAssembly(DomainAssembly)
                .That()
                .HaveNameEndingWith("Id")
                .Should()
                .HaveDependencyOn(typeof(EntityId<>).FullName)
                .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void Specifications_ShouldEndWithSpec()
        {
            var result = Types.InAssembly(DomainAssembly)
                              .That()
                              .HaveDependencyOn(typeof(Specification<>).FullName)
                              .Should()
                              .HaveNameEndingWith("Spec")
                              .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }
    }
}
