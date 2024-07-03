using RFTools.Constants;
using RFTools.Enums;
using RFTools.Extensions;
using RFTools.Models;

namespace RFTools.Calculators;

public class EffectiveIsotropicRadiatedPower : Calculator, IEffectiveIsotropicRadiatedPower
{
    /// <summary>
    /// Calculate EIRP of an antenna.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="power"></param>
    /// <param name="losses"></param>
    /// <param name="gain"></param>
    /// <returns></returns>
    public Result Calculate(Input type, double power, double losses, double gain)
    {
        if (type == Input.mW)
        {
            power = power.ToDecibelMilliwatts();
        }
        
        double eirp = power - losses + gain;
        
        return new Result("EIRP", eirp, Units.DecibelMilliwatts);
    }
}