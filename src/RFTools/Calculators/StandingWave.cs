using RFTools.Constants;
using RFTools.Contracts;
using RFTools.Enums;
using RFTools.Models;

namespace RFTools.Calculators;

public class StandingWave : Calculator, IStandingWave
{
    /// <summary>
    /// Calculate Standing Waves
    /// </summary>
    /// <param name="variable">Type of wave</param>
    /// <param name="value">Numerical value of the standing wave</param>
    /// <param name="precision">Number of decimals in result</param>
    /// <exception cref="ArgumentException">Returns if input is invalid.</exception>
    /// <returns>IReadOnlyList of Results</returns>
    public List<Result> Calculate(Known variable, double value)
    {
        double vswr = default!;
        double reflectionCoefficient = default!;
        double returnLoss = default!;
        double mismatchLoss = default!;

        if (variable == Known.Vswr)
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

        if (variable == Known.ReflectionCoefficient)
        {
            reflectionCoefficient = value;

            if (reflectionCoefficient is >= 1 or <= 0)
            {
                throw new ArgumentException(message: "Reflection Coefficient should be between 0 and 1.",
                    nameof(reflectionCoefficient));
            }
            
            vswr = Math.Abs((reflectionCoefficient + 1) / (reflectionCoefficient - 1));
            returnLoss = -20 * Math.Log10(reflectionCoefficient);
            mismatchLoss = -10 * Math.Log10(1 - reflectionCoefficient * reflectionCoefficient);
        }

        if (variable == Known.ReturnLoss)
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
            new Result("VSWR", vswr),
            new Result("Reflection Coefficient (Γ)", reflectionCoefficient),
            new Result("Return Loss", returnLoss, Units.Decibel),
            new Result("Mismatch Loss", mismatchLoss, Units.Decibel)
        ];
    }
}