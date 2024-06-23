namespace RFTools.Models.SelectListItems;

public class Material(string caption, double resistivity, double permeability) : SelectListItem
{
    public string Caption { get; init; } = caption;
    public double Resistivity { get; init; } = resistivity;
    public double Permeability { get; init; } = permeability;
}