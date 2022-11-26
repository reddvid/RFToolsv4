using RFToolsv4.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFToolsv4.Models
{
    public class Result
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public string Units { get; set; }

        public Result(string name, double value, string units, bool isSI = false, bool isEngineering = false)
        {
            var valueSI = SINumber.GetSI(value, units);

            Name = name;
            Value = isEngineering ? value : ValueFormat.ToLimitedDouble(isSI ? valueSI.Value : value);
            Units = isSI ? valueSI.Unit : units;
        }
    }
}
