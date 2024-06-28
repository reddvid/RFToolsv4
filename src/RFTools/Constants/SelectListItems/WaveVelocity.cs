using RFTools.Models;

namespace RFTools.Constants.SelectListItems;

public class WaveVelocity(string caption, double velocity) : SelectListItem
{
    public string Caption { get; init; } = caption;
    public double Velocity { get; init; } = velocity;
}