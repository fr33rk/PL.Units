﻿using NUnit.Framework;
using System;
using System.Globalization;
using System.Threading;

namespace PL.Units.Tests
{
	[TestFixture]
	public class LengthMetricTests
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
		[TestCase("1 Em", QuantityMetric.MetricPrefix.Exa)]
		[TestCase("1 Pm", QuantityMetric.MetricPrefix.Peta)]
		[TestCase("1 Tm", QuantityMetric.MetricPrefix.Tera)]
		[TestCase("1 Gm", QuantityMetric.MetricPrefix.Giga)]
		[TestCase("1 Mm", QuantityMetric.MetricPrefix.Mega)]
		[TestCase("1 km", QuantityMetric.MetricPrefix.Kilo)]
		[TestCase("1 hm", QuantityMetric.MetricPrefix.Hecto)]
		[TestCase("1 dam", QuantityMetric.MetricPrefix.Deca)]
		[TestCase("1 m", QuantityMetric.MetricPrefix.Base)]
		[TestCase("1 dm", QuantityMetric.MetricPrefix.Deci)]
		[TestCase("1 cm", QuantityMetric.MetricPrefix.Centi)]
		[TestCase("1 mm", QuantityMetric.MetricPrefix.Milli)]
		[TestCase("1 μm", QuantityMetric.MetricPrefix.Micro)]
		[TestCase("1 nm", QuantityMetric.MetricPrefix.Nano)]
		[TestCase("1 pm", QuantityMetric.MetricPrefix.Pico)]
		[TestCase("1 fm", QuantityMetric.MetricPrefix.Femto)]
		[TestCase("1 am", QuantityMetric.MetricPrefix.Atto)]
		public void FromString_ValidString_CorrectUnitSubType(string asString, QuantityMetric.MetricPrefix expectedMetricPrefix)
		{
			// Act
			var unitUnderTest = new LengthMetric().FromString(asString);

			// Assert
			Assert.That(unitUnderTest.UnitSubType, Is.EqualTo((ushort)expectedMetricPrefix));
		}

		[Test]
		[TestCase("  1 Em", QuantityMetric.MetricPrefix.Exa)]
		[TestCase("1 Em  ", QuantityMetric.MetricPrefix.Exa)]
		public void FromString_ValidStringWithSpaces_CorrectUnitSubType(string asString, QuantityMetric.MetricPrefix expectedMetricPrefix)
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

		[Test]
		public void Constructor_WithValidDna_CreatesLengthMetric()
		{
			// Arrange
			var validDna = new QuantityDna
			{
				QuantityType = QuantityType.Length,
				UnitType = (ushort)Length.LengthUnit.Metric,
				UnitSubType = (ushort)QuantityMetric.MetricPrefix.Base,
				Precision = 2
			};

			// Act
			var unitUnderTest = new LengthMetric(validDna, 21.09);

			// Assert
			Assert.That(unitUnderTest.ToString(), Is.EqualTo("21,09m"));
		}

		[Test]
		public void Constructor_InvalidDna_ThrowsException()
		{
			// Arrange
			var invalidDna = new QuantityDna
			{
				QuantityType = QuantityType.Length,
				UnitType = (ushort)Length.LengthUnit.Imperial,
				UnitSubType = (ushort)QuantityMetric.MetricPrefix.Base,
				Precision = 2
			};

			// Act and Assert
			Assert.Throws<ArgumentException>(() =>
			{
				var unused = new LengthMetric(invalidDna, 21.09);
			});
		}
	}
}