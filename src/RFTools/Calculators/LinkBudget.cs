using RFTools.Constants;
using RFTools.Models;

namespace RFTools.Calculators;

public class LinkBudget : Calculator, ILinkBudget
{
    /// <summary>
    /// Calculate Link Budget
    /// </summary>
    /// <param name="transmitterPower">Transmitter output power in dBm</param>
    /// <param name="transmitterGain">Transmitter gain in dBi</param>
    /// <param name="transmitterLoss">Transmitter losses in dB</param>
    /// <param name="receiverGain">Receiver gain in dBi</param>
    /// <param name="receiverLoss">Receiver losses in dB</param>
    /// <param name="distance">System distance in kilometers</param>
    /// <param name="frequency">System frequency in Megahertz</param>
    /// <returns>Received Power in dBm</returns>
    public List<Result> Calculate(
        double transmitterPower,
        double transmitterGain,
        double transmitterLoss,
        double receiverGain,
        double receiverLoss,
        double distance,
        double frequency)
    {
        if (distance <= 0 || frequency <= 0)
        {
            throw new ArgumentException("Value must be greater than zero.");
        }
        
        double pathLoss = 20 * Math.Log10(distance) + 20 * Math.Log10(frequency) + 32.44;
        double receivedPower = transmitterPower + transmitterGain + receiverGain -
                               transmitterLoss - receiverLoss - pathLoss;
        return
        [
            new Result("Transmitted Power", transmitterPower, unit: Units.DecibelMilliwatts),
            new Result("Received Power", receivedPower, unit: Units.DecibelMilliwatts),
            new Result("Path Loss", pathLoss, unit: Units.Decibel)
        ];
    }
}