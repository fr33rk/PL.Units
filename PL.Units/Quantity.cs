using System;
using System.Collections.Generic;
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

	public struct QuantityDna
	{
		public QuantityType QuantityType { get; set; }
		public ushort UnitType { get; set; }
		public ushort UnitSubType { get; set; }
	}

	public abstract class Quantity
	{
		protected QuantityDna Dna = new QuantityDna()
		{
			QuantityType = QuantityType.Unspecified,
			UnitType = 0,
			UnitSubType = 0
		};

		public QuantityType QuantityType => Dna.QuantityType;
		public ushort UnitType => Dna.UnitType;
		public ushort UnitSubType => Dna.UnitSubType;

		private double mValue;

		public double Value
		{
			get => mValue;
			set
			{
				mValue = value;
				ValueInBaseUnitSubType = ConvertValueToSubTypeBase();
				ValueInBaseUnitType = ConvertValueToUnitTypeBase();
			}
		}

		public double ValueInBaseUnitSubType { get; private set; }

		public double ValueInBaseUnitType { get; private set; }

		public abstract Quantity FromString(string asString);

		protected double GetValueFromString(string asString)
		{
			var values = Regex.Matches(asString, @"\A-?\d*[.,]?\d+(e[+-]\d+)?");
			if (values.Count == 1)
			{
				var valueAsString = values[0].Value;
				valueAsString = valueAsString.Replace(',', '.');
				return Convert.ToDouble(valueAsString, CultureInfo.InvariantCulture);
			}

			throw new ArgumentException($"Cannot convert {asString} to {nameof(Length)}");
		}

		protected string PreProcessStringBeforeParsing(string asString)
		{
			if (string.IsNullOrEmpty(asString))
				throw new ArgumentException($"Cannot convert to {GetType().Name} when string is null or empty");

			return asString.Trim();
		}

		protected abstract Dictionary<ushort, double> UnitSubTypeFactorTable { get; }

		protected abstract double UnitTypeBaseFactor { get; }

		private double ConvertValueToSubTypeBase()
		{
			return Value * UnitSubTypeFactorTable[Dna.UnitSubType];
		}

		public double ConvertValueToUnitTypeBase()
		{
			return Value * UnitSubTypeFactorTable[Dna.UnitSubType] * UnitTypeBaseFactor;
		}
	}
}