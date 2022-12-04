using RFToolsv4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFToolsv4.Constants
{
    public class ToolLinks
    {
        public static List<ToolLink> UriLinks { get; private set; } = new()
        {
            new ToolLink("PathLoss", "Wikipedia", "Free-space_path_loss"),
            new ToolLink("PathLoss", "electronics-notes", "https://www.electronics-notes.com/articles/antennas-propagation/propagation-overview/free-space-path-loss.php"),
            new ToolLink("LinkBudget", "Wikipedia", "Link_budget"),
            new ToolLink("LinkBudget", "electronics-notes", "https://www.electronics-notes.com/articles/antennas-propagation/propagation-overview/radio-link-budget-formula-calculator.php")
        };
    }

   
}
