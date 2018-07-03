using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace PL.Units
{
	public class LengthMetric : Length
	{
		#region Constructor(s)

		public LengthMetric()
		{
			Dna.UnitType = (ushort)LengthUnit.Metric;
		}

		public LengthMetric(QuantityDna dna, double value)
			: base(dna, value)
		{
			if (dna.UnitType != (ushort)LengthUnit.Metric)
				throw new ArgumentException($"Invalid DNA. Expected unit type {LengthUnit.Metric}");
		}

		#endregion Constructor(s)

		#region FromString

		private static string mRegularExpressionForSubUnit;

		protected override string GetRegularExpressionForSubUnit()
		{
			return mRegularExpressionForSubUnit 
			       ?? (mRegularExpressionForSubUnit = QuantityMetric.GetRegularExpressionForSubUnit(SubTypeToShortString));
		}

		protected override ushort GetUnitSubTypeFromString(string asString)
		{
			return QuantityMetric.GetUnitSubTypeFromString(asString, GetRegularExpressionForSubUnit());
		}

		#endregion FromString

		protected override string PrefixToString => QuantityMetric.MetricPrefixSymbol[(QuantityMetric.MetricPrefix)UnitSubType];

	    protected override string SubTypeToShortString => "m";

        #region Conversion

        protected override double UnitTypeBaseFactor => 1;
		protected override Dictionary<ushort, double> UnitSubTypeFactorTable => QuantityMetric.MetricPrefixFactors;

		#endregion Conversion

		protected override Quantity CreateInstanceForClone()
		{
			return new LengthMetric();
		}
	}
}