using RFTools.Models;

namespace RFTools.Calculators;

public interface IDipoleAntennaLength
{
    /// <summary>
    /// Calculate the length of dipole antenna and length of each pole.
    /// </summary>
    /// <param name="frequency">The system frequency in MHz or GHz</param>
    /// <returns>The total length of dipole and each pole.</returns>
    List<Result> Calculate(double frequency);
}