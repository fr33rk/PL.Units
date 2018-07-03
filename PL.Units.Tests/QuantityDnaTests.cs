using NUnit.Framework;

namespace PL.Units.Tests
{
    [TestFixture]
    public class QuantityDnaTests
    {
        [Test]
        public void ToInt_FromValidDna_ExpectedValue()
        {
            // Arrange
            var unitUnderTest = new QuantityDna()
            {
                QuantityType = QuantityType.Length,
                UnitType = (ushort)Length.LengthUnit.Metric,
                UnitSubType = (ushort)QuantityMetric.MetricPrefix.Centi,
                Precision = 2
            };

            var expectedResult = 1001002;

            // Act
            var actualResult = unitUnderTest.ToInt();

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void FromInt_FromValidDna_ExpectedValue()
        {
            // Act
            var actualResult = QuantityDna.FromInt(1001002);

            // Assert
            Assert.That(actualResult.QuantityType, Is.EqualTo(QuantityType.Length));
            Assert.That(actualResult.UnitType, Is.EqualTo((ushort)Length.LengthUnit.Metric));
            Assert.That(actualResult.UnitSubType, Is.EqualTo((ushort)QuantityMetric.MetricPrefix.Centi));
            Assert.That(actualResult.Precision, Is.EqualTo(2));
        }
    }
}