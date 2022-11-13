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
        public static List<Flex> Distance { get; } = new()
        {
            new Flex("m", 1),
            new Flex("km", Math.Pow(10, 3)),
            new Flex("mi",  0.000621371),
            new Flex("ft", 3.28084)
        };

        public static List<Flex> LargeFrequency { get; } = new()
        {
            new Flex("THz", Math.Pow(10, 12)),
            new Flex("GHz", Math.Pow(10, 9)),
            new Flex("MHz", Math.Pow(10, 6)),
            new Flex("kHz", Math.Pow(10, 3)),
        };

        public static List<Material> Materials { get; } = new()
        {
            new Material("Custom", default, default),
            new Material("Silver", 1.586, 0.9998),
            new Material("Copper", 1.678, 0.999991),
            new Material("Gold", 2.24, 1),
            new Material("Aluminum", 2.6548, 1.00002),
            new Material("Nickle", 6.84, 600)
        };

        public static List<Flex> Wavelengths { get; } = new()
        {
            new Flex("m", 1),
            new Flex("cm", Math.Pow(10, -2)),
            new Flex("mm", Math.Pow(10, -3)),
            new Flex("μm", Math.Pow(10, -6)),
            new Flex("nm", Math.Pow(10, -9))
        };

        public static List<Flex> Frequencies { get; } = new()
        {
            new Flex("THz", Math.Pow(10,12)),
            new Flex("GHz", Math.Pow(10,9)),
            new Flex("MHz", Math.Pow(10,6)),
            new Flex("kHz", Math.Pow(10,3)),
            new Flex("Hz", 1),
        };

        public static List<Preset> Media { get; } = new()
        {
            new Preset("light in vacuum", 299_792_458),
            new Preset("light in air", 299_702_547),
            new Preset("light in water", 225_238_511),
            new Preset("light in glass", 199_861_639),
            new Preset("sound in air at 20°C", 343),
            new Preset("sound in air at 40°C", 355),
            new Preset("sound in water at 20°C", 1_481),
            new Preset("sound in water at 40°C", 1_526),
            new Preset("sound in seawater at 20°C", 1_522),
            new Preset("sound in seawater at 40°C", 1_563),
            new Preset("sound in glass", 4_540),
            new Preset("sound in copper", 4_600),
            new Preset("sound in aluminum", 6_320),
        };

        public static List<Flex> Power { get; } = new()
        {
            new Flex("mW", 1),
            new Flex("W", Math.Pow(10, 3)),
            new Flex("dBm",  1.2589),
            new Flex("dBW", 1_258.9254)
        };

        public static List<Flex> Energy { get; } = new()
        {
            new Flex("kWh", Math.Pow(10, 3)),
            new Flex("Wh", 1),
            new Flex("BTU", 0.293071),
            new Flex("Joule", 0.000277778),
            new Flex("kilojoule",0.277778),
            new Flex("electronvolt", 4.4505 * Math.Pow(10, -23)),
            new Flex("kilocalorie", 1.16222),
        };

        public static List<Flex> Charge { get; } = new()
        {
            new Flex("Microcoulomb (μC)", Math.Pow(10, -6)),
            new Flex("Millicoulomb (mC)", Math.Pow(10, -3)),
            new Flex("Coulomb (C)", 1),
            new Flex("Electron charge (Qₑ)", 6.2415 * Math.Pow(10, -18)),
            new Flex("Ampere-hour (Ah)", 0.000277778),
            new Flex("mAh", 0.277778),
        };

        public static List<Flex> Capacitance { get; } = new()
        {
            new Flex("pF", Math.Pow(10, -12)),
            new Flex("nF", Math.Pow(10, -9)),
            new Flex("μF", Math.Pow(10, -6)),
            new Flex("mF", Math.Pow(10, -3)),
            new Flex("F", 1),
        };

        public static List<Flex> Inductance { get; } = new()
        {
            new Flex("pH", Math.Pow(10, -12)),
            new Flex("nH", Math.Pow(10, -9)),
            new Flex("μH", Math.Pow(10, -6)),
            new Flex("mH", Math.Pow(10, -3)),
            new Flex("H", 1),
        };

        public static List<Flex> Resistance { get; } = new()
        {
            new Flex("megohms (MΩ)", Math.Pow(10, 6)),
            new Flex("kilohms (kΩ)", Math.Pow(10, 3)),
            new Flex("ohms (Ω)", 1),
            new Flex("milliohms (mΩ)", Math.Pow(10, -3)),
        };

        public static List<Flex> OutputPower { get; } = new()
        {
            new Flex("dBm", 1),
            new Flex("mW", default),
            new Flex("W",  default),
            new Flex("kW", default),
        };

        public static List<Preset> DielectricConstants { get; } = new()
        {
            new Preset("Vacuum (1)", 1),
            new Preset("Air (1.00054)", 1.00054),
            new Preset("Gypsum (≥2.5)", 2.5),
            new Preset("Silica (≥3.0)", 3.0),
            new Preset("Paper (≥3.5)", 3.5),
            new Preset("Concrete (4.5)", 4.5),
        };
    }
}
