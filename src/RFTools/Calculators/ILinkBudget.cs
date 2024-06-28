using RFTools.Models;

namespace RFTools.Calculators;

public interface ILinkBudget
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
    List<Result> Calculate(
        double transmitterPower,
        double transmitterGain,
        double transmitterLoss,
        double receiverGain,
        double receiverLoss,
        double distance,
        double frequency);
}