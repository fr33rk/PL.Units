using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PL.Units
{
	public class LengthImperial : Length
	{
		#region Definitions

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

		public static Dictionary<ushort, double> ImperialLengthFactors = new Dictionary<ushort, double>()
		{
			{ (ushort)ImperialLengthUnit.Thou, 1 / 12000d },
			{ (ushort)ImperialLengthUnit.Inch, 1/ 12d },
			{ (ushort)ImperialLengthUnit.Foot, 1 },
			{ (ushort)ImperialLengthUnit.Yard, 3 },
			{ (ushort)ImperialLengthUnit.Chain, 66 },
			{ (ushort)ImperialLengthUnit.Furlong, 660 },
			{ (ushort)ImperialLengthUnit.Mile, 5280}
		};

		public static Dictionary<ImperialLengthUnit, string[]> ImperialSymbols =
			new Dictionary<ImperialLengthUnit, string[]>()
			{
				{ ImperialLengthUnit.Thou,    new[] {"thou"   , "th"} },
				{ ImperialLengthUnit.Inch,    new[] {"inch"   , "in"} },
				{ ImperialLengthUnit.Foot,    new[] {"foot"   , "ft"} },
				{ ImperialLengthUnit.Yard,    new[] {"yard"   , "yd"} },
				{ ImperialLengthUnit.Chain,   new[] {"chain"  , "ch"} },
				{ ImperialLengthUnit.Furlong, new[] {"furlong", "fur"} },
				{ ImperialLengthUnit.Mile,    new[] {"mile"   , "ml"} }
			};

		#endregion Definitions

		#region Constructors

		public LengthImperial()
		{
			Dna.UnitType = (ushort)LengthUnit.Imperial;
		}

		public LengthImperial(QuantityDna dna, double value)
			: base(dna, value)
		{
			if (dna.UnitType != (ushort)LengthUnit.Imperial)
				throw new ArgumentException($"Invalid DNA. Expected unit type {LengthUnit.Imperial}");
		}

		#endregion Constructors

		#region ToString

		private static string mRegularExpressionForSubUnit;

		protected override string GetRegularExpressionForSubUnit()
		{
			if (mRegularExpressionForSubUnit == null)
			{
				var listOfSymbols = new List<string>();
				foreach (var imperialSymbol in ImperialSymbols)
				{
					listOfSymbols.AddRange(imperialSymbol.Value);
				}

				var listOfSymbolsAsString = string.Join("|", listOfSymbols);
				mRegularExpressionForSubUnit = $@"(?<=( |\d))({listOfSymbolsAsString})?\z";
			}

			return mRegularExpressionForSubUnit;
		}

		protected override ushort GetUnitSubTypeFromString(string asString)
		{
			var regularExpressionForSubUnit = GetRegularExpressionForSubUnit();

			var values = Regex.Matches(asString, regularExpressionForSubUnit);
			if (values.Count == 1 && values[0].Groups.Count == 3)
			{
				var prefixAsString = values[0].Groups[2].Value;
				var prefixKeyValue = ImperialSymbols.FirstOrDefault(p => p.Value.Contains(prefixAsString));
				return Convert.ToUInt16(prefixKeyValue.Key);
			}

			throw new ArgumentException($"Cannot convert {asString} to {GetType()}");
		}

		protected override string PrefixToString => ImperialSymbols[(ImperialLengthUnit)UnitSubType][1];

		#endregion ToString

		#region Conversion

		protected override Dictionary<ushort, double> UnitSubTypeFactorTable => ImperialLengthFactors;

		protected override double UnitTypeBaseFactor => 0.3048d;

		#endregion Conversion

		protected override Quantity CreateInstanceForClone()
		{
			return new LengthImperial();
		}
	}
}