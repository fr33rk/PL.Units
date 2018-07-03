using System;
using System.Globalization;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace PL.Units.Tests
{
    [TestFixture]
    public class LengthUsCustomaryTests
    {
	    [SetUp]
	    public void SetUp()
	    {
		    Thread.CurrentThread.CurrentCulture = new CultureInfo("nl");
	    }

		[Test]
        public void FromString_ValidString_CorrectQuantityType()
        {
            // Act
            var unitUnderTest = new LengthUsCustomary().FromString("1 yard");

            // Assert
            Assert.That(unitUnderTest.QuantityType, Is.EqualTo(QuantityType.Length));
        }

        [Test]
        [TestCase("1 point")]
        [TestCase("1 p")]
        [TestCase("1 pica")]
        [TestCase("1 pi")]
        [TestCase("1 inch")]
        [TestCase("1 in")]
        [TestCase("1 foot")]
        [TestCase("1 ft")]
        [TestCase("1 yard")]
        [TestCase("1 yd")]
        [TestCase("1 mile")]
        [TestCase("1 ml")]
        public void FromString_ValidString_CorrectUnitType(string asString)
        {
            // Act
            var unitUnderTest = new LengthUsCustomary().FromString(asString);

            // Assert
            Assert.That(unitUnderTest.UnitType, Is.EqualTo((ushort) Length.LengthUnit.UsCustomary));
        }

        [Test]
        [TestCase("1 point", LengthUsCustomary.UsCustomaryLengthUnit.Point)]
        [TestCase("1 p", LengthUsCustomary.UsCustomaryLengthUnit.Point)]
        [TestCase("1 pica", LengthUsCustomary.UsCustomaryLengthUnit.Pica)]
        [TestCase("1 pi", LengthUsCustomary.UsCustomaryLengthUnit.Pica)]
        [TestCase("1 inch", LengthUsCustomary.UsCustomaryLengthUnit.Inch)]
        [TestCase("1 in", LengthUsCustomary.UsCustomaryLengthUnit.Inch)]
        [TestCase("1 foot", LengthUsCustomary.UsCustomaryLengthUnit.Foot)]
        [TestCase("1 ft", LengthUsCustomary.UsCustomaryLengthUnit.Foot)]
        [TestCase("1 yard", LengthUsCustomary.UsCustomaryLengthUnit.Yard)]
        [TestCase("1 yd", LengthUsCustomary.UsCustomaryLengthUnit.Yard)]
        [TestCase("1 mile", LengthUsCustomary.UsCustomaryLengthUnit.Mile)]
        [TestCase("1 ml", LengthUsCustomary.UsCustomaryLengthUnit.Mile)]
        public void FromString_ValidString_CorrectUnitSubType(string asString,
            LengthUsCustomary.UsCustomaryLengthUnit expectedMetricPrefix)
        {
            // Act
            var unitUnderTest = new LengthUsCustomary().FromString(asString);

            // Assert
            Assert.That(unitUnderTest.UnitSubType, Is.EqualTo((ushort) expectedMetricPrefix));
        }

        [Test]
        [TestCase("  1 in", LengthUsCustomary.UsCustomaryLengthUnit.Inch)]
        [TestCase("1 in  ", LengthUsCustomary.UsCustomaryLengthUnit.Inch)]
        public void FromString_ValidStringWithSpaces_CorrectUnitSubType(string asString,
            LengthUsCustomary.UsCustomaryLengthUnit expectedMetricPrefix)
        {
            // Act
            var unitUnderTest = new LengthUsCustomary().FromString(asString);

            // Assert
            Assert.That(unitUnderTest.UnitSubType, Is.EqualTo((ushort) expectedMetricPrefix));
        }

        [Test]
        [TestCase("1 m")]
        [TestCase("")]
        public void FromString_InvalidString_ThrowsException(string asString)
        {
            Assert.Throws<ArgumentException>(() => new LengthUsCustomary().FromString(asString));
        }

        [Test]
        public void FromString_NullString_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new LengthUsCustomary().FromString(null));
        }

        [Test]
        public void ConvertValue_FromMileToBase_CorrectValue()
        {
            // Arrange
            var unitUnderTest = new LengthUsCustomary().FromString("12 mile");
            const int expectedValue = 63360;

            // Act
            var actualValue = unitUnderTest.ValueInBaseUnitSubType;

            // Assert
            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [Test]
        public void Constructor_WithValidDna_CreatesLengthUsCustomary()
        {
            // Arrange
            var validDna = new QuantityDna
            {
                QuantityType = QuantityType.Length,
                UnitType = (ushort) Length.LengthUnit.UsCustomary,
                UnitSubType = (ushort) LengthUsCustomary.UsCustomaryLengthUnit.Inch,
                Precision = 2
            };

            // Act
            var unitUnderTest = new LengthUsCustomary(validDna, 21.09);

            // Assert
            Assert.That(unitUnderTest.ToString(), Is.EqualTo("21,09in"));
        }


        [Test]
        public void Constructor_InvalidDna_ThrowsException()
        {
            // Arrange
            var invalidDna = new QuantityDna
            {
                QuantityType = QuantityType.Length,
                UnitType = (ushort)Length.LengthUnit.Metric,
                UnitSubType = (ushort)LengthUsCustomary.UsCustomaryLengthUnit.Inch,
                Precision = 2
            };

            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var unused = new LengthUsCustomary(invalidDna, 21.09);
            });
        }
    }
}