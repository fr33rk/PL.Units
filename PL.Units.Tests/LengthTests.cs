using System;
using NUnit.Framework;

namespace PL.Units.Tests
{
	[TestFixture]
	public class LengthTests
	{

        [Test]
        public void Constructor_InvalidDna_ThrowsException()
        {
            // Arrange
            var invalidDna = new QuantityDna
            {
                QuantityType = QuantityType.Area
            };

            // Act and assert
            Assert.Throws<ArgumentException>(() => new LengthMetric(invalidDna, 0));
        }
	}
}