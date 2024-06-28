using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFTools.Uwp.Models;

namespace RFTools.Uwp.Constants
{
    public class ToolLinks
    {
        public static List<ToolLink> UriLinks { get; private set; } = new()
        {
            new ToolLink("PathLoss", "Wikipedia", "Free-space_path_loss"),
            new ToolLink("PathLoss", "electronicsnotes", "https://www.electronics-notes.com/articles/antennas-propagation/propagation-overview/free-space-path-loss.php"),
            new ToolLink("LinkBudget", "Wikipedia", "Link_budget"),
            new ToolLink("LinkBudget", "electronicsnotes", "https://www.electronics-notes.com/articles/antennas-propagation/propagation-overview/radio-link-budget-formula-calculator.php"),
            new ToolLink("VSWR", "Wikipedia", "https://en.wikipedia.org/wiki/Standing_wave"),
            new ToolLink("VSWR", "physics.info", "https://physics.info/waves-standing/"),
            new ToolLink("FresnelZone", "Wikipedia", "https://en.wikipedia.org/wiki/Fresnel_zone"),
            new ToolLink("Resonance", "Wikipedia", "https://en.wikipedia.org/wiki/Resonance"),
            new ToolLink("SkinDepth", "Wikipedia", "https://en.wikipedia.org/wiki/Skin_effect"),
            new ToolLink("SkinDepth", "Microwaves101", "https://www.microwaves101.com/encyclopedias/skin-depth"),
            new ToolLink("Wavelength", "Wikipedia", "https://en.wikipedia.org/wiki/Wavelength"),
            new ToolLink("Wavelength", "Energy Education", "https://energyeducation.ca/encyclopedia/Wavelength"),
            new ToolLink("DeltaWye", "Wikipedia", "https://en.wikipedia.org/wiki/Delta-wye_transformer"),
            new ToolLink("DeltaWye", "Khan Academy", "https://www.khanacademy.org/science/electrical-engineering/ee-circuit-analysis-topic/ee-resistor-circuits/a/ee-delta-wye-resistor-networks"),
            new ToolLink("PEC", "Electric Power (Wikipedia)", "https://en.wikipedia.org/wiki/Electric_power"),
            new ToolLink("PEC", "Electrical Power (electronicsnotes)", "https://www.electronics-notes.com/articles/basic_concepts/power/what-is-electrical-power-basics-tutorial.php"),
            new ToolLink("PEC", "Electrical Energy (Wikipedia)", "https://en.wikipedia.org/wiki/Electrical_energy"),
            new ToolLink("PEC", "Electrical Charge (Wikipedia)", "https://en.wikipedia.org/wiki/Electric_charge"),
        };
    }


}
