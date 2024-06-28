using RFTools.Enums;
using RFTools.Models;

namespace RFTools.Calculators;

public interface ILineOfSight
{
    /// <summary>
    /// Calculate the horizon distance from an antenna. And radio horizon.
    /// </summary>
    /// <param name="height">Antenna height</param>
    /// <param name="type">Input value in Meters or Feet</param>
    /// <returns>The Line of Sight distance and Radio horizon (service range) in kilometers.</returns>
    List<Result> Calculate(double height, Input type);
}