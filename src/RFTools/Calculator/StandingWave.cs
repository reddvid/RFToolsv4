using RFTools.Constants;
using RFTools.Models;

namespace RFTools.Calculator;

public class StandingWave : IStandingWave
{
    /// <summary>
    /// Calculate Standing Waves
    /// </summary>
    /// <param name="variable">Type of wave</param>
    /// <param name="value">Numerical value of the standing wave</param>
    /// <param name="precision"></param>
    /// <returns>IReadOnlyList of Results</returns>
    public List<Result> Calculate(StandingWaves variable, double value, int precision = Values.PrecisionUnits)
    {
        double vswr = default!;
        double reflectionCoefficient = default!;
        double returnLoss = default!;
        double mismatchLoss = default!;

        if (variable == StandingWaves.VSWR)
        {
            vswr = value;
            reflectionCoefficient = (vswr - 1) / (vswr + 1);
            returnLoss = -20 * Math.Log10(reflectionCoefficient);
            mismatchLoss = -10 * Math.Log10(1 - reflectionCoefficient * reflectionCoefficient);
        }

        if (variable == StandingWaves.ReflectionCoefficient)
        {
            reflectionCoefficient = value;

            if (reflectionCoefficient >= 1)
            {
                vswr = double.PositiveInfinity;
                returnLoss = 0;
                mismatchLoss = double.PositiveInfinity;
            }
            else if (reflectionCoefficient == 0)
            {
                vswr = 0;
                returnLoss = double.PositiveInfinity;
                mismatchLoss = 0;
            }
            else if (reflectionCoefficient < 0 && reflectionCoefficient < -1)
            {
                vswr = 0;
                returnLoss = double.NaN;
                mismatchLoss = -10 * Math.Log10(1 - reflectionCoefficient * reflectionCoefficient);
            }
            else if (reflectionCoefficient <= -1)
            {
                vswr = 0;
                returnLoss = double.NaN;
                mismatchLoss = double.PositiveInfinity;
            }
            else
            {
                vswr = Math.Abs((reflectionCoefficient + 1) / (reflectionCoefficient - 1));
                returnLoss = -20 * Math.Log10(reflectionCoefficient);
                mismatchLoss = -10 * Math.Log10(1 - reflectionCoefficient * reflectionCoefficient);
            }
        }

        if (variable == StandingWaves.ReturnLoss)
        {
            returnLoss = value;
            reflectionCoefficient = Math.Pow(10, returnLoss / -20);
            vswr = Math.Abs((reflectionCoefficient + 1) / (reflectionCoefficient - 1));
            mismatchLoss = -10 * Math.Log10(1 - reflectionCoefficient * reflectionCoefficient);
        }

        return
        [
            new Result("VSWR", vswr, precision: 2),
            new Result("Reflection Coefficient (Γ)", reflectionCoefficient, precision: 2),
            new Result("Return Loss", returnLoss, Units.Decibel, precision: 2),
            new Result("Mismatch Loss", mismatchLoss, Units.Decibel, precision: 2)
        ];
    }
}