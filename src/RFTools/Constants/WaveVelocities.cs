using RFTools.Constants.SelectListItems;

namespace RFTools.Constants;

/// <summary>
/// Wave velocity presets in meters per second
/// </summary>
public class WaveVelocities
{
    public static List<WaveVelocity> All()
    {
        return
        [
            new("Light in Vacuum", 299_792_458),
            new("Light in Air", 299_702_547),
            new("Light in Water", 225_238_511),
            new("Light in Glass", 199_861_639),
            new("Sound in Air at 20 °C", 343),
            new("Sound in Air at 40 °C", 355),
            new("Sound in Water at 20 °C", 1_481),
            new("Sound in Water at 40 °C", 1_526),
            new("Sound in Seawater at 20 °C", 1_522),
            new("Sound in Seawater at 40 °C", 1_563),
            new("Sound in Copper", 4_600),
            new("Sound in Copper", 4_600),
        ];
    }
    
}