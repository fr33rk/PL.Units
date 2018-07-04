using System;
using System.Collections.Generic;

namespace PL.Units
{
	public class VolumeMetric : Volume
	{
		public VolumeMetric()
		{
			Dna.UnitType = (ushort)VolumeUnit.Metric;
		}

		public VolumeMetric(QuantityDna dna, double value)
			: base(dna, value)
		{
			if (dna.UnitType != (ushort)VolumeUnit.Metric)
				throw new ArgumentException($"Invalid DNA. Expected unit type {VolumeUnit.Metric}");
		}

		private static string mRegularExpressionForSubUnit;

		protected override string GetRegularExpressionForSubUnit()
		{
			return mRegularExpressionForSubUnit
			       ?? (mRegularExpressionForSubUnit 
				       = QuantityMetric.GetRegularExpressionForSubUnit($"({SubTypeToShortString}|m3)"));
		}

		protected override ushort GetUnitSubTypeFromString(string asString)
		{
			return QuantityMetric.GetUnitSubTypeFromString(asString, GetRegularExpressionForSubUnit());
		}

		protected override string PrefixToString => QuantityMetric.MetricPrefixSymbol[(QuantityMetric.MetricPrefix)UnitSubType];
		protected override string SubTypeToShortString => "m³";
		protected override Dictionary<ushort, double> UnitSubTypeFactorTable => QuantityMetric.MetricPrefixFactors;
		protected override double UnitTypeBaseFactor => 1;

		protected override Quantity CreateInstanceForClone()
		{
			return new VolumeMetric();
		}
	}
}