using RFTools.Constants;
using RFTools.Contracts;
using RFTools.Models;

namespace RFTools.Calculators;

public class StandingWave : Calculator, IStandingWave
{
    /// <summary>
    /// Calculate Standing Waves
    /// </summary>
    /// <param name="variable">Type of wave</param>
    /// <param name="value">Numerical value of the standing wave</param>
    /// <param name="precision"></param>
    /// <exception cref="ArgumentException">Returns if input is invalid.</exception>
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
            if (vswr < 1)
            {
                throw new ArgumentException(message: "VSWR should not be lower than 1.",
                    nameof(vswr));
            }
            
            reflectionCoefficient = (vswr - 1) / (vswr + 1);
            returnLoss = -20 * Math.Log10(reflectionCoefficient);
            mismatchLoss = -10 * Math.Log10(1 - reflectionCoefficient * reflectionCoefficient);
        }

        if (variable == StandingWaves.ReflectionCoefficient)
        {
            reflectionCoefficient = value;

            if (reflectionCoefficient is >= 1 or < 0)
            {
                throw new ArgumentException(message: "Reflection Coefficient should be between 0 and 1.",
                    nameof(reflectionCoefficient));
            }
            
            if (reflectionCoefficient == 0)
            {
                vswr = 0;
                returnLoss = double.PositiveInfinity;
                mismatchLoss = 0;
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

            if (returnLoss <= 0)
            {
                throw new ArgumentException(message: "Return Loss should not be 0 or lower than 0.",
                    nameof(returnLoss));
            }
            
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