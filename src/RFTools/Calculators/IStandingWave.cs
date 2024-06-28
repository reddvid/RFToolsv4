using RFTools.Constants;
using RFTools.Enums;
using RFTools.Models;

namespace RFTools.Calculators;

public interface IStandingWave
{
    /// <summary>
    /// Calculate Standing Waves
    /// </summary>
    /// <param name="variable">Type of wave</param>
    /// <param name="value">Numerical value of the standing wave</param>
    /// <exception cref="ArgumentException">Returns if input is invalid.</exception>
    /// <returns>IReadOnlyList of Results</returns>
    List<Result> Calculate(Known variable, double value);
}