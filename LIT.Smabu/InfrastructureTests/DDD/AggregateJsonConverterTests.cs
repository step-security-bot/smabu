using Microsoft.VisualStudio.TestTools.UnitTesting;
using LIT.Smabu.Infrastructure.Persistence;
using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Infrastructure.DDD;

namespace LIT.Smabu.InfrastructureTests.DDD
{
    [TestClass()]
    public class AggregateJsonConverterTests
    {
        private InvoiceId fakeAggregateId = default!;
        private Invoice fakeAggregate = default!;

        [TestInitialize]
        public void TestInitialize()
        {
            fakeAggregateId = new InvoiceId(Guid.NewGuid());
            fakeAggregate = CreateFakeAggregate(fakeAggregateId);
        }

        [TestMethod()]
        public void ConvertToJsonTest()
        {
            // Arrange

            // Act
            var testee = AggregateJsonConverter.ConvertToJson(fakeAggregate);
            // Assert
            Assert.IsNotNull(testee);
            Assert.IsTrue(testee.Contains(fakeAggregateId.Value.ToString()));
        }

        [TestMethod()]
        public void ConvertToAggregateTest()
        {
            // Arrange
            var json = AggregateJsonConverter.ConvertToJson(fakeAggregate);
            // Act
            var testee = AggregateJsonConverter.ConvertFromJson<Invoice>(json);
            // Assert
            Assert.IsNotNull(testee);
            Assert.AreEqual(typeof(Invoice), fakeAggregate.GetType());
            Assert.IsNotNull(testee.Id);
            Assert.AreEqual(fakeAggregateId, testee.Id);
            Assert.AreEqual(3, testee.Items.Count);
            Assert.IsNotNull(testee.Meta);
        }

        private static Invoice CreateFakeAggregate(InvoiceId id)
        {
            var result = Invoice.Create(id, new CustomerId(Guid.NewGuid()), 2023,
                new Address("fake", "", "", "", "", "", ""),
                DatePeriod.CreateFrom(DateTime.Now.AddDays(-1), DateTime.Now), Currency.GetEuro(), 19, "fake2");
            result.AddItem(new InvoiceItemId(Guid.NewGuid()), "fakeLine1", new Quantity(1, "fake"), 1, null);
            result.AddItem(new InvoiceItemId(Guid.NewGuid()), "fakeLine2", new Quantity(1, "fake"), 2, null);
            result.AddItem(new InvoiceItemId(Guid.NewGuid()), "fakeLine3", new Quantity(1, "fake"), 3, null);
            result.UpdateMeta(new AggregateMeta(1, DateTime.Now, Guid.NewGuid().ToString(), "fake", null, null, null));
            return result;
        }
    }
}