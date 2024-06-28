using System.Globalization;
using System.Text;
using RFTools.Extensions;
using RFTools.Models;

namespace RFTools.Utilities;

public class ResultBuilder
{
    private static readonly char[] EngineeringCharacters = ['e', 'E'];

    public static string Build(List<Result> results)
    {
        StringBuilder stringBuilder = new();
        foreach (var result in results)
        {
            if (result.Unit.Contains("too small"))
                stringBuilder.AppendLine($"{result.Name}: Value is too small!");
            else if (EngineeringCharacters.Any(c =>
                         result.Value.ToString(CultureInfo.InvariantCulture).ToCharArray().Contains(c)))
                stringBuilder.AppendLine($"{result.Name}: {result.Value.ToString("G4")} {result.Unit}");
            else
                stringBuilder.AppendLine($"{result.Name}: {result.Value.ToString("N")} {result.Unit}");
        }
        
        return stringBuilder.ToString();
    }
}