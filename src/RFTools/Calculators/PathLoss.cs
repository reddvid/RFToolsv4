using RFTools.Constants;
using RFTools.Contracts;
using RFTools.Models;

namespace RFTools.Calculators;

public class PathLoss : Calculator, IPathLoss
{
    /// <summary>
    /// Calculate Free-Space Path Loss
    /// </summary>
    /// <param name="transmitterGain">Transmitter Gain in dBi</param>
    /// <param name="receiverGain">Receiver Gain in dBi</param>
    /// <param name="distance">Distance between the two systems in kilometers</param>
    /// <param name="frequency">Link or system frequency in Megahertz</param>
    /// <param name="precision">Number of decimals in result</param>
    /// <returns>Return the System Loss(es) in dB</returns>
    public Result Calculate(
        double transmitterGain, 
        double receiverGain, 
        double distance, 
        double frequency, 
        int precision = 4)
    {
        if (distance <= 0)
        {
            throw new ArgumentException("Value must be greater than zero.");
        }
        
        double pathLoss = 20 * Math.Log10(distance) + 20 * Math.Log10(frequency) + 32.44 - receiverGain - transmitterGain;

        return new Result("Path Loss", pathLoss, unit: Units.Decibel, precision: 2);
    }
}