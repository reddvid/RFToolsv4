using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFTools.Uwp.Helpers
{
    public class ValueFormat
    {
        public static double ToLimitedDouble(double value, int maxDigits = 3)
        {
            string newValue = maxDigits switch
            {
                0 => value.ToString("N0"),
                1 => value.ToString("N1"),
                2 => value.ToString("N2"),
                3 => value.ToString("N3"),
                4 => value.ToString("N4"),
                _ => value.ToString("N"),
            };

            return Convert.ToDouble(newValue);
        }
    }
}
