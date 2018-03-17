using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PL.Units
{
	public class LengthMetric : Length
	{
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

		public static Dictionary<MetricPrefix, double> MetricPrefixFactors = new Dictionary<MetricPrefix, double>()
		{
			{ MetricPrefix.Exa, 10e18 },
			{ MetricPrefix.Peta, 10e15},
			{ MetricPrefix.Tera, 10e12 },
			{ MetricPrefix.Giga, 10e9 },
			{ MetricPrefix.Mega, 10e6 },
			{ MetricPrefix.Kilo, 10e3 },
			{ MetricPrefix.Hecto, 10e2},
			{ MetricPrefix.Deca, 10e1 },
			{ MetricPrefix.Base, 10e0 },
			{ MetricPrefix.Deci, 10e-1 },
			{ MetricPrefix.Centi, 10e-2 },
			{ MetricPrefix.Milli, 10e-3},
			{ MetricPrefix.Micro, 10e-6 },
			{ MetricPrefix.Nano, 10e-9 },
			{ MetricPrefix.Pico, 10e-12 },
			{ MetricPrefix.Femto, 10e-15},
			{ MetricPrefix.Atto, 10e-18}
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

		public LengthMetric()
		{
			UnitType = (ushort)LengthUnit.Metric;
		}

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
				UnitSubType = GetUnitSubTypeFromString(asString),
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


		public double ToUnitBaseValue()
		{
			return double.NaN;
		}
	}
}