using RFTools.Enums;
using RFTools.Models;

namespace RFTools.Calculators;

public interface IAntennaDownTiltAngle
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
    Result Calculate(Input heightType, double baseAntennaHeight, double remoteAntennaHeight, double distance,
        Input distanceType);
}