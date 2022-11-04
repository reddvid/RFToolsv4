using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFToolsv4.Models
{
    public class SelectorModels
    {
        public class Distance
        {
            public double Multiplier { get; set; }
            public string Caption { get; set; }
            public Distance(string caption, double multiplier)
            {
                Caption = caption;
                Multiplier = multiplier;
            }
        }

        public class Material
        {
            public string Caption { get; set; }
            public double Resistivity { get; set; }
            public double Permeability { get; set; }
            public Material(string caption, double resistivity, double permeability)
            {
                Caption = caption;
                Resistivity = resistivity;
                Permeability = permeability;
            }
        }

        public class Frequency
        {
            public double Multiplier { get; set; }
            public string Caption { get; set; }
            public Frequency(string caption, double multiplier)
            {
                Caption = caption;
                Multiplier = multiplier;
            }
        }
    }
}
