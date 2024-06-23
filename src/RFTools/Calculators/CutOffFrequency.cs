using RFTools.Constants;
using RFTools.Contracts;
using RFTools.Enums;
using RFTools.Models;

namespace RFTools.Calculators;

public class CutOffFrequency : Calculator
{
    /// <summary>
    /// Find the cut-off frequency of RC/RL circuits
    /// </summary>
    /// <param name="type">The type of circuit (RC or RL)</param>
    /// <param name="resistance">The circuit's resistance in Ohm</param>
    /// <param name="capacitance">The circuit's capacitance in Farad</param>
    /// <param name="inductance">The circuit's inductance in Henry</param>
    /// <returns>Cut-off frequency in Hertz</returns>
    public Result Calculate(CircuitType type, double resistance, double capacitance = 0, double inductance = 0)
    {
        double cutOffFrequency = default!;

        if (type == CircuitType.RC)
        {
            cutOffFrequency = 1 / (2 * Math.PI * resistance * capacitance);
        }

        if (type == CircuitType.RL)
        {
            cutOffFrequency = resistance / (2 * Math.PI * inductance);
        }

        return new Result("Cut-off Frequency", cutOffFrequency, Units.Hertz);
    }

    public Result Calculate(
        CircuitType type,
        Unknown unknown,
        double frequency,
        double resistance = 0,
        double capacitance = 0,
        double inductance = 0)
    {
        double unknownValue = default!;
        string unknownName = default!;
        string unit = default!;

        if (unknown == Unknown.Capacitance)
        {
            unknownValue = 1 / (2 * Math.PI * resistance * frequency);
            unknownName = "Capacitance";
            unit = Units.Farad;
        }
        else if (unknown == Unknown.Resistance)
        {
            if (type == CircuitType.RC)
            {
                unknownValue = 1 / (2 * Math.PI * capacitance * frequency);
                unknownName = "Resistance";
                unit = Units.Ohm;
            }
            else if (type == CircuitType.RL)
            {
                unknownValue = 2 * Math.PI * inductance * frequency;
                unknownName = "Resistance";
                unit = Units.Ohm;
            }
        }
        else if (unknown == Unknown.Inductance)
        {
            unknownValue = resistance / (2 * Math.PI * frequency);
            unknownName = "Inductance";
            unit = Units.Henry;
        }

        return new Result(unknownName, unknownValue, unit);
    }
}