using System;
using System.Collections.Generic;

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

		protected Length()
		{
			Dna.QuantityType = QuantityType.Length;
		}

		protected Length(QuantityDna dna, double value)
			: base(dna, value)
		{
			if (Dna.QuantityType != QuantityType.Length)
				throw new ArgumentException($"Invalid DNA. Expected quantity type {QuantityType.Length}");
		}
	}
}