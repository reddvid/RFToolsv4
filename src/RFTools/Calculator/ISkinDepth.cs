using RFTools.Models;

namespace RFTools.Calculator;

public interface ISkinDepth: ICalculator
{
    Result Calculate(
        double resistivity,
        double permeability,
        double frequency);
}