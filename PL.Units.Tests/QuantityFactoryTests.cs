using NUnit.Framework;
using System;

namespace PL.Units.Tests
{
    [TestFixture]
    public class QuantityFactoryTests
    {
        [Test]
        [TestCase("1 cm", typeof(LengthMetric))]
        [TestCase("1 in", typeof(LengthImperial))]
        [TestCase("1 pi", typeof(LengthUsCustomary))]
        public void FromString_ValidString_CreatesQuantity(string asString, Type expectedType)
        {
            // Act
            var result = QuantityFactory.FromString(asString);

            // Assert
            Assert.That(result.GetType(), Is.EqualTo(expectedType));
        }

        [Test]
        public void FromString_InvalidString_ThrowsException()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() => QuantityFactory.FromString("1 kg"));
        }

        [Test]
        [TestCase(QuantityType.Length, (ushort)Length.LengthUnit.Metric, typeof(LengthMetric))]
        [TestCase(QuantityType.Length, (ushort)Length.LengthUnit.Imperial, typeof(LengthImperial))]
        [TestCase(QuantityType.Length, (ushort)Length.LengthUnit.UsCustomary, typeof(LengthUsCustomary))]
        public void FromDna_ValidDna_CreatesQuantity(QuantityType type, ushort unitType, Type expectedType)
        {
            // Arrange
            var dna = new QuantityDna
            {
                QuantityType = type,
                UnitType = unitType
            };

            // Act
            var result = QuantityFactory.FromDna(dna, 0);

            // Assert
            Assert.That(result.GetType(), Is.EqualTo(expectedType));
        }

        [Test]
        public void FromDna_InvalidDna_ThrowsException()
        {
            // Arrange
            // Arrange
            var dna = new QuantityDna
            {
                QuantityType = (QuantityType)ushort.MaxValue,
            };

            // Act and assert
            Assert.Throws<ArgumentException>(() => QuantityFactory.FromDna(dna, 0));
        }


        [Test]
        [TestCase(QuantityType.Length, (ushort)Length.LengthUnit.Metric, typeof(LengthMetric))]
        [TestCase(QuantityType.Length, (ushort)Length.LengthUnit.Imperial, typeof(LengthImperial))]
        [TestCase(QuantityType.Length, (ushort)Length.LengthUnit.UsCustomary, typeof(LengthUsCustomary))]
        public void LengthFromDna_ValidDna_CreatesQuantity(QuantityType type, ushort unitType, Type expectedType)
        {
            // Arrange
            var dna = new QuantityDna
            {
                QuantityType = type,
                UnitType = unitType
            };

            // Act
            var result = QuantityFactory.LengthFromDna(dna, 0);

            // Assert
            Assert.That(result.GetType(), Is.EqualTo(expectedType));
        }

        [Test]
        public void LengthFromDna_InvalidDna_ThrowsException()
        {
            // Arrange
            // Arrange
            var dna = new QuantityDna
            {
                QuantityType = (QuantityType)ushort.MaxValue,
                UnitType = ushort.MaxValue
            };

            // Act and assert
            Assert.Throws<ArgumentException>(() => QuantityFactory.LengthFromDna(dna, 0));
        }
    }
}