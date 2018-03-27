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
			Dna.QuantityType = QuantityType.Length;
		}
	}
}