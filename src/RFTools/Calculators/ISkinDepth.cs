using RFTools.Models;

namespace RFTools.Calculators;

public interface ISkinDepth
{
    Result Calculate(double resistivity,
        double permeability,
        double frequency, int precision = 4);
}