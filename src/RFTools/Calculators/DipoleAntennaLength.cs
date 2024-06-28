using RFTools.Constants;
using RFTools.Contracts;
using RFTools.Models;

namespace RFTools.Calculators;

public class DipoleAntennaLength : Calculator, IDipoleAntennaLength
{
    /// <summary>
    /// Calculate the length of dipole antenna and length of each pole.
    /// </summary>
    /// <param name="frequency">The system frequency in MHz or GHz</param>
    /// <returns>The total length of dipole and each pole.</returns>
    public List<Result> Calculate(double frequency)
    {
        double totalLength = 468 / frequency;
        double monopoleLength = totalLength / 2;

        return
        [
            new Result("Total Length of Dipole", totalLength, Units.Feet),
            new Result("Length of Each Dipole", monopoleLength, Units.Feet)
        ];
    }
}