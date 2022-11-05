using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using RFToolsv4.Helpers;
using RFToolsv4.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static RFToolsv4.Models.SelectorModels;

namespace RFToolsv4.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public List<MenuItem> MenuItems = new()
        {
           new MenuItem("Free-Space Path Loss", "path_loss"),
           new MenuItem("Link Budget", "link_budget"),
           new MenuItem("Standing Waves", "vswr"),
           new MenuItem("Fresnel Zones", "fresnel"),
           new MenuItem("Coaxial Cable Loss", "coax"),
           new MenuItem("Resonance", "resonance"),
           new MenuItem("Two-Wire Transmission Lines", "two_wire"),
           new MenuItem("Skin Depth", "skin"),
           new MenuItem("Wavelength & Frequency Conversion", "wave"),
           new MenuItem("Delta-Wye Conversion", "delta"),
           new MenuItem("Power, Energy, & Charge Conversion", "power"),
        };

    }
}
