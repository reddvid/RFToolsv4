using RFTools.Constants;
using RFTools.Models;

namespace RFTools.Calculators;

public interface IStandingWave
{
    List<Result> Calculate(StandingWaves variable, double value, int precision = Values.PrecisionUnits);
}