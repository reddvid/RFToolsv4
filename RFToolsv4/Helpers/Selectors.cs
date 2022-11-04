using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RFToolsv4.Models.SelectorModels;

namespace RFToolsv4.Helpers
{
    public class Selectors
    {
        public static List<Distance> LargeDistance = new()
        {
            new Distance("m", 1),
            new Distance("km", 1_000),
            new Distance("mi",  0.000621371),
            new Distance("ft", 3.28084)
        };

        public static List<Frequency> LargeFrequency = new()
        {
            new Frequency("GHz", 1_000_000_000),
            new Frequency("MHz", 1_000_000),
            new Frequency("kHz", 1_000),
        };

        public static List<Material> Materials = new()
        {
            new Material("Custom", default, default),
            new Material("Silver", 1.586, 0.9998),
            new Material("Copper", 1.678, 0.999991),
            new Material("Gold", 2.24, 1),
            new Material("Aluminum", 2.6548, 1.00002),
            new Material("Nickle", 6.84, 600)
        };
    }
}
