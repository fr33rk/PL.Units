using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PL.Units
{
    public abstract class Quantity
    {
        #region Constructor(s)

        protected Quantity()
        {
            // Nothing additional to do here
        }

        protected Quantity(QuantityDna dna, double value)
        {
            Dna = dna;
            Value = value;
        }

        #endregion Constructor(s)

        #region DNA

        protected QuantityDna Dna = new QuantityDna()
        {
            QuantityType = QuantityType.Unspecified,
            UnitType = 0,
            UnitSubType = 0,
            Precision = 0
        };

        public QuantityType QuantityType => Dna.QuantityType;
        public ushort UnitType => Dna.UnitType;
        public ushort UnitSubType => Dna.UnitSubType;
        public ushort Precision => Dna.Precision;

        #endregion DNA

        #region Value

        private double mValue;

        public double Value
        {
            get => mValue;
            set
            {
                mValue = value;
                ValueInBaseUnitSubType = ConvertValueToSubTypeBase();
                mValueInBaseUnitType = ConvertValueToUnitTypeBase();
            }
        }

        public double ValueInBaseUnitSubType
        {
            get;
            private set;
        }

        private double mValueInBaseUnitType;

        public double ValueInBaseUnitType
        {
            get => mValueInBaseUnitType;
            private set
            {
                mValueInBaseUnitType = value;
                mValue = (mValueInBaseUnitType / UnitTypeBaseFactor) / UnitSubTypeFactorTable[Dna.UnitSubType];
            }
        }

        #endregion Value

        #region From String

        public virtual Quantity FromString(string asString)
        {
            var retValue = CreateInstanceForClone();

            asString = PreProcessStringBeforeParsing(asString);

            retValue.Dna.Precision = GetPrecisionFromString(asString);
            retValue.Dna.UnitSubType = GetUnitSubTypeFromString(asString);

            retValue.Value = GetValueFromString(asString);

            return retValue;
        }

        protected abstract string GetRegularExpressionForSubUnit();

        protected double GetValueFromString(string asString)
        {
            var values = Regex.Matches(asString, @"\A-?\d*[.,]?\d+(e[+-]\d+)?");
            if (values.Count != 1) throw new ArgumentException($"Cannot convert {asString} to {nameof(Length)}");

            var valueAsString = values[0].Value;
            valueAsString = valueAsString.Replace(',', '.');
            return Convert.ToDouble(valueAsString, CultureInfo.InvariantCulture);
        }

        internal static ushort GetPrecisionFromString(string asString)
        {
            var decimals = Regex.Matches(asString, @"(?<=[.,])\d+");

            if (decimals.Count == 0)
                return 0;
            if (decimals.Count == 1)
                return (ushort)decimals[0].Length;

            throw new ArgumentException($"Cannot determine no of decimals for {asString}");
        }

        protected abstract ushort GetUnitSubTypeFromString(string asString);

        protected string PreProcessStringBeforeParsing(string asString)
        {
            if (string.IsNullOrEmpty(asString))
                throw new ArgumentException($"Cannot convert to {GetType().Name} when string is null or empty");

            return asString.Trim();
        }

        #endregion From String

        #region To String

        public override string ToString()
        {
            return $"{Value.ToString($"F{Dna.Precision}")}{PrefixToString}{SubTypeToString}";
        }

        protected abstract string PrefixToString { get; }

        protected virtual string SubTypeToString => "m";

        #endregion To String

        protected abstract Dictionary<ushort, double> UnitSubTypeFactorTable { get; }

        protected abstract double UnitTypeBaseFactor { get; }

        private double ConvertValueToSubTypeBase()
        {
            return Value * UnitSubTypeFactorTable[Dna.UnitSubType];
        }

        public double ConvertValueToUnitTypeBase()
        {
            return Value * UnitSubTypeFactorTable[Dna.UnitSubType] * UnitTypeBaseFactor;
        }

        protected abstract Quantity CreateInstanceForClone();

        private Quantity Clone()
        {
            var clone = CreateInstanceForClone();
            clone.Dna = Dna;
            clone.Value = Value;
            return clone;
        }

        #region Operators

        public static Quantity operator +(Quantity x, Quantity y)
        {
            var result = x.Clone();
            result.ValueInBaseUnitType += y.ValueInBaseUnitType;
            return result;
        }

        public static Quantity operator +(Quantity x, double y)
        {
            return null;
        }

        public static Quantity operator -(Quantity x, Quantity y)
        {
            return null;
        }

        public static Quantity operator -(Quantity x, double y)
        {
            return null;
        }

        #endregion Operators
    }
}