using System.Globalization;
using RFTools.Constants;
using RFTools.Models;

namespace RFTools.Extensions;

public static class NumberValueExtensions
{
    static decimal ToNumberFormat(this double value, int significantUnits = Values.PrecisionUnits)
    {
        return Math.Round((decimal)value, significantUnits);
    }

    public static (decimal Value, string Unit) ToSimplifiedSiDoubleValue(this (double Value, string Unit) numberWithUnit,
        int significantUnits = Values.PrecisionUnits)
    {
        string[] prefixes =
        [
            "y", "z", "a", "f", "p", "n", "µ", "m", "", "k", "M", "G", "T", "P", "E", "Z", "Y"
        ];

        int logOfTen = (int)Math.Log10(Math.Abs(numberWithUnit.Value));

        if (logOfTen < -27)
        {
            return (0, "Value is too small!");
        }

        if (logOfTen % 3 < 0)
        {
            logOfTen -= 3;
        }

        int logOfThousand = Math.Max(-8, Math.Min(logOfTen / 3, 8));

        var formattedValue = numberWithUnit.Value / Math.Pow(10, logOfThousand * 3);
        var formattedUnit = prefixes[logOfThousand + 8] + numberWithUnit.Unit;

        return (formattedValue.ToNumberFormat(significantUnits), formattedUnit);
    }
}