using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Units
{
	public static class QuantityFactory
	{
		private static List<Quantity> mQuantities = new List<Quantity>()
		{
			new LengthMetric(),
			new LengthImperial()
		};

		public static Quantity FromString(string asString)
		{
			foreach (var quantity in mQuantities)
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
	}


}
