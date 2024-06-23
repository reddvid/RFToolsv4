namespace RFTools.Calculator;

/// <summary>
/// Calculate Skin Depth
/// </summary>
/// <param name="resistivity">Material resistivity in <b>Ohms/cm</b></param>
/// <param name="permeability">Material permeability (Value is usually ~1)</param>
/// <param name="frequency">System operating frequency in <b>Hertz</b></param>
public class SkinDepth(
    double resistivity,
    double permeability,
    double frequency) : Calculator
{
    public override string Calculate()
    {
        double denominator = Math.Pow(Math.PI, 2) * frequency * permeability * 4 * Math.Pow(10, -7);

        double skinDepthInMeters = Math.Sqrt(resistivity / denominator);
        
        return default!;
    }
}