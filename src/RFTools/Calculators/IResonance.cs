using RFTools.Constants;
using RFTools.Enums;
using RFTools.Models;

namespace RFTools.Calculators;

public interface IResonance
{
    /// <summary>
    /// Calculate Resonance Frequency
    /// </summary>
    /// <param name="type">Unknown enum for the unknown value.</param>
    /// <param name="frequency">Frequency in Hertz</param>
    /// <param name="capacitance">Capacitance in Farad</param>
    /// <param name="inductance">Inductance in Henry</param>
    /// <returns>Returns selected Unknown enum</returns>
    Result Calculate(Unknown type, double frequency = 0, double capacitance = 0, double inductance = 0);
}