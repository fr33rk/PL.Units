using NUnit.Framework;
using System;
using System.Globalization;
using System.Threading;

namespace PL.Units.Tests
{
    public class QuantityTests
    {
	    [SetUp]
	    public void SetUp()
	    {
		    Thread.CurrentThread.CurrentCulture = new CultureInfo("nl");
	    }

		[Test]
        [TestCase("1 km")]
        [TestCase("1 mile")]
        [TestCase("1 yd")]
        public void FromString_LengthMetricString_CreatesLengthMetric(string asString)
        {
            // Arrange
            var test = QuantityFactory.FromString(asString);

            Assert.That(test, Is.Not.Null);
            Assert.That(test.QuantityType, Is.EqualTo(QuantityType.Length));
        }

        [Test]
        public void FromString_UnknownUnit_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => QuantityFactory.FromString("1 FdL"));
        }

        [Test]
        // Decimals
        [TestCase("1", 1d)]
        [TestCase("1.1234", 1.1234d)]
        [TestCase("1,1234", 1.1234d)]
        // Spaces
        [TestCase(" 1.1234", 1.1234d)]
        [TestCase("  1.1234", 1.1234d)]
        [TestCase("1.1234 ", 1.1234d)]
        [TestCase("1.1234  ", 1.1234d)]
        // Minus
        [TestCase("-1.1234 ", -1.1234d)]
        // Exponents
        [TestCase("1.1234e+10", 1.1234e+10d)]
        [TestCase("1.1234e-10", 1.1234e-10d)]
        public void FromString_validString_ValueConverted(string asString, double expectedValue)
        {
            // Arrange and act
            var unitUnderTest = new LengthMetric().FromString(asString + "m");

            // Assert
            Assert.That(unitUnderTest.Value, Is.EqualTo(expectedValue));
        }

        [Test]
        [TestCase("a")]
        [TestCase("1. 1234")]
        [TestCase("1_1234")]
        [TestCase("1.1234a+10")]
        [TestCase("1.1234e+10.2")]
        public void FromString_InvalidString_ExceptionThrown(string asString)
        {
            Assert.Ignore("Does not work");
            Assert.Throws<ArgumentException>(() => new LengthMetric().FromString(asString + "m"));
        }

        [Test]
        [TestCase("1m", 0)]
        [TestCase("1.1m", 1)]
        [TestCase("1,1m", 1)]
        [TestCase("1,12m", 2)]
        [TestCase("1,00m", 2)]
        public void FromString_ValidString_CorrectDecimals(string asString, int expectedDecimals)
        {
            // Arrange and act
            var unitUnderTest = new LengthMetric().FromString(asString);

            // Assert
            Assert.That(unitUnderTest.Precision, Is.EqualTo(expectedDecimals));
        }

        [Test]
        [TestCase(QuantityType.Length, (ushort)Length.LengthUnit.Metric, (ushort)LengthMetric.MetricPrefix.Centi, typeof(LengthMetric))]
        [TestCase(QuantityType.Length, (ushort)Length.LengthUnit.Imperial, (ushort)LengthImperial.ImperialLengthUnit.Chain, typeof(LengthImperial))]
        [TestCase(QuantityType.Length, (ushort)Length.LengthUnit.UsCustomary, (ushort)LengthUsCustomary.UsCustomaryLengthUnit.Pica, typeof(LengthUsCustomary))]
        public void FromDna_ValidDna_QuantityCreated(QuantityType actualType, ushort actualSubType, ushort actualPrefix, Type expectedType)
        {
            // Arrange
            var dna = new QuantityDna()
            {
                QuantityType = actualType,
                UnitType = actualSubType,
                UnitSubType = actualPrefix
            };

            const int value = 100;

            // Act
            var actualResult = QuantityFactory.FromDna(dna, value);

            // Assert
            Assert.That(actualResult, Is.InstanceOf(expectedType));
            Assert.That(actualResult.Value, Is.EqualTo(value));
            Assert.That(actualResult.UnitSubType, Is.EqualTo(actualPrefix));
        }

        [Test]
        [TestCase("1,00m", "2,00m", "3,00m")]
        [TestCase("1,0m", "10cm", "1,1m")]
        [TestCase("10cm", "1,0m", "110cm")]
        [TestCase("1.00 cm", "1 inch", "3,54cm")]
        public void AddQuantity_SameUnits_ExpectedResult(string valueA, string valueB, string expectedOutcome)
        {
            var test = new LengthMetric();

            // Arrange
            var quantityA = QuantityFactory.FromString(valueA);
            var quantityB = QuantityFactory.FromString(valueB);

            // Act
            var actualResult = (quantityA + quantityB).ToString();

            // Assert
            Assert.That(actualResult.ToString(), Is.EqualTo(expectedOutcome));
        }


        [Test]
        public void AddQuantity_DifferentTypes_ThrowException()
        {
            Assert.Ignore("There are no other quantity types to test with yet.");

            // Arrange
            
            // Act

            // Assert
        }


        [Test]
        [TestCase("1,00m", 2, "3,00m")]
        [TestCase("1,00m", 0.2, "1,20m")]
        [TestCase("1,00m", 0.02, "1,02m")]
        [TestCase("1,00m", 0.002, "1,00m")]
        public void AddDouble_DifferentUnits_ExpectedBehavior(string valueA, double valueB, string expectedOutcome)
        {
            // Arrange
            var quantityA = QuantityFactory.FromString(valueA);

            // Act
            var actualResult = quantityA + valueB;

            // Assert
            Assert.That(actualResult.ToString(), Is.EqualTo(expectedOutcome));
        }

        [Test]
        [TestCase("1,00m", "2,00m", "-1,00m")]
        [TestCase("1,0m", "10cm", "0,9m")]
        [TestCase("10cm", "1,0m", "-90cm")]
        [TestCase("1.00 cm", "1 inch", "-1,54cm")]
        public void SubtractQuantity_SameUnits_ExpectedResult(string valueA, string valueB, string expectedOutcome)
        {
            var test = new LengthMetric();

            // Arrange
            var quantityA = QuantityFactory.FromString(valueA);
            var quantityB = QuantityFactory.FromString(valueB);

            // Act
            var actualResult = (quantityA - quantityB).ToString();

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedOutcome));
        }

        [Test]
        [TestCase("1,00m", 2, "-1,00m")]
        [TestCase("1,00m", 0.2, "0,80m")]
        [TestCase("1,00m", 0.02, "0,98m")]
        [TestCase("1,00m", 0.002, "1,00m")]
        public void SubtractDouble_DifferentUnits_ExpectedBehavior(string valueA, double valueB, string expectedOutcome)
        {
            // Arrange
            var quantityA = QuantityFactory.FromString(valueA);

            // Act
            var actualResult = quantityA - valueB;

            // Assert
            Assert.That(actualResult.ToString(), Is.EqualTo(expectedOutcome));
        }
    }
}