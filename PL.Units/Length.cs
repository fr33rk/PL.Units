using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace PL.Units
{
	public abstract class Length : Quantity
	{
		public enum LengthUnit
		{
			Metric,
			Imperial,
			UsCustomary
		}

		public Dictionary<LengthUnit, double> LengthUnitFactors = new Dictionary<LengthUnit, double>()
		{
			{ LengthUnit.Metric, 1 },
			{ LengthUnit.Imperial, 0.3048},
			{ LengthUnit.UsCustomary, 0.3048 }
		};

		public enum ImperialLengthUnit
		{
			Thou,
			Inch,
			Foot,
			Yard,
			Chain,
			Furlong,
			Mile
		}

		public static Dictionary<ImperialLengthUnit, double> ImperialLengthFactors  = new Dictionary<ImperialLengthUnit, double>()
		{
			{ ImperialLengthUnit.Thou, 1 / 12000d },
			{ ImperialLengthUnit.Inch, 1/ 12d },
			{ ImperialLengthUnit.Foot, 1 },
			{ ImperialLengthUnit.Yard, 3 },
			{ ImperialLengthUnit.Chain, 66 },
			{ ImperialLengthUnit.Furlong, 660 },
			{ ImperialLengthUnit.Mile, 5280}
		};

		public static Dictionary<ImperialLengthUnit, string[]> ImperialSymbols =
			new Dictionary<ImperialLengthUnit, string[]>()
			{
				{ ImperialLengthUnit.Thou, new[] {"thou", "th"} },
				{ ImperialLengthUnit.Inch, new[] {"inch", "in"} },
				{ ImperialLengthUnit.Foot, new[] {"foot", "ft"} },
				{ ImperialLengthUnit.Yard, new[] {"yard", "yd"} },
				{ ImperialLengthUnit.Chain, new[] {"chain", "ch"} },
				{ ImperialLengthUnit.Furlong, new[] {"furlong", "fur"} },
				{ ImperialLengthUnit.Mile, new[] { "mile", "ml"} }
			};

		public enum UsCustomaryLengthUnit
		{
			Point,
			Pica,
			Inch,
			Foot,
			Yard,
			Mile
		}

		public static Dictionary<UsCustomaryLengthUnit, double> UsCustomaryLengthFactors =
			new Dictionary<UsCustomaryLengthUnit, double>()
			{
				{ UsCustomaryLengthUnit.Point,1 / 864d},
				{ UsCustomaryLengthUnit.Pica, 1 / 72d },
				{ UsCustomaryLengthUnit.Inch, 1 / 12d },
				{ UsCustomaryLengthUnit.Foot, 1 },
				{ UsCustomaryLengthUnit.Yard, 3 },
				{ UsCustomaryLengthUnit.Mile, 5280}
			};

		public static Dictionary<UsCustomaryLengthUnit, string[]> UsCustomarySymbols =
			new Dictionary<UsCustomaryLengthUnit, string[]>()
			{
				{UsCustomaryLengthUnit.Point, new[] {"point", "p"}},
				{UsCustomaryLengthUnit.Pica, new[] {"pica", "pi"}},
				{UsCustomaryLengthUnit.Inch, new[] {"inch", "in", "\""}},
				{UsCustomaryLengthUnit.Foot, new[] {"foot", "ft", "'"}},
				{UsCustomaryLengthUnit.Yard, new[] {"yard", "yd"}},
				{UsCustomaryLengthUnit.Mile, new[] {"mile", "ml"}}
			};



		private double mValue;

		public double Value
		{
			get => mValue;
			set
			{
				mValue = value;
				//mBaseValue = Convert(mValue, )
			}
		}

		private double mBaseValue;

		public double BaseValue
		{
			get => mBaseValue;
			set
			{
				mBaseValue = value;
			}
		}

		public ushort Prefix { get; private set; }

		private const string mShortUnitName = "m";

		public override string ToString()
		{
			return null;
		}

		//public static Length FromString(string asString)
		//{
		//	var retValue = new Length();
		//	asString = asString.Trim();

		//	retValue.Value = GetValueFromString(asString);

		//	//var listOfPrefixes = string.Join("|", MetricPrefixSymbol.SelectMany(p => p.Value).ToList());
		//	//var regexExpression = $"({listOfPrefixes})?{mShortUnitName}";
		//	//values = Regex.Matches(asString, regexExpression);
		//	//if (values.Count == 1 && values[0].Groups.Count == 2)
		//	//{
		//	//	var prefixAsString = values[0].Groups[1].Value;
		//	//	var prefixKeyValue = MetricPrefixSymbol.FirstOrDefault(p => p.Value == prefixAsString);
		//	//	if (prefixKeyValue.Value == prefixAsString)
		//	//	{
		//	//		retValue.Prefix = Convert.ToUInt16(prefixKeyValue.Key);
		//	//	}
		//	//	else
		//	//	{
		//	//		throw new ArgumentException($"Cannot convert {asString} to {nameof(Length)}");
		//	//	}
		//	//}
		//	//else
		//	//{
		//	//	throw new ArgumentException($"Cannot convert {asString} to {nameof(Length)}");
		//	//}

		//	return retValue;
		//}



		public static Length operator +(Length x, Length y)
		{
			return null;
		}

		public static Length operator +(Length x, double y)
		{
			return null;
		}

		public static Length operator -(Length x, Length y)
		{
			return null;
		}

		public static Length operator -(Length x, double y)
		{
			return null;
		}

		protected Length()
		{
			QuantityType = QuantityType.Length;
		}
	}
}