using System;
using NUnit.Framework;

namespace PL.Units.Tests
{
	[TestFixture]
	public class LengthMetricTests
	{
		[Test]
		public void FromString_ValidString_CorrectQuantityType()
		{
			// Act
			var unitUnderTest = new LengthMetric().FromString("1 m");

			// Assert
			Assert.That(unitUnderTest.QuantityType, Is.EqualTo(QuantityType.Length));
		}

		[Test]
		[TestCase("1 Em")]
		[TestCase("1 Pm")]
		[TestCase("1 Tm")]
		[TestCase("1 Gm")]
		[TestCase("1 Mm")]
		[TestCase("1 km")]
		[TestCase("1 hm")]
		[TestCase("1 dam")]
		[TestCase("1 m")]
		[TestCase("1 dm")]
		[TestCase("1 cm")]
		[TestCase("1 mm")]
		[TestCase("1 μm")]
		[TestCase("1 nm")]
		[TestCase("1 pm")]
		[TestCase("1 fm")]
		[TestCase("1 am")]
		public void FromString_ValidString_CorrectUnitType(string asString)
		{
			// Act
			var unitUnderTest = new LengthMetric().FromString(asString);

			// Assert
			Assert.That(unitUnderTest.UnitType, Is.EqualTo((ushort)Length.LengthUnit.Metric));
		}

		[Test]
		[TestCase("1 Em", LengthMetric.MetricPrefix.Exa)]
		[TestCase("1 Pm", LengthMetric.MetricPrefix.Peta)]
		[TestCase("1 Tm", LengthMetric.MetricPrefix.Tera)]
		[TestCase("1 Gm", LengthMetric.MetricPrefix.Giga)]
		[TestCase("1 Mm", LengthMetric.MetricPrefix.Mega)]
		[TestCase("1 km", LengthMetric.MetricPrefix.Kilo)]
		[TestCase("1 hm", LengthMetric.MetricPrefix.Hecto)]
		[TestCase("1 dam", LengthMetric.MetricPrefix.Deca)]
		[TestCase("1 m", LengthMetric.MetricPrefix.Base)]
		[TestCase("1 dm", LengthMetric.MetricPrefix.Deci)]
		[TestCase("1 cm", LengthMetric.MetricPrefix.Centi)]
		[TestCase("1 mm", LengthMetric.MetricPrefix.Milli)]
		[TestCase("1 μm", LengthMetric.MetricPrefix.Micro)]
		[TestCase("1 nm", LengthMetric.MetricPrefix.Nano)]
		[TestCase("1 pm", LengthMetric.MetricPrefix.Pico)]
		[TestCase("1 fm", LengthMetric.MetricPrefix.Femto)]
		[TestCase("1 am", LengthMetric.MetricPrefix.Atto)]
		public void FromString_ValidString_CorrectUnitSubType(string asString, LengthMetric.MetricPrefix expectedMetricPrefix)
		{
			// Act
			var unitUnderTest = new LengthMetric().FromString(asString);

			// Assert
			Assert.That(unitUnderTest.UnitSubType, Is.EqualTo((ushort)expectedMetricPrefix));
		}

		[Test]
		[TestCase("  1 Em", LengthMetric.MetricPrefix.Exa)]
		[TestCase("1 Em  ", LengthMetric.MetricPrefix.Exa)]
		public void FromString_ValidStringWithSpaces_CorrectUnitSubType(string asString, LengthMetric.MetricPrefix expectedMetricPrefix)
		{
			// Act
			var unitUnderTest = new LengthMetric().FromString(asString);

			// Assert
			Assert.That(unitUnderTest.UnitSubType, Is.EqualTo((ushort)expectedMetricPrefix));

		}

		[Test]
		[TestCase("1 ml")]
		[TestCase("")]
		[TestCase("1 Rm")]
		public void FromString_InvalidString_ThrowsException(string asString)
		{
			Assert.Throws<ArgumentException>(() => new LengthMetric().FromString(asString));
		}

		[Test]
		public void FromString_NullString_ThrowsException()
		{
			Assert.Throws<ArgumentException>(() => new LengthMetric().FromString(null));
		}

		[Test]
		public void ConvertValue_FromKmToBase_CorrectValue()
		{
			// Arrange
			var unitUnderTest = (LengthMetric)new LengthMetric().FromString("12 km");
			const int expectedValue = 12000;

			// Act
			var actualValue = unitUnderTest.ValueInBaseUnitSubType;

			// Assert
			Assert.That(actualValue, Is.EqualTo(expectedValue));
		}

	}
}