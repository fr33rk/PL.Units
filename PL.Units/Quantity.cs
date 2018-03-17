using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PL.Units
{
	public enum QuantityType
	{
		Unspecified,
		Length,
		Area,
		Volume,
		Temperature,
	}

	public abstract class Quantity
	{
		public QuantityType QuantityType { get; protected set; } = QuantityType.Unspecified;
		public ushort UnitType { get; protected set; } = 0;
		public ushort UnitSubType { get; protected set; }= 0;

		public abstract Quantity FromString(string asString);

		protected double GetValueFromString(string asString)
		{
			var values = Regex.Matches(asString, @"\A-?\d*[.,]?\d+");
			if (values.Count == 1)
			{
				var valueAsString = values[0].Value;
				valueAsString.Replace(',', '.');
				return Convert.ToDouble(valueAsString, CultureInfo.InvariantCulture);
			}

			throw new ArgumentException($"Cannot convert {asString} to {nameof(Length)}");
		}

		protected string PreProcessStringBeforeParsing(string asString)
		{
			if (string.IsNullOrEmpty(asString))
				throw new ArgumentException($"Cannot convert to {this.GetType().Name} when string is null or empty");

			return asString.Trim();
		}

	}
}