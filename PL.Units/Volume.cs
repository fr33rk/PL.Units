using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Units
{
    public abstract class Volume : Quantity
    {
	    public enum VolumeUnit
	    {
			Metric,
			Liter
	    }

	    public Dictionary<VolumeUnit, double> VolumeUnitFactors = new Dictionary<VolumeUnit, double>
	    {
		    {VolumeUnit.Metric, 1}
	    };

	    public Volume()
	    {
		    Dna.QuantityType = QuantityType.Volume;
	    }

	    public Volume(QuantityDna dna, double value)
		    : base(dna, value)
	    {
		    if (Dna.QuantityType != QuantityType.Volume)
			    throw new ArgumentException($"Invalid DNA. Expected quantity type {QuantityType.Volume}");
		}
    }
}
