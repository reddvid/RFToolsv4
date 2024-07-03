using RFTools.Constants;
using RFTools.Constants.SelectListItems;
using RFTools.Enums;
using RFTools.Models;

namespace RFTools.Converters;

// TODO:
public class WavelengthAndFrequency : Converter
{
    public Result Convert(Conversion conversion, double input, WaveVelocity preset)
    {
        double result = preset.Velocity / input;
        string name = conversion == Conversion.FrequencyToWavelength ? "Wavelength" : "Frequency";
        string unit = conversion == Conversion.FrequencyToWavelength ? Units.Hertz : Units.Meter;
        
        return new Result(name, result, unit);
    }
}