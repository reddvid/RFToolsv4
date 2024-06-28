using RFTools.Constants;
using RFTools.Contracts;
using RFTools.Enums;
using RFTools.Models;

namespace RFTools.Calculators;

public class Resonance : Calculator, IResonance
{
    /// <summary>
    /// Calculate Resonance Frequency
    /// </summary>
    /// <param name="type">Unknown enum for the unknown value.</param>
    /// <param name="frequency">Frequency in Hertz</param>
    /// <param name="capacitance">Capacitance in Farad</param>
    /// <param name="inductance">Inductance in Henry</param>
    /// <returns>Returns selected Unknown enum</returns>
    public Result Calculate(
        Unknown type, 
        double frequency = 0, 
        double capacitance = 0, 
        double inductance = 0)
    {
        double result = default!;
        string unit = default!;
        string resultName = default!;

        switch (type)
        {
            case Unknown.Frequency:
                result = 1 / (2 * Math.PI * Math.Sqrt(capacitance * inductance));
                unit = Units.Hertz;
                resultName = "Resonant Frequency";
                break;
            case Unknown.Capacitance:
                result = 1 / (4 * Math.Pow(Math.PI, 2) * Math.Pow(frequency, 2) * inductance);
                unit = Units.Farad;
                resultName = "Capacitance";
                break;
            case Unknown.Inductance:
                result = 1 / (4 * Math.Pow(Math.PI, 2) * Math.Pow(frequency, 2) * capacitance);
                unit = Units.Henry;
                resultName = "Inductance";
                break;
        }
        
        return new Result(resultName, result, unit);
    }
}