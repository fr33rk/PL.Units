using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PL.Units
{
	public class QuantityMetric
	{
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
			{ (ushort) MetricPrefix.Exa, 1e18 },
			{ (ushort) MetricPrefix.Peta, 1e15},
			{ (ushort) MetricPrefix.Tera, 1e12 },
			{ (ushort) MetricPrefix.Giga, 1e9 },
			{ (ushort) MetricPrefix.Mega, 1e6 },
			{ (ushort) MetricPrefix.Kilo, 1e3 },
			{ (ushort) MetricPrefix.Hecto, 1e2},
			{ (ushort) MetricPrefix.Deca, 1e1 },
			{ (ushort) MetricPrefix.Base, 1e0 },
			{ (ushort) MetricPrefix.Deci, 1e-1 },
			{ (ushort) MetricPrefix.Centi, 1e-2 },
			{ (ushort) MetricPrefix.Milli, 1e-3},
			{ (ushort) MetricPrefix.Micro, 1e-6 },
			{ (ushort) MetricPrefix.Nano, 1e-9 },
			{ (ushort) MetricPrefix.Pico, 1e-12 },
			{ (ushort) MetricPrefix.Femto, 1e-15},
			{ (ushort) MetricPrefix.Atto, 1e-18}
		};

		public static string GetRegularExpressionForSubUnit(string subTypeToShortString)
		{
			var listOfPrefixes = string.Join("|", QuantityMetric.MetricPrefixSymbol.Values.Where(v => !string.IsNullOrEmpty(v)).ToList());
			return $@"(?<=( |\d))({listOfPrefixes})?{subTypeToShortString}\z";
		}

		public static ushort GetUnitSubTypeFromString(string asString, string regularExpressionForSubUnit)
		{
			var values = Regex.Matches(asString, regularExpressionForSubUnit);
			if (values.Count == 1 && values[0].Groups.Count == 3)
			{
				var prefixAsString = values[0].Groups[2].Value;
				var prefixKeyValue = MetricPrefixSymbol.FirstOrDefault(p => p.Value == prefixAsString);
				return Convert.ToUInt16(prefixKeyValue.Key);
			}

			throw new ArgumentException($"Cannot convert {asString} to {nameof(LengthMetric)}");
		}

	}
}