using LIT.Smabu.Domain.Shared.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LIT.Smabu.Shared.Domain.Contracts.Tests
{
    [TestClass()]
    public class SimpleValueObjectTests
    {
        private SVOStringFakeClass testeeString = default!;
        private SVOIntFakeClass testeeInt = default!;

        [TestInitialize]
        public void TestInitialize()
        {
            this.testeeString = new SVOStringFakeClass("fake");
            this.testeeInt = new SVOIntFakeClass(42);
        }

        [TestMethod()]
        public void EqualsTest_SameValue_IsEqual()
        {
            // Arrange
            var compareObjectStringSameValue = new SVOStringFakeClass("fake");
            var compareObjectIntSameValue = new SVOIntFakeClass(42);

            // Act
            var resultForString = this.testeeString.Equals(compareObjectStringSameValue);
            var resultForInt = this.testeeInt.Equals(compareObjectIntSameValue);

            // Assert
            Assert.IsTrue(resultForString);
            Assert.IsTrue(resultForInt);
            Assert.AreEqual(testeeString, compareObjectStringSameValue);
            Assert.AreEqual(testeeInt, compareObjectIntSameValue);
        }

        [TestMethod()]
        public void EqualsTest_DifferentValue_IsNotEqual()
        {
            // Arrange
            var compareObjectStringSameValue = new SVOStringFakeClass("fake123");
            var compareObjectIntSameValue = new SVOIntFakeClass(42123);

            // Act
            var resultForString = this.testeeString.Equals(compareObjectStringSameValue);
            var resultForInt = this.testeeInt.Equals(compareObjectIntSameValue);

            // Assert
            Assert.IsFalse(resultForString);
            Assert.IsFalse(resultForInt);
            Assert.AreNotEqual(testeeString, compareObjectStringSameValue);
            Assert.AreNotEqual(testeeInt, compareObjectIntSameValue);
        }

        [TestMethod()]
        public void GetHashCodeTest_SameValue_ReturnsSameHashCode()
        {
            // Arrange
            var compareObjectStringSameValue = new SVOStringFakeClass("fake");

            // Act
            var hashCode1 = this.testeeString.GetHashCode();
            var hashCode2 = compareObjectStringSameValue.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2);
        }

        [TestMethod()]
        public void CompareToTest_SameValue_Succeeds()
        {
            // Arrange
            var compareObjectStringSameValue = new SVOStringFakeClass("fake");
            var compareObjectIntSameValue = new SVOIntFakeClass(42);

            // Act
            var resultForString = this.testeeString.CompareTo(compareObjectStringSameValue);
            var resultForInt = this.testeeInt.CompareTo(compareObjectIntSameValue);

            // Assert
            Assert.AreEqual(0, resultForString);
            Assert.AreEqual(0, resultForInt);
        }

        private record SVOStringFakeClass : SimpleValueObject<string>
        {
            public SVOStringFakeClass(string value) : base(value)
            {
            }
        }

        private record SVOIntFakeClass : SimpleValueObject<int>
        {
            public SVOIntFakeClass(int value) : base(value)
            {
            }
        }
    }
}