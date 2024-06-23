using RFTools.Constants;
using RFTools.Models;

namespace RFTools.Calculators;

public interface IStandingWave
{
    List<Result> Calculate(Known variable, double value, int precision = Values.PrecisionUnits);
}