using RFTools.Models;

namespace RFTools.Calculators;

public interface IPathLoss
{
    /// <summary>
    /// Calculate Free-Space Path Loss
    /// </summary>
    /// <param name="transmitterGain">Transmitter Gain in dBi</param>
    /// <param name="receiverGain">Receiver Gain in dBi</param>
    /// <param name="distance">Distance between the two systems in kilometers</param>
    /// <param name="frequency">Link or system frequency in Megahertz</param>
    /// <returns>Return the System Loss(es) in dB</returns>
    Result Calculate(
        double transmitterGain, 
        double receiverGain, 
        double distance, 
        double frequency);
}