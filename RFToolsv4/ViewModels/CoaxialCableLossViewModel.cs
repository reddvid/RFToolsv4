using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFToolsv4.ViewModels
{
    public class CoaxialCableLossViewModel
    {
        public List<string> Manufacturers { get; private set; }

        public CoaxialCableLossViewModel()
        {
            Manufacturers = new()
            {
                "Andrew",
                "Belden",
                "David RF",
                "Radio Shack",
                "Times Microwave System",
                "Wireman (Coaxial)",
                "Wireman (Ladder Line)",
                "Others"
            };
        }
    }
}
