using System;
using System.Collections.Generic;

namespace PL.Units
{
    public static class QuantityFactory
    {
        private static readonly List<Quantity> Quantities = new List<Quantity>()
        {
            new LengthMetric(),
            new LengthImperial(),
            new LengthUsCustomary()
        };

        public static Quantity FromString(string asString)
        {
            foreach (var quantity in Quantities)
            {
                try
                {
                    var result = quantity.FromString(asString);
                    return result;
                }
                catch (ArgumentException)
                {
                    // Exceptions will be thrown when conversion fails.
                }
            }

            throw new ArgumentException($"Cannot convert {asString} to {nameof(Quantity)}");
        }

        public static Quantity FromDna(QuantityDna dna, double value)
        {
            switch (dna.QuantityType)
            {
                case QuantityType.Length:
                    return LengthFromDna(dna, value);

                case QuantityType.Area:
                case QuantityType.Volume:
                case QuantityType.Temperature:
                case QuantityType.Unspecified:
                default:
                    throw new ArgumentException($"Cannot create quantity from type {dna.QuantityType}");
            }
        }

        public static Quantity LengthFromDna(QuantityDna dna, double value)
        {
            switch (dna.UnitType)
            {
                case (ushort)Length.LengthUnit.Metric:
                    return new LengthMetric(dna, value);

                case (ushort)Length.LengthUnit.Imperial:
                    return new LengthImperial(dna, value);

                case (ushort)Length.LengthUnit.UsCustomary:
                    return new LengthUsCustomary(dna, value);

                default:
                    throw new ArgumentException($"Cannot create length from unit type {dna.UnitType}");
            }
        }
    }
}