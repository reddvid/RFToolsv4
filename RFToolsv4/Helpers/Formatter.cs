using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization.NumberFormatting;

namespace RFToolsv4.Helpers
{
    public class Formatter
    {
        public static DecimalFormatter FrequencyNumberFormat => new() { FractionDigits = 2 };
    }
}
