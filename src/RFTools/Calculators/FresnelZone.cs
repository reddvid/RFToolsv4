using RFTools.Constants;
using RFTools.Models;

namespace RFTools.Calculators;

public class FresnelZone : Calculator, IFresnelZone
{
    /// <summary>
    /// Calculate Fresnel Zone radius of antenna links
    /// </summary>
    /// <param name="distance">The distance between antennas in kilometers</param>
    /// <param name="frequency">The link frequency in GHz</param>
    /// <param name="isObstructed">Boolean value whether the link is obstructed</param>
    /// <param name="obstructionDistance">The obstruction distance from one of the antenna in kilometers</param>
    /// <returns>Returns List&lt;Result&gt; of Max Radius Fresnel Zone, 60 percent clearance radius, and Earth curvature height</returns>
    public List<Result> Calculate(double distance, double frequency, bool isObstructed = false,
        double obstructionDistance = 0)
    {
        string name = default!;

        double earthCurvatureHeight = Math.Pow(distance, 2) / (8 * 6_378) * 1_000;
        double clearanceRadius = 8.66 * Math.Sqrt(0.6 * distance / frequency);
        double fresnelZone = 17.32 * Math.Sqrt(distance / (4 * frequency));

        if (isObstructed)
        {
            name = $"Fresnel Zone Max Radius at {distance / 2} km";
            return
            [
                new Result(name, fresnelZone, Units.Meter),
                new Result("60% Clearance Radius", clearanceRadius, Units.Meter),
                new Result("Earth Curvature Height", earthCurvatureHeight, Units.Meter)
            ];
        }

        name = $"Fresnel Zone Radius at {obstructionDistance} km";
        return
        [
            new Result(name, fresnelZone, Units.Meter),
            new Result("Earth Curvature Height", earthCurvatureHeight, Units.Meter)
        ];
    }
}