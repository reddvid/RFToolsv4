using RFTools.Constants;
using RFTools.Models;

namespace RFTools.Calculator;

public interface IStandingWave
{
    List<Result> Calculate(StandingWaves variable, double value, int precision = Values.PrecisionUnits);
}