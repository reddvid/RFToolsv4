using RFTools.Constants;
using RFTools.Extensions;

namespace RFTools.Models;

public class Result
{
    public string Name { get; init; }
    public double Value { get; init; }
    public string Unit { get; init; }

    public Result(string name, double value, string unit, int significantUnits = NumberConstants.SignificantUnits, bool isEngineeringUnit = false)
    {
        var valueInSiUnit = (value, unit).SimplifyToSiUnit();

        Name = name;
        Value = isEngineeringUnit ? value : valueInSiUnit.Value;
        Unit = isEngineeringUnit ? unit : valueInSiUnit.Unit;
    }
}