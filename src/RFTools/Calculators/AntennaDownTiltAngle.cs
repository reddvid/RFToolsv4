using RFTools.Constants;
using RFTools.Contracts;
using RFTools.Enums;
using RFTools.Models;

namespace RFTools.Calculators;

public class AntennaDownTiltAngle : Calculator, IAntennaDownTiltAngle
{
    /// <summary>
    /// Calculate the angle of antenna down-tilt.
    /// </summary>
    /// <param name="heightType">The unit of height for the antennas in Feet or Meters</param>
    /// <param name="baseAntennaHeight">The first (base) antenna height.</param>
    /// <param name="remoteAntennaHeight">The second (remote) antenna height.</param>
    /// <param name="distance">THe distance value</param>
    /// <param name="distanceType">The unit of distance in Miles or Kilometers</param>
    /// <returns></returns>
    public Result Calculate(Input heightType, double baseAntennaHeight, double remoteAntennaHeight, double distance,
        Input distanceType)
    {
        if (heightType == Input.Meters)
        {
            // Convert to Feet
            baseAntennaHeight = baseAntennaHeight * 3.28;
            remoteAntennaHeight = remoteAntennaHeight * 3.28;
        }

        if (distanceType == Input.Kilometers)
        {
            distance = distance / 1.609;
        }

        var angle = Math.Atan((baseAntennaHeight - remoteAntennaHeight) / (distance * 5280)) * 57.2957;

        return new Result("Down-tilt Angle", angle, Units.Degree);
    }
}