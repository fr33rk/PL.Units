namespace PL.Units
{
    public struct QuantityDna
    {
        public QuantityType QuantityType { get; set; }
        public ushort UnitType { get; set; }
        public ushort UnitSubType { get; set; }
        public ushort Precision { get; set; }

        public int ToInt()
        {
            var result = (int)QuantityType * 1000000;
            result += UnitType * 10000;
            result += UnitSubType * 100;
            result += Precision;
            return result;
        }

        public static QuantityDna FromInt(uint fromValue)
        {
            var result = new QuantityDna
            {
                Precision = (ushort) (fromValue % 100),
                UnitSubType = (ushort) ((fromValue /= 100) % 100),
                UnitType = (ushort) ((fromValue /= 100) % 100),
                QuantityType = (QuantityType) (fromValue / 100)
            };

            return result;
        }

    }
}