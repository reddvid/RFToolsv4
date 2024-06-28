using RFTools.Enums;
using RFTools.Models;

namespace RFTools.Calculators;

public interface IEffectiveIsotropicRadiatedPower
{
    /// <summary>
    /// Calculate EIRP of an antenna.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="power"></param>
    /// <param name="losses"></param>
    /// <param name="gain"></param>
    /// <returns></returns>
    Result Calculate(Input type, double power, double losses, double gain);
}