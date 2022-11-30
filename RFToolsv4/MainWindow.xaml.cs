using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WinRT;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using RFToolsv4.ViewModels;
using static RFToolsv4.Models.SelectorModels;
using System.Diagnostics;
using System.Reflection;
using RFToolsv4.Views;
using Windows.Storage;
using RFToolsv4.Models;
using CommunityToolkit.WinUI.UI.Controls;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;
using MenuItem = RFToolsv4.Models.MenuItem;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RFToolsv4
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        WindowsSystemDispatcherQueueHelper m_wsdqHelper; // See below for implementation.
        MicaController m_backdropController;
        SystemBackdropConfiguration m_configurationSource;

        public MainWindow()
        {
            this.InitializeComponent();

            Title = "RF Tools";
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(appTitleBar);
            // SubClassing();
        }

        private void MenuListView_Loaded(object sender, RoutedEventArgs e)
        {
            SelectMenuFromLastSavedItem();
        }

        private void SelectMenuFromLastSavedItem()
        {
            string savedNavigation = GetLastMenuItem();

            if (string.IsNullOrWhiteSpace(savedNavigation)) navigationView.SelectedItem = "Free-Space Path Loss";
                    
            foreach (NavigationViewItem menuGroup in ViewModel.MenuItems)
            {
                var list = menuGroup.MenuItemsSource as List<MenuItem>;
                if (list == null) return;

                foreach (MenuItem menuItem in list)
                {
                    if (savedNavigation == menuItem.Title)
                    {
                        navigationView.SelectedItem = menuItem;
                        break;
                    }
                }
            }
        }

        private static string GetLastMenuItem()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            return localSettings.Values["navigation"] as string;
        }

        private void MenuListView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var nav = sender as NavigationView;
            if (args.IsSettingsSelected)
            {
                // Navigate Frame to Settings Page
                ViewModel.IsSettingsPage = true;
                mainFrame.Navigate(typeof(SettingsPage));
                InfoCards.Visibility = Visibility.Collapsed;
                AboutCards.Visibility = Visibility.Visible;
            }
            else
            {
                // TODO: Fix cards and navigation
                ViewModel.IsSettingsPage = false;
                if (nav.SelectedItem as NavigationViewItem != null && (nav.SelectedItem as NavigationViewItem).MenuItems != null)
                {
                    ViewModel.IsSettingsPage = true;
                    InfoCards.Visibility = Visibility.Collapsed;
                    AboutCards.Visibility = Visibility.Visible;
                    return;
                }

                if ((nav.SelectedItem as MenuItem) == null)
                {
                    ViewModel.IsSettingsPage = true;
                    InfoCards.Visibility = Visibility.Collapsed;
                    AboutCards.Visibility = Visibility.Visible;
                    return;
                }

                string item = (nav.SelectedItem as MenuItem).Title;
                NavigateFrame(nav.SelectedItem as MenuItem);
                SaveMenuItem(item);
                SetInfoCards(item);
                InfoCards.Visibility = Visibility.Visible;
                AboutCards.Visibility = Visibility.Collapsed;
            }
        }

        private void SetInfoCards(string item)
        {
            string resourceName = item switch
            {
                "Free-Space Path Loss" => "PathLoss",
                "Link Budget" => "LinkBudget",
                "Standing Waves" => "VSWR",
                "Fresnel Zones" => "FresnelZone",
                "Resonance" => "Resonance",
                "Skin Depth" => "SkinDepth",
                "Wavelength & Frequency Conversion" => "Wavelength",
                "Two-Wire Transmission Lines" => "TwoWireLines",
                "Delta-Wye Conversion" => "DeltaWye",
                "Power, Energy, & Charge Conversion" => "PEC",
                _ => "PathLoss"
            };

            string localizedMessage = (string)Application.Current.Resources[resourceName];
            txbDefinition.Text = localizedMessage;
        }

        private static void SaveMenuItem(string item)
        {
            Debug.WriteLine($"Saving {item}");
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["navigation"] = item;
        }

        private void NavigateFrame(MenuItem item)
        {
            if (mainFrame == null || item == null) return;
            foreach (NavigationViewItem menuGroup in ViewModel.MenuItems)
            {
                var list = menuGroup.MenuItemsSource;
                foreach (MenuItem menuItem in menuGroup.MenuItems)
                {
                    if (menuItem.Title == item.Title)
                    {
                        mainFrame.Navigate(menuItem.Page);
                        break;
                    }
                }
            }
        }

        private void NavigateFrame(string item)
        {
            if (mainFrame == null || item == null) return;
            // TODO: Improve simplification/use ViewModel
            var targetTypePage = item switch
            {
                "Free-Space Path Loss" => typeof(FreeSpacePathLossPage),
                "Link Budget" => typeof(LinkBudgetPage),
                "Standing Waves" => typeof(StandingWavesPage),
                "Fresnel Zones" => typeof(FresnelZonesPage),
                "Resonance" => typeof(ResonancePage),
                "Skin Depth" => typeof(SkinDepthPage),
                "Wavelength & Frequency Conversion" => typeof(WavelengthFrequencyPage),
                "Two-Wire Transmission Lines" => typeof(TransmissionLinesPage),
                "Delta-Wye Conversion" => typeof(DeltaWyePage),
                "Power, Energy, & Charge Conversion" => typeof(PECConversionPage),
                _ => typeof(FreeSpacePathLossPage)
            };
            mainFrame.Navigate(targetTypePage);
        }

        private async void ChangelogMD_Loaded(object sender, RoutedEventArgs e)
        {
            var markdown = (sender as MarkdownTextBlock);
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            var file = @"CHANGELOG.md";
            var path = Path.Combine(directory, file);
            markdown.Text = await File.ReadAllTextAsync(path);
        }

        private void NewToolWindow_Click(object sender, RoutedEventArgs e)
        {
            // C# code to create a new window
            //var newWindow = WindowHelper.CreateWindow();
            //var rootPage = new NavigationRootPage();
            //rootPage.RequestedTheme = ThemeHelper.RootTheme;
            //newWindow.Content = rootPage;
            //newWindow.Activate();

            // C# code to navigate in the new window           
            //var targetPageType = item switch
            //{
            //    "Free-Space Path Loss" => typeof(FreeSpacePathLossPage),
            //    "Link Budget" => typeof(LinkBudgetPage),
            //    "Standing Waves" => typeof(StandingWavesPage),
            //    "Fresnel Zones" => typeof(FresnelZonesPage),
            //    "Resonance" => typeof(ResonancePage),
            //    "Skin Depth" => typeof(SkinDepthPage),
            //    "Two-Wire Transmission Lines" => typeof(TransmissionLinesPage),
            //    "Wavelength & Frequency Conversion" => typeof(WavelengthFrequencyPage),
            //    "Delta-Wye Conversion" => typeof(DeltaWyePage),
            //    "Power, Energy, & Charge Conversion" => typeof(PECConversionPage),
            //    _ => typeof(FreeSpacePathLossPage)
            //};

            //string targetPageArguments = string.Empty;
            //rootPage.Navigate(targetPageType, targetPageArguments);
        }
    }

    class WindowsSystemDispatcherQueueHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        struct DispatcherQueueOptions
        {
            internal int dwSize;
            internal int threadType;
            internal int apartmentType;
        }

        [DllImport("CoreMessaging.dll")]
        private static extern int CreateDispatcherQueueController([In] DispatcherQueueOptions options, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object dispatcherQueueController);

        object m_dispatcherQueueController = null;
        public void EnsureWindowsSystemDispatcherQueueController()
        {
            if (Windows.System.DispatcherQueue.GetForCurrentThread() != null)
            {
                // one already exists, so we'll just use it.
                return;
            }

            if (m_dispatcherQueueController == null)
            {
                DispatcherQueueOptions options;
                options.dwSize = Marshal.SizeOf(typeof(DispatcherQueueOptions));
                options.threadType = 2;    // DQTYPE_THREAD_CURRENT
                options.apartmentType = 2; // DQTAT_COM_STA

                CreateDispatcherQueueController(options, ref m_dispatcherQueueController);
            }
        }
    }
}
