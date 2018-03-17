using NUnit.Framework;

namespace PL.Units.Tests
{
	[TestFixture]
	public class LengthTests
	{
		[Test]
		[TestCase("1 m", 8, 1)]
		[TestCase("1 dm", 9, 0.01)]
		[TestCase("1 cm", 10, 0.01)]
		public void Length_FromString_CorrectLengthObject(string asString, int expectedPrefix, double expectedBaseValue)
		{
			//// Arrange and act
			//var unitUnderTest = Length.FromString(asString);

			//// Assert
			//Assert.That(unitUnderTest.Prefix, Is.EqualTo(expectedPrefix));
			//Assert.That(unitUnderTest.Value, Is.EqualTo(expectedBaseValue));
		}
	}
}