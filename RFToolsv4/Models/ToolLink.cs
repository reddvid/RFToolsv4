using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFToolsv4.Models
{
    public class ToolLink
    {
        public string Tool { get; set; }
        public string Title { get; set; }
        public Uri Link { get; set; }
        public ToolLink(string tool, string title, string link)
        {
            Tool = tool;
            Title = title;
            Link = (title.Contains("Wikipedia") && !link.Contains("wikipedia")) 
                    ? new Uri($"https://wikipedia.org/wiki/{link}") 
                    : new Uri(link);
        }
    }
}
