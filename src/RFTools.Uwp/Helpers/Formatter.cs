using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization.NumberFormatting;

namespace RFTools.Uwp.Helpers
{
    public class NumFormatter
    {
        public static DecimalFormatter FrequencyNumberFormat => new() { FractionDigits = 2 };
    }
}
