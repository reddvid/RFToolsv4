using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFTools.Uwp.Models;

namespace RFTools.Uwp.Helpers
{
    public class ResultsBuilder
    {
        public static string Build(List<Result> results)
        {
            StringBuilder builder = new();
            foreach (var result in results)
            {
                if (result.Units != null && result.Units.Contains("too small"))
                {
                    builder.AppendLine($"{result.Name}: Value is too small!");
                }
                else if (result.Value.ToString().ToUpper().Contains("E"))
                {
                    builder.AppendLine($"{result.Name}: {result.Value.ToString("G4")} {result.Units}");
                }
                else
                {
                    builder.AppendLine($"{result.Name}: {result.Value.ToString("N")} {result.Units}");
                }
            }

            return builder.ToString();
        }
    }
}
