using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFToolsv4.Models
{
    public class SelectorModels
    {
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

        public class Cable
        {
            public string Manufacturer { get; set; }
            public string Brand { get; set; }
        }

        public class Flex
        {
            public string Caption { get; set; }
            public double Multiplier { get; set; }
            public Flex(string caption, double multiplier)
            {
                Caption = caption;
                Multiplier = multiplier;
            }
        }

        public class Preset
        {
            public string Caption { get; set; }
            public double Value { get; set; }
            public Preset(string caption, double value)
            {
                Caption = caption;
                Value = value;
            }
        }
    }
}
