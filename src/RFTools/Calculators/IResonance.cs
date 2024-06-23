using RFTools.Constants;
using RFTools.Enums;
using RFTools.Models;

namespace RFTools.Calculators;

public interface IResonance
{
    Result Calculate(Unknown type, double frequency = 0, double capacitance = 0, double inductance = 0);
}