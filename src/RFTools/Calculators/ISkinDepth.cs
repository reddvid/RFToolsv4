using RFTools.Models;

namespace RFTools.Calculators;

public interface ISkinDepth
{
    /// <summary>
    /// Calculate Skin Depth
    /// </summary>
    /// <param name="resistivity">Material resistivity in <b>microhms/cm (µΩ/cm)</b></param>
    /// <param name="permeability">Material permeability (Value is usually ~1)</param>
    /// <param name="frequency">System operating frequency in <b>Hertz</b></param>
    Result Calculate(double resistivity,
        double permeability,
        double frequency);
}