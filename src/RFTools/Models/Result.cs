using RFTools.Constants;
using RFTools.Extensions;

namespace RFTools.Models;

public class Result
{
    public string Name { get; init; }
    public double Value { get; init; }
    public string? Unit { get; init; }

    public Result(string name, double value, string? unit = Units.None)
    {
        var valueInSiUnit = (value, unit!).ToSimplifiedSiDoubleValue();

        var isEngineeringUnit = unit switch
        {
            Units.Feet or Units.Miles or Units.Kelvin or Units.Decibel or Units.DecibelIsotropic
                or Units.DecibelMilliwatts or Units.Degree or Units.DegreeCelsius or Units.DegreeFahrenheit
                or Units.NauticalMiles => true,
            _ => false
        };

        Name = name;
        Value = isEngineeringUnit ? value : valueInSiUnit.Value;
        Unit = isEngineeringUnit ? unit : valueInSiUnit.Unit;

        Console.WriteLine($"{Value} {Unit}");
    }
}