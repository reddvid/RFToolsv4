using RFTools.Enums;
using RFTools.Models;

namespace RFTools.Calculators;

public interface ICutOffFrequency
{
    /// <summary>
    /// Find the cut-off frequency of RC/RL circuits
    /// </summary>
    /// <param name="type">The type of circuit (RC or RL)</param>
    /// <param name="resistance">The circuit's resistance in Ohm</param>
    /// <param name="capacitance">The circuit's capacitance in Farad</param>
    /// <param name="inductance">The circuit's inductance in Henry</param>
    /// <returns>Returns the cut-off frequency in Hertz</returns>
    Result Calculate(CircuitType type, double resistance, double capacitance = 0, double inductance = 0);

    /// <summary>
    /// Find an unknown component of RC/RL circuits at a certain cut-off frequency.
    /// </summary>
    /// <param name="type">The type of circuit (RC or RL)</param>
    /// <param name="unknown">The unknown electronic component. Either Resistance, Capacitance, or Inductance</param>
    /// <param name="frequency">Cut-off frequency in Hertz</param>
    /// <param name="resistance">The resistance of the RC/RL circuit</param>
    /// <param name="capacitance">The capacitance of the RC/RL circuit</param>
    /// <param name="inductance">The inductance of the RC/RL circuit</param>
    /// <returns>Returns the unknown circuit component</returns>
    Result Calculate(
        CircuitType type,
        Unknown unknown,
        double frequency,
        double resistance = 0,
        double capacitance = 0,
        double inductance = 0);
}