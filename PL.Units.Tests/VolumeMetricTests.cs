using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PL.Units.Tests
{
    public class VolumeMetricTests
    {
		[SetUp]
	    public void SetUp()
	    {
		    Thread.CurrentThread.CurrentCulture = new CultureInfo("nl");
	    }

	    [Test]
		[TestCase("1 m3")]
	    [TestCase("1 m³")]
		public void FromString_ValidString_CorrectQuantityType(string baseString)
	    {
		    // Act
		    var unitUnderTest = new VolumeMetric().FromString(baseString);

		    // Assert
		    Assert.That(unitUnderTest.QuantityType, Is.EqualTo(QuantityType.Volume));
	    }

	    [Test]
	    [TestCase("1 Em³")]
	    [TestCase("1 Pm³")]
	    [TestCase("1 Tm³")]
	    [TestCase("1 Gm³")]
	    [TestCase("1 Mm³")]
	    [TestCase("1 km³")]
	    [TestCase("1 hm³")]
	    [TestCase("1 dam³")]
	    [TestCase("1 m³")]
	    [TestCase("1 dm³")]
	    [TestCase("1 cm³")]
	    [TestCase("1 mm³")]
	    [TestCase("1 μm³")]
	    [TestCase("1 nm³")]
	    [TestCase("1 pm³")]
	    [TestCase("1 fm³")]
	    [TestCase("1 am³")]
	    public void FromString_ValidString_CorrectUnitType(string asString)
	    {
		    // Act
		    var unitUnderTest = new VolumeMetric().FromString(asString);

		    // Assert
		    Assert.That(unitUnderTest.UnitType, Is.EqualTo((ushort)Volume.VolumeUnit.Metric));
	    }

	    [Test]
	    [TestCase("1 Em³", QuantityMetric.MetricPrefix.Exa)]
	    [TestCase("1 Pm³", QuantityMetric.MetricPrefix.Peta)]
	    [TestCase("1 Tm³", QuantityMetric.MetricPrefix.Tera)]
	    [TestCase("1 Gm³", QuantityMetric.MetricPrefix.Giga)]
	    [TestCase("1 Mm³", QuantityMetric.MetricPrefix.Mega)]
	    [TestCase("1 km³", QuantityMetric.MetricPrefix.Kilo)]
	    [TestCase("1 hm³", QuantityMetric.MetricPrefix.Hecto)]
	    [TestCase("1 dam³", QuantityMetric.MetricPrefix.Deca)]
	    [TestCase("1 m³", QuantityMetric.MetricPrefix.Base)]
	    [TestCase("1 dm³", QuantityMetric.MetricPrefix.Deci)]
	    [TestCase("1 cm³", QuantityMetric.MetricPrefix.Centi)]
	    [TestCase("1 mm³", QuantityMetric.MetricPrefix.Milli)]
	    [TestCase("1 μm³", QuantityMetric.MetricPrefix.Micro)]
	    [TestCase("1 nm³", QuantityMetric.MetricPrefix.Nano)]
	    [TestCase("1 pm³", QuantityMetric.MetricPrefix.Pico)]
	    [TestCase("1 fm³", QuantityMetric.MetricPrefix.Femto)]
	    [TestCase("1 am³", QuantityMetric.MetricPrefix.Atto)]
	    public void FromString_ValidString_CorrectUnitSubType(string asString, QuantityMetric.MetricPrefix expectedMetricPrefix)
	    {
		    // Act
		    var unitUnderTest = new VolumeMetric().FromString(asString);

		    // Assert
		    Assert.That(unitUnderTest.UnitSubType, Is.EqualTo((ushort)expectedMetricPrefix));
	    }

	    [Test]
	    [TestCase("1 ml")]
	    [TestCase("")]
	    [TestCase("1 m")]
	    [TestCase("1 Rm3")]
		public void FromString_InvalidString_ThrowsException(string asString)
	    {
		    Assert.Throws<ArgumentException>(() => new VolumeMetric().FromString(asString));
	    }

	    [Test]
	    public void FromString_NullString_ThrowsException()
	    {
		    Assert.Throws<ArgumentException>(() => new VolumeMetric().FromString(null));
	    }

	    [Test]
	    public void ConvertValue_FromKmToBase_CorrectValue()
	    {
		    // Arrange
		    var unitUnderTest = (VolumeMetric)new VolumeMetric().FromString("12 km³");
		    const int expectedValue = 12000;

		    // Act
		    var actualValue = unitUnderTest.ValueInBaseUnitSubType;

		    // Assert
		    Assert.That(actualValue, Is.EqualTo(expectedValue));
	    }

	    [Test]
	    public void Constructor_WithValidDna_CreatesVolumeMetric()
	    {
		    // Arrange
		    var validDna = new QuantityDna
		    {
			    QuantityType = QuantityType.Volume,
			    UnitType = (ushort)Volume.VolumeUnit.Metric,
			    UnitSubType = (ushort)QuantityMetric.MetricPrefix.Base,
			    Precision = 2
		    };

		    // Act
		    var unitUnderTest = new VolumeMetric(validDna, 21.09);

		    // Assert
		    Assert.That(unitUnderTest.ToString(), Is.EqualTo("21,09m³"));
	    }

	    [Test]
	    public void Constructor_InvalidDna_ThrowsException()
	    {
		    // Arrange
		    var invalidDna = new QuantityDna
		    {
			    QuantityType = QuantityType.Length,
			    UnitType = (ushort)Volume.VolumeUnit.Liter,
			    UnitSubType = (ushort)QuantityMetric.MetricPrefix.Base,
				Precision = 2
		    };

		    // Act and Assert
		    Assert.Throws<ArgumentException>(() =>
		    {
			    var unused = new VolumeMetric(invalidDna, 21.09);
		    });
	    }
	}
}
