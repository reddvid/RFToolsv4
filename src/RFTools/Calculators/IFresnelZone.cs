using RFTools.Models;

namespace RFTools.Calculators;

public interface IFresnelZone
{
    /// <summary>
    /// Calculate Fresnel Zone radius of antenna links
    /// </summary>
    /// <param name="distance">The distance between antennas in kilometers</param>
    /// <param name="frequency">The link frequency in GHz</param>
    /// <param name="isObstructed">Boolean value whether the link is obstructed</param>
    /// <param name="obstructionDistance">The obstruction distance from one of the antenna in kilometers</param>
    /// <returns>Returns List&lt;Result&gt; of Max Radius Fresnel Zone, 60 percent clearance radius, and Earth curvature height</returns>
    List<Result> Calculate(double distance, double frequency, bool isObstructed = false,
        double obstructionDistance = 0);
}