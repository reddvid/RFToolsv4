using RFTools.Constants;
using RFTools.Extensions;

namespace RFTools.Models;

public class Result
{
    public string Name { get; init; }
    public decimal Value { get; init; }
    public string? Unit { get; init; }

    public Result(string name, double value, string? unit = Units.None, int precision = Values.PrecisionUnits,
        bool isEngineeringUnit = false)
    {
        var valueInSiUnit = (value, unit!).ToSimplifiedSiDoubleValue(precision);

        Name = name;
        Value = isEngineeringUnit ? (decimal)value : valueInSiUnit.Value;
        Unit = isEngineeringUnit ? unit : valueInSiUnit.Unit;
    }
}