using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using RFToolsv4.Helpers;
using RFToolsv4.Models;
using RFToolsv4.Views;
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
        // TODO: Simplify MenuItems
        public List<NavigationViewItem> MenuItems = new()
        {
            new NavigationViewItem() { 
                Content = "Calculators", 
                Icon = new FontIcon() { 
                    FontFamily = new Microsoft.UI.Xaml.Media.FontFamily("Segoe Fluent Icons"),
                    Glyph = "\xE1D0"
            }, MenuItemsSource = new List<MenuItem>() {
                new MenuItem("Free-Space Path Loss", typeof(FreeSpacePathLossPage)),
                new MenuItem("Link Budget", typeof(LinkBudgetPage)),
                new MenuItem("Standing Waves", typeof(StandingWavesPage)),
                new MenuItem("Fresnel Zones", typeof(FresnelZonesPage)),
            }},
            new NavigationViewItem() { 
                Content = "Converters",
                Icon = new FontIcon() {
                    FontFamily = new Microsoft.UI.Xaml.Media.FontFamily("Segoe Fluent Icons"),
                    Glyph = "\xE1CD"
            }, MenuItemsSource = new List<MenuItem>() {
                // new MenuItem("Coaxial Cable Loss", typeof(CoaxialCableLossPage)),
                new MenuItem("Resonance", typeof(ResonancePage)),
                new MenuItem("Two-Wire Transmission Lines", typeof(TransmissionLinesPage)),
                new MenuItem("Skin Depth", typeof(SkinDepthPage)),
                new MenuItem("Wavelength & Frequency", typeof(WavelengthFrequencyPage)),
                new MenuItem("Delta-Wye", typeof(DeltaWyePage)),
                new MenuItem("Power, Energy, & Charge", typeof(PECConversionPage)),
            }},
            new NavigationViewItem() { 
                Content = "Pin-outs (Coming soon)", 
                IsEnabled = false,
                Icon = new FontIcon() {
                    FontFamily = new Microsoft.UI.Xaml.Media.FontFamily("Segoe Fluent Icons"),
                    Glyph = "\xEF90"
                }, 
            },
            new NavigationViewItem() { 
                Content = "Resources (Coming soon)", 
                IsEnabled = false,
                Icon = new FontIcon() {
                    FontFamily = new Microsoft.UI.Xaml.Media.FontFamily("Segoe Fluent Icons"),
                    Glyph = "\xF571"
                },
            },
            new NavigationViewItem() {
                Content = "Dictionary (Coming soon)",
                IsEnabled = false,
                Icon = new FontIcon() {
                    FontFamily = new Microsoft.UI.Xaml.Media.FontFamily("Segoe Fluent Icons"),
                    Glyph = "\xE82D"
                },
            },
        };


        private bool _isSettingsPage;

        public bool IsSettingsPage
        {
            get => _isSettingsPage;
            set
            {
                SetProperty(ref _isSettingsPage, !value);
            }
        }

    }
}
