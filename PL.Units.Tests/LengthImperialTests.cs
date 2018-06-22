using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace PL.Units.Tests
{
	[TestFixture]
    public class LengthImperialTests
    {
		[Test]
		public void FromString_ValidString_CorrectQuantityType()
		{
			// Act
			var unitUnderTest = new LengthImperial().FromString("1 yard");

			// Assert
			Assert.That(unitUnderTest.QuantityType, Is.EqualTo(QuantityType.Length));
		}

		[Test]
		[TestCase("1 thou"   )]
		[TestCase("1 inch"   )]
		[TestCase("1 foot"   )]
		[TestCase("1 yard"   )]
		[TestCase("1 chain"  )]
		[TestCase("1 furlong")]
		[TestCase("1 mile"   )]
		[TestCase("1 th"	 )]
		[TestCase("1 in"	 )]
		[TestCase("1 ft"	 )]
		[TestCase("1 yd"	 )]
		[TestCase("1 ch"	 )]
		[TestCase("1 fur"    )]
		public void FromString_ValidString_CorrectUnitType(string asString)
		{
			// Act
			var unitUnderTest = new LengthImperial().FromString(asString);

			// Assert
			Assert.That(unitUnderTest.UnitType, Is.EqualTo((ushort)Length.LengthUnit.Imperial));
		}

		[Test]
		[TestCase("1 thou"   , LengthImperial.ImperialLengthUnit.Thou)]  
		[TestCase("1 inch"   , LengthImperial.ImperialLengthUnit.Inch)]   
		[TestCase("1 foot"   , LengthImperial.ImperialLengthUnit.Foot)]   
		[TestCase("1 yard"   , LengthImperial.ImperialLengthUnit.Yard)]   
		[TestCase("1 chain"  , LengthImperial.ImperialLengthUnit.Chain)] 
		[TestCase("1 furlong", LengthImperial.ImperialLengthUnit.Furlong)]
		[TestCase("1 mile"   , LengthImperial.ImperialLengthUnit.Mile)]
		[TestCase("1 th"  	 , LengthImperial.ImperialLengthUnit.Thou)]  
		[TestCase("1 in"	 , LengthImperial.ImperialLengthUnit.Inch)]   
		[TestCase("1 ft"	 , LengthImperial.ImperialLengthUnit.Foot)]   
		[TestCase("1 yd"	 , LengthImperial.ImperialLengthUnit.Yard)]   
		[TestCase("1 ch"	 , LengthImperial.ImperialLengthUnit.Chain)] 
		[TestCase("1 fur"    , LengthImperial.ImperialLengthUnit.Furlong)]
		[TestCase("1 ml"     , LengthImperial.ImperialLengthUnit.Mile)]
		public void FromString_ValidString_CorrectUnitSubType(string asString, LengthMetric.MetricPrefix expectedMetricPrefix)
		{
			// Act
			var unitUnderTest = new LengthImperial().FromString(asString);

			// Assert
			Assert.That(unitUnderTest.UnitSubType, Is.EqualTo((ushort)expectedMetricPrefix));
		}

		[Test]
		[TestCase("  1 in", LengthImperial.ImperialLengthUnit.Inch)]
		[TestCase("1 in  ", LengthImperial.ImperialLengthUnit.Inch)]
		public void FromString_ValidStringWithSpaces_CorrectUnitSubType(string asString, LengthMetric.MetricPrefix expectedMetricPrefix)
		{
			// Act
			var unitUnderTest = new LengthImperial().FromString(asString);

			// Assert
			Assert.That(unitUnderTest.UnitSubType, Is.EqualTo((ushort)expectedMetricPrefix));

		}

		[Test]
		[TestCase("1 m")]
		[TestCase("")]
		public void FromString_InvalidString_ThrowsException(string asString)
		{
			Assert.Throws<ArgumentException>(() => new LengthImperial().FromString(asString));
		}

		[Test]
		public void FromString_NullString_ThrowsException()
		{
			Assert.Throws<ArgumentException>(() => new LengthImperial().FromString(null));
		}

		[Test]
		public void ConvertValue_FromMileToBase_CorrectValue()
		{
			// Arrange
			var unitUnderTest = new LengthImperial().FromString("12 mile");
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
                UnitType = (ushort)Length.LengthUnit.Imperial,
                UnitSubType = (ushort)LengthImperial.ImperialLengthUnit.Inch,
                Precision = 2
            };

            // Act
            var unitUnderTest = new LengthImperial(validDna, 21.09);

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
                UnitSubType = (ushort)LengthImperial.ImperialLengthUnit.Inch,
                Precision = 2
            };

            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var unused = new LengthImperial(invalidDna, 21.09);
            });
        }
    }
}
