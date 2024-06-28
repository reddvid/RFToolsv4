using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFTools.Uwp.Models;
using static RFTools.Uwp.Models.SelectorModels;

namespace RFTools.Uwp.Helpers
{
    public class Selectors
    {
        public static List<SelectorModels.Flex> Distance { get; } = new()
        {
            new SelectorModels.Flex("m", 1),
            new SelectorModels.Flex("km", Math.Pow(10, 3)),
            new SelectorModels.Flex("mi",  1_609.34),
            new SelectorModels.Flex("ft", 0.3048)
        };

        public static List<SelectorModels.Flex> LargeFrequency { get; } = new()
        {
            new SelectorModels.Flex("THz", Math.Pow(10, 12)),
            new SelectorModels.Flex("GHz", Math.Pow(10, 9)),
            new SelectorModels.Flex("MHz", Math.Pow(10, 6)),
            new SelectorModels.Flex("kHz", Math.Pow(10, 3)),
        };

        public static List<SelectorModels.Material> Materials { get; } = new()
        {
            new SelectorModels.Material("Custom", default, default),
            new SelectorModels.Material("Silver", 1.586, 0.9998),
            new SelectorModels.Material("Copper", 1.678, 0.999991),
            new SelectorModels.Material("Gold", 2.24, 1),
            new SelectorModels.Material("Aluminum", 2.6548, 1.00002),
            new SelectorModels.Material("Nickle", 6.84, 600)
        };

        public static List<SelectorModels.Flex> Wavelengths { get; } = new()
        {
            new SelectorModels.Flex("m", 1),
            new SelectorModels.Flex("cm", Math.Pow(10, -2)),
            new SelectorModels.Flex("mm", Math.Pow(10, -3)),
            new SelectorModels.Flex("μm", Math.Pow(10, -6)),
            new SelectorModels.Flex("nm", Math.Pow(10, -9))
        };

        public static List<SelectorModels.Flex> Frequencies { get; } = new()
        {
            new SelectorModels.Flex("THz", Math.Pow(10,12)),
            new SelectorModels.Flex("GHz", Math.Pow(10,9)),
            new SelectorModels.Flex("MHz", Math.Pow(10,6)),
            new SelectorModels.Flex("kHz", Math.Pow(10,3)),
            new SelectorModels.Flex("Hz", 1),
        };

        public static List<SelectorModels.Preset> Media { get; } = new()
        {
            new SelectorModels.Preset("light in vacuum", 299_792_458),
            new SelectorModels.Preset("light in air", 299_702_547),
            new SelectorModels.Preset("light in water", 225_238_511),
            new SelectorModels.Preset("light in glass", 199_861_639),
            new SelectorModels.Preset("sound in air at 20°C", 343),
            new SelectorModels.Preset("sound in air at 40°C", 355),
            new SelectorModels.Preset("sound in water at 20°C", 1_481),
            new SelectorModels.Preset("sound in water at 40°C", 1_526),
            new SelectorModels.Preset("sound in seawater at 20°C", 1_522),
            new SelectorModels.Preset("sound in seawater at 40°C", 1_563),
            new SelectorModels.Preset("sound in glass", 4_540),
            new SelectorModels.Preset("sound in copper", 4_600),
            new SelectorModels.Preset("sound in aluminum", 6_320),
        };

        public static List<SelectorModels.Flex> Power { get; } = new()
        {
            new SelectorModels.Flex("mW", default),
            new SelectorModels.Flex("W", default),
            new SelectorModels.Flex("dBm",  1),
            new SelectorModels.Flex("dBW", default)
        };

        public static List<SelectorModels.Flex> Energy { get; } = new()
        {
            new SelectorModels.Flex("kWh", Math.Pow(10, 3)),
            new SelectorModels.Flex("Wh", 1),
            new SelectorModels.Flex("BTU", 0.293071),
            new SelectorModels.Flex("Joule", 0.000277778),
            new SelectorModels.Flex("kilojoule",0.277778),
            new SelectorModels.Flex("electronvolt", 4.4505 * Math.Pow(10, -23)),
            new SelectorModels.Flex("kilocalorie", 1.16222),
        };

        public static List<SelectorModels.Flex> Charge { get; } = new()
        {
            new SelectorModels.Flex("Microcoulomb (μC)", Math.Pow(10, -6)),
            new SelectorModels.Flex("Millicoulomb (mC)", Math.Pow(10, -3)),
            new SelectorModels.Flex("Coulomb (C)", 1),
            new SelectorModels.Flex("Electron charge (Qₑ)", 6.2415 * Math.Pow(10, -18)),
            new SelectorModels.Flex("Ampere-hour (Ah)", 0.000277778),
            new SelectorModels.Flex("mAh", 0.277778),
        };

        public static List<SelectorModels.Flex> Capacitance { get; } = new()
        {
            new SelectorModels.Flex("pF", Math.Pow(10, -12)),
            new SelectorModels.Flex("nF", Math.Pow(10, -9)),
            new SelectorModels.Flex("μF", Math.Pow(10, -6)),
            new SelectorModels.Flex("mF", Math.Pow(10, -3)),
            new SelectorModels.Flex("F", 1),
        };

        public static List<SelectorModels.Flex> Inductance { get; } = new()
        {
            new SelectorModels.Flex("pH", Math.Pow(10, -12)),
            new SelectorModels.Flex("nH", Math.Pow(10, -9)),
            new SelectorModels.Flex("μH", Math.Pow(10, -6)),
            new SelectorModels.Flex("mH", Math.Pow(10, -3)),
            new SelectorModels.Flex("H", 1),
        };

        public static List<SelectorModels.Flex> Resistance { get; } = new()
        {
            new SelectorModels.Flex("megohms (MΩ)", Math.Pow(10, 6)),
            new SelectorModels.Flex("kilohms (kΩ)", Math.Pow(10, 3)),
            new SelectorModels.Flex("ohms (Ω)", 1),
            new SelectorModels.Flex("milliohms (mΩ)", Math.Pow(10, -3)),
        };

        public static List<SelectorModels.Flex> OutputPower { get; } = new()
        {
            new SelectorModels.Flex("dBm", 1),
            new SelectorModels.Flex("mW", default),
            new SelectorModels.Flex("W",  default),
            new SelectorModels.Flex("kW", default),
        };

        public static List<SelectorModels.Preset> DielectricConstants { get; } = new()
        {
            new SelectorModels.Preset("Vacuum (1)", 1),
            new SelectorModels.Preset("Air (1.00054)", 1.00054),
            new SelectorModels.Preset("Gypsum (≥2.5)", 2.5),
            new SelectorModels.Preset("Silica (≥3.0)", 3.0),
            new SelectorModels.Preset("Paper (≥3.5)", 3.5),
            new SelectorModels.Preset("Concrete (4.5)", 4.5),
        };

        public static List<SelectorModels.Flex> Diamters { get; } = new()
        {
            new SelectorModels.Flex("nm", Math.Pow(10, -9)),
            new SelectorModels.Flex("μm", Math.Pow(10, -5)),
            new SelectorModels.Flex("mm", Math.Pow(10, -3)),
            new SelectorModels.Flex("cm", Math.Pow(10, -2)),
            new SelectorModels.Flex("in", 0.0254),
        };
    }
}
