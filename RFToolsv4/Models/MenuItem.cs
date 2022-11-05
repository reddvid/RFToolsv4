using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFToolsv4.Models
{
    public class MenuItem
    {
        public string Title { get; set; }
        public string Tag { get; set; }

        public MenuItem(string title, string tag)
        {
            Title = title;
            Tag = tag;
        }
    }
}
