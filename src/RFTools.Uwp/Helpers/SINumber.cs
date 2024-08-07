﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFTools.Uwp.Helpers
{
    public class SINumber
    {
        private static string[] prefixes =
        {
            "y", "z", "a", "f","p", "n", "µ", "m", "", "k", "M", "G", "T", "P", "E", "Z", "Y"
        };

        public static SIResult GetSI(double number, string unit = "")
        {
            int logOfTen = (int)Math.Log10(Math.Abs(number));
            if (logOfTen < -27)
            {
                return new SIResult(0, "Value is too small!");
            }

            if (logOfTen % 3 < 0)
            {
                logOfTen -= 3;
            }

            int logOfThousand = Math.Max(-8, Math.Min(logOfTen / 3, 8));

            return new SIResult(ValueFormat.ToLimitedDouble((double)number / Math.Pow(10, logOfThousand * 3)), prefixes[logOfThousand + 8] + unit);
        }
    }

    public record struct SIResult(double Value, string Unit);
}
