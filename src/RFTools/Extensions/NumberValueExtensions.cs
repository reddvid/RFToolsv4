using RFTools.Models;

namespace RFTools.Extensions;

public static class NumberValueExtensions
{
    public static double ToLimitedDouble(this double value, int maxDigits = 3)
    {
        var newValue = maxDigits switch
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

    public static (double Value, string Unit) SimplifyToSiUnit(this (double Value, string Unit) numberWithUnit)
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
        
        return (formattedValue.ToLimitedDouble(), formattedUnit);
    }
}