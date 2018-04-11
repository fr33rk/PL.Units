﻿using System;
using NUnit.Framework;

namespace PL.Units.Tests
{
	public class QuantityTests
	{
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
			Assert.Throws<ArgumentException>(() => new LengthMetric().FromString(asString + "m"));
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
        [TestCase("1,0m", "10cm", "1,1m")]
	    public void AddQuantity_SameUnits_ExpectedResult(string valueA, string valueB, string expectedOutcome)
	    {
            // Arrange
	        var quantityA = QuantityFactory.FromString(valueA);
	        var quantityB = QuantityFactory.FromString(valueB);

            // Act
	        var actualResult = quantityA + quantityB;

            // Assert
            Assert.That(actualResult.ToString(), Is.EqualTo(expectedOutcome));

	    }
	}
}