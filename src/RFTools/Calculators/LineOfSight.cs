using RFTools.Constants;
using RFTools.Contracts;
using RFTools.Enums;
using RFTools.Models;

namespace RFTools.Calculators;

public class LineOfSight : Calculator, ILineOfSight
{
    /// <summary>
    /// Calculate the horizon distance from an antenna. And radio horizon.
    /// </summary>
    /// <param name="height">Antenna height</param>
    /// <param name="type">Input value in Meters or Feet</param>
    /// <returns>The Line of Sight distance and Radio horizon (service range) in kilometers.</returns>
    public List<Result> Calculate(double height, Input type)
    {
        if (type == Input.Feet)
        {
            // Convert to meters
            height = height / 3.28;
        }
        
        var losDistance = 3.57 * Math.Sqrt(height);
        var radioHorizon = 4.12 * Math.Sqrt(height);

        return
        [
            new Result("Line of Sight distance", losDistance, Units.Meter),
            new Result("Radio Horizon (Service Range)", radioHorizon, Units.Meter),
        ];
    }
}