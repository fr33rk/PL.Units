using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PL.Units.Tests
{
    public class QuantityTests
    {
	    [Test]
	    public void FromString_LengthMetricString_CreatesLengthMetric()
	    {
			// Arrange
		    var test = QuantityFactory.FromString("1 km");

			Assert.That(test, Is.Not.Null);
			Assert.That(test.QuantityType, Is.EqualTo(QuantityType.Length));
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
		public void FromString_invalidString_ExceptionThrown(string asString)
		{
			Assert.Throws<ArgumentException>(() => new LengthMetric().FromString(asString + "m"));
		}
	}
}
