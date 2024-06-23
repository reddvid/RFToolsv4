using RFTools.Models;

namespace RFTools.Calculator;

public interface ISkinDepth
{
    Result Calculate(
        double resistivity,
        double permeability,
        double frequency);
}