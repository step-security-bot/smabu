using Microsoft.VisualStudio.TestTools.UnitTesting;
using LIT.Smabu.Infrastructure.Persistence;
using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Domain.Shared.Customers;

namespace LIT.Smabu.Infrastructure.DDD.Tests
{
    [TestClass()]
    public class AggregateJsonConverterTests
    {
        private InvoiceId fakeAggregateId = default!;
        private Invoice fakeAggregate = default!;

        [TestInitialize]
        public void TestInitialize()
        {
            this.fakeAggregateId = new InvoiceId(Guid.NewGuid());
            this.fakeAggregate = CreateFakeAggregate(this.fakeAggregateId);
        }

        [TestMethod()]
        public void ConvertToJsonTest()
        {
            // Arrange

            // Act
            var testee = AggregateJsonConverter.ConvertToJson(this.fakeAggregate);
            // Assert
            Assert.IsNotNull(testee);
            Assert.IsTrue(testee.Contains(this.fakeAggregateId.Value.ToString()));
        }

        [TestMethod()]
        public void ConvertToAggregateTest()
        {
            // Arrange
            var json = AggregateJsonConverter.ConvertToJson(this.fakeAggregate);
            // Act
            var testee = AggregateJsonConverter.ConvertFromJson<Invoice>(json);
            // Assert
            Assert.IsNotNull(testee);
            Assert.AreEqual(typeof(Invoice), fakeAggregate.GetType());
            Assert.IsNotNull(testee.Id);
            Assert.AreEqual(this.fakeAggregateId, testee.Id);
            Assert.AreEqual(3, testee.InvoiceLines.Count);
            Assert.IsNotNull(testee.Meta);
        }

        private static Invoice CreateFakeAggregate(InvoiceId id)
        {
            var result = Invoice.Create(id, new CustomerId(Guid.NewGuid()), 2023, DatePeriod.CreateFrom(DateTime.Now.AddDays(-1), DateTime.Now), Currency.GetEuro(), 19, "fake2");
            result.AddInvoiceLine("fakeLine1", new Quantity(1, "fake"), 1, null);
            result.AddInvoiceLine("fakeLine2", new Quantity(1, "fake"), 2, null);
            result.AddInvoiceLine("fakeLine3", new Quantity(1, "fake"), 3, null);
            result.UpdateMeta(new AggregateMeta(1, DateTime.Now, Guid.NewGuid().ToString(), "fake", null, null, null));
            return result;
        }
    }
}