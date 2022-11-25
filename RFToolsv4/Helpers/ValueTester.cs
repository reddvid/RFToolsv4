using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFToolsv4.Helpers
{
    public class ValueTester
    {
        public static bool NaNTest(double[] inputs)
        {
            foreach (double input in inputs)
            {
                if (input == 0 || double.IsNaN(input)) return false;
                else continue;
            }
            return true;
        }

        public static bool NaNTest(double input)
        {
            if (input == 0 || double.IsNaN(input)) return false;
            return true;
        }


    }
}
