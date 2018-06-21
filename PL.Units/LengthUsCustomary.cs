using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PL.Units
{
	public class LengthUsCustomary : Length
	{
		#region Definitions

		public enum UsCustomaryLengthUnit
		{
			Point,
			Pica,
			Inch,
			Foot,
			Yard,
			Mile
		}

		public static Dictionary<ushort, double> UsCustomaryLengthFactors =
			new Dictionary<ushort, double>()
			{
				{ (ushort)UsCustomaryLengthUnit.Point,1 / 864d},
				{ (ushort)UsCustomaryLengthUnit.Pica, 1 / 72d },
				{ (ushort)UsCustomaryLengthUnit.Inch, 1 / 12d },
				{ (ushort)UsCustomaryLengthUnit.Foot, 1 },
				{ (ushort)UsCustomaryLengthUnit.Yard, 3 },
				{ (ushort)UsCustomaryLengthUnit.Mile, 5280}
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

		#endregion Definitions

		public LengthUsCustomary()
		{
			Dna.UnitType = (ushort)LengthUnit.UsCustomary;
		}

		public LengthUsCustomary(QuantityDna dna, double value)
			: base(dna, value)
		{
			if (dna.UnitType != (ushort)LengthUnit.UsCustomary)
				throw new ArgumentException($"Invalid DNA. Expected unit type {LengthUnit.UsCustomary}");
		}

		//public override Quantity FromString(string asString)
		//{
		//	asString = PreProcessStringBeforeParsing(asString);
		//	var retValue = new LengthUsCustomary()
		//	{
		//		Dna = { UnitSubType = GetUnitSubTypeFromString(asString) },
		//		Value = GetValueFromString(asString)
		//	};
		//	return retValue;
		//}

		protected override ushort GetUnitSubTypeFromString(string asString)
		{
			var regularExpressionForSubUnit = GetRegularExpressionForSubUnit();

			var values = Regex.Matches(asString, regularExpressionForSubUnit);
			if (values.Count == 1 && values[0].Groups.Count == 3)
			{
				var prefixAsString = values[0].Groups[2].Value;
				var prefixKeyValue = UsCustomarySymbols.FirstOrDefault(p => p.Value.Contains(prefixAsString));
				return Convert.ToUInt16(prefixKeyValue.Key);
			}

			throw new ArgumentException($"Cannot convert {asString} to {GetType()}");
		}

		private static string mRegularExpressionForSubUnit;

		protected override string GetRegularExpressionForSubUnit()
		{
			if (mRegularExpressionForSubUnit == null)
			{
				var listOfSymbols = new List<string>();
				foreach (var imperialSymbol in UsCustomarySymbols)
				{
					listOfSymbols.AddRange(imperialSymbol.Value);
				}

				var listOfSymbolsAsString = string.Join("|", listOfSymbols);
				mRegularExpressionForSubUnit = $@"(?<=( |\d))({listOfSymbolsAsString})?\z";
			}

			return mRegularExpressionForSubUnit;
		}


		protected override Dictionary<ushort, double> UnitSubTypeFactorTable => UsCustomaryLengthFactors;
		protected override double UnitTypeBaseFactor => 0.3048d;

	    protected override Quantity CreateInstanceForClone()
	    {
	        return new LengthUsCustomary();
	    }
	}
}