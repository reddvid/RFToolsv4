using RFTools.Constants;
using RFTools.Models;
using RFTools.Contracts;

namespace RFTools.Calculators;

public class SkinDepth : Calculator, ISkinDepth
{
    /// <summary>
    /// Calculate Skin Depth
    /// </summary>
    /// <param name="resistivity">Material resistivity in <b>Ohms/cm</b></param>
    /// <param name="permeability">Material permeability (Value is usually ~1)</param>
    /// <param name="frequency">System operating frequency in <b>Hertz</b></param>
    /// <param name="precision">Number of decimals in result</param>
    public Result Calculate(
        double resistivity,
        double permeability,
        double frequency)
    {
        double denominator = Math.PI * frequency * permeability * (4 * Math.PI * Math.Pow(10, -7));

        double skinDepthInMeters = Math.Sqrt(resistivity / 100 / denominator);

        return new Result("Skin Depth", skinDepthInMeters, Units.Meter);
    }
}