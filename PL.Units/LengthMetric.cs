using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PL.Units
{
	public class LengthMetric : Length
	{
		#region Definitions

		public enum MetricPrefix
		{
			Exa,
			Peta,
			Tera,
			Giga,
			Mega,
			Kilo,
			Hecto,
			Deca,
			Base,
			Deci,
			Centi,
			Milli,
			Micro,
			Nano,
			Pico,
			Femto,
			Atto
		}

		public static Dictionary<ushort, double> MetricPrefixFactors = new Dictionary<ushort, double>()
		{
			{ (ushort)MetricPrefix.Exa, 1e18 },
			{ (ushort)MetricPrefix.Peta, 1e15},
			{ (ushort)MetricPrefix.Tera, 1e12 },
			{ (ushort)MetricPrefix.Giga, 1e9 },
			{ (ushort)MetricPrefix.Mega, 1e6 },
			{ (ushort)MetricPrefix.Kilo, 1e3 },
			{ (ushort)MetricPrefix.Hecto, 1e2},
			{ (ushort)MetricPrefix.Deca, 1e1 },
			{ (ushort)MetricPrefix.Base, 1e0 },
			{ (ushort)MetricPrefix.Deci, 1e-1 },
			{ (ushort)MetricPrefix.Centi, 1e-2 },
			{ (ushort)MetricPrefix.Milli, 1e-3},
			{ (ushort)MetricPrefix.Micro, 1e-6 },
			{ (ushort)MetricPrefix.Nano, 1e-9 },
			{ (ushort)MetricPrefix.Pico, 1e-12 },
			{ (ushort)MetricPrefix.Femto, 1e-15},
			{ (ushort)MetricPrefix.Atto, 1e-18}
		};

		public static Dictionary<MetricPrefix, string> MetricPrefixSymbol = new Dictionary<MetricPrefix, string>()
		{
			{MetricPrefix.Exa,  "E"},
			{MetricPrefix.Peta, "P"},
			{MetricPrefix.Tera, "T"},
			{MetricPrefix.Giga, "G"},
			{MetricPrefix.Mega, "M"},
			{MetricPrefix.Kilo, "k"},
			{MetricPrefix.Hecto,"h"},
			{MetricPrefix.Deca, "da"},
			{MetricPrefix.Base, ""},
			{MetricPrefix.Deci, "d"},
			{MetricPrefix.Centi,"c"},
			{MetricPrefix.Milli,"m"},
			{MetricPrefix.Micro,"μ"},
			{MetricPrefix.Nano, "n"},
			{MetricPrefix.Pico, "p"},
			{MetricPrefix.Femto,"f"},
			{MetricPrefix.Atto, "a"}
		};

		private const string ShortUnitName = "m";

		#endregion Definitions

		#region Constructor(s)

		public LengthMetric()
		{
			Dna.UnitType = (ushort)LengthUnit.Metric;
		}

		#endregion Constructor(s)

		#region FromString

		private static string mRegularExpressionForSubUnit;

		private static string GetRegularExpressionForSubUnit()
		{
			if (mRegularExpressionForSubUnit == null)
			{
				var listOfPrefixes = string.Join("|", MetricPrefixSymbol.Values.Where(v => !string.IsNullOrEmpty(v)).ToList());
				mRegularExpressionForSubUnit = $@"(?<=( |\d))({listOfPrefixes})?{ShortUnitName}\z";
			}

			return mRegularExpressionForSubUnit;
		}

		public override Quantity FromString(string asString)
		{
			asString = PreProcessStringBeforeParsing(asString);
			var retValue = new LengthMetric
			{
				Dna = { UnitSubType = GetUnitSubTypeFromString(asString) },
				Value = GetValueFromString(asString)
			};
			return retValue;
		}

		private static ushort GetUnitSubTypeFromString(string asString)
		{
			var regularExpressionForSubUnit = GetRegularExpressionForSubUnit();

			var values = Regex.Matches(asString, regularExpressionForSubUnit);
			if (values.Count == 1 && values[0].Groups.Count == 3)
			{
				var prefixAsString = values[0].Groups[2].Value;
				var prefixKeyValue = MetricPrefixSymbol.FirstOrDefault(p => p.Value == prefixAsString);
				return Convert.ToUInt16(prefixKeyValue.Key);
			}

			throw new ArgumentException($"Cannot convert {asString} to {nameof(LengthMetric)}");
		}

		#endregion FromString

		#region Conversion

		protected override double UnitTypeBaseFactor => 1;
		protected override Dictionary<ushort, double> UnitSubTypeFactorTable => MetricPrefixFactors;

		#endregion Conversion
	}
}