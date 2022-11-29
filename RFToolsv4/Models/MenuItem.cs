using Microsoft.UI.Xaml.Controls;
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
        public Type Page { get; set; }

        public MenuItem(string title, Type type)
        {
            Title = title;
            Page = type;
        }

        public override string ToString()
        {
            return Title;
        }
    }

    public class MenuGroup
    {
        public string Category { get; set; }
        public List<MenuItem> Menu { get; set; }

        public MenuGroup(string category, List<MenuItem> menuItems)
        {
            Category = category;
            Menu = menuItems;
        }

        public override string ToString()
        {
            return Category;
        }
    }

}