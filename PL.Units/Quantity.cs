using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PL.Units
{
    public enum QuantityType
    {
        Unspecified,
        Length,
        Area,
        Volume,
        Temperature,
    }

    public struct QuantityDna
    {
        public QuantityType QuantityType { get; set; }
        public ushort UnitType { get; set; }
        public ushort UnitSubType { get; set; }
        public ushort Precision { get; set; }
    }

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
            UnitSubType = 0
        };

        public QuantityType QuantityType => Dna.QuantityType;
        public ushort UnitType => Dna.UnitType;
        public ushort UnitSubType => Dna.UnitSubType;

        #endregion DNA

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

        public abstract Quantity FromString(string asString);

        protected double GetValueFromString(string asString)
        {
            var values = Regex.Matches(asString, @"\A-?\d*[.,]?\d+(e[+-]\d+)?");
            if (values.Count == 1)
            {
                var valueAsString = values[0].Value;
                valueAsString = valueAsString.Replace(',', '.');
                return Convert.ToDouble(valueAsString, CultureInfo.InvariantCulture);
            }

            throw new ArgumentException($"Cannot convert {asString} to {nameof(Length)}");
        }

        protected string PreProcessStringBeforeParsing(string asString)
        {
            if (string.IsNullOrEmpty(asString))
                throw new ArgumentException($"Cannot convert to {GetType().Name} when string is null or empty");

            return asString.Trim();
        }

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