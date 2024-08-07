﻿using Microsoft.UI.Composition.SystemBackdrops;
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
using RFTools.Uwp.ViewModels;
using static RFTools.Uwp.Models.SelectorModels;
using System.Diagnostics;
using System.Reflection;
using RFTools.Uwp.Views;
using Windows.Storage;
using CommunityToolkit.WinUI.UI.Controls;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;
using MenuItem = RFTools.Uwp.Models.MenuItem;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using RFTools.Uwp.Constants;
using RFTools.Uwp.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RFTools.Uwp
{
    using MenuItem = Models.MenuItem;

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
            // this.SizeChanged += MainWindow_SizeChanged;

            Windowing();
            // SubClassing();
        }

        private void MainWindow_SizeChanged(object sender, WindowSizeChangedEventArgs args)
        {
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this); // m_window in App.cs
            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

#if DEBUG
            appTitleText.Text = $"{args.Size.Width} x {args.Size.Height}";
#endif

            if (args.Size.Width < 1200 && args.Size.Height >= 720)
            {
                appWindow.Resize(new Windows.Graphics.SizeInt32 { 
                    Width = 1200, 
                    Height = (int)args.Size.Height
                });
            }

            if (args.Size.Height < 720 && args.Size.Width >= 1200)
            {
                appWindow.Resize(new Windows.Graphics.SizeInt32 { 
                    Width = (int)args.Size.Width, 
                    Height = 720 
                });
            }
        }

        private void Windowing()
        {
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this); // m_window in App.cs
            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

            var size = new Windows.Graphics.SizeInt32();
            size.Width = 1200;
            size.Height = 720;

            appWindow.Resize(size);
        }

        private void MenuListView_Loaded(object sender, RoutedEventArgs e)
        {
            SelectMenuFromLastSavedItem();
        }

        private void SelectMenuFromLastSavedItem()
        {
            string savedNavigation = GetLastMenuItem();

            if (string.IsNullOrWhiteSpace(savedNavigation)) navigationView.SelectedItem = "Free-Space Path Loss";

            foreach (var menuItem in ViewModel.MenuItems)
            {
                if (savedNavigation == menuItem.Title)
                {
                    navigationView.SelectedItem = menuItem;
                    break;
                }
            }
        }

        private static string GetLastMenuItem()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            return localSettings.Values["navigation"] as string;
        }

        #region WINDOW_CONTROL
        private delegate IntPtr WinProc(IntPtr hWnd, PInvoke.User32.WindowMessage Msg, IntPtr wParam, IntPtr lParam);
        private WinProc newWndProc = null;
        private IntPtr oldWndProc = IntPtr.Zero;
        [DllImport("user32")]
        private static extern IntPtr SetWindowLong(IntPtr hWnd, PInvoke.User32.WindowLongIndexFlags nIndex, WinProc newProc);
        [DllImport("user32.dll")]
        static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, PInvoke.User32.WindowMessage Msg, IntPtr wParam, IntPtr lParam);

        private void SubClassing()
        {
            //Get the Window's HWND
            var hwnd = this.As<IWindowNative>().WindowHandle;

            newWndProc = new WinProc(NewWindowProc);
            oldWndProc = SetWindowLong(hwnd, PInvoke.User32.WindowLongIndexFlags.GWL_WNDPROC, newWndProc);
        }

        int MinWidth = 1200;
        int MinHeight = 600;

        [StructLayout(LayoutKind.Sequential)]
        struct MINMAXINFO
        {
            public PInvoke.POINT ptReserved;
            public PInvoke.POINT ptMaxSize;
            public PInvoke.POINT ptMaxPosition;
            public PInvoke.POINT ptMinTrackSize;
            public PInvoke.POINT ptMaxTrackSize;
        }

        private IntPtr NewWindowProc(IntPtr hWnd, PInvoke.User32.WindowMessage Msg, IntPtr wParam, IntPtr lParam)
        {
            switch (Msg)
            {
                case PInvoke.User32.WindowMessage.WM_GETMINMAXINFO:
                    var dpi = PInvoke.User32.GetDpiForWindow(hWnd);
                    float scalingFactor = (float)dpi / 96;

                    MINMAXINFO minMaxInfo = Marshal.PtrToStructure<MINMAXINFO>(lParam);
                    minMaxInfo.ptMinTrackSize.x = (int)(MinWidth * scalingFactor);
                    minMaxInfo.ptMinTrackSize.y = (int)(MinHeight * scalingFactor);
                    Marshal.StructureToPtr(minMaxInfo, lParam, true);
                    break;

            }
            return CallWindowProc(oldWndProc, hWnd, Msg, wParam, lParam);
        }


        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("EECDBF0E-BAE9-4CB6-A68E-9598E1CB57BB")]
        internal interface IWindowNative
        {
            IntPtr WindowHandle { get; }
        }

        bool TrySetSystemBackdrop()
        {
            if (Microsoft.UI.Composition.SystemBackdrops.MicaController.IsSupported())
            {
                m_wsdqHelper = new WindowsSystemDispatcherQueueHelper();
                m_wsdqHelper.EnsureWindowsSystemDispatcherQueueController();

                // Create the policy object.
                m_configurationSource = new SystemBackdropConfiguration();
                this.Activated += Window_Activated;
                this.Closed += Window_Closed;
                ((FrameworkElement)this.Content).ActualThemeChanged += Window_ThemeChanged;

                // Initial configuration state.
                m_configurationSource.IsInputActive = true;
                SetConfigurationSourceTheme();

                m_backdropController = new Microsoft.UI.Composition.SystemBackdrops.MicaController();

                // Enable the system backdrop.
                // Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
                m_backdropController.AddSystemBackdropTarget(this.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
                m_backdropController.SetSystemBackdropConfiguration(m_configurationSource);
                return true; // succeeded
            }

            return false; // Mica is not supported on this system
        }

        private void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
            m_configurationSource.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            // Make sure any Mica/Acrylic controller is disposed
            // so it doesn't try to use this closed window.
            if (m_backdropController != null)
            {
                m_backdropController.Dispose();
                m_backdropController = null;
            }
            this.Activated -= Window_Activated;
            m_configurationSource = null;
        }

        private void Window_ThemeChanged(FrameworkElement sender, object args)
        {
            if (m_configurationSource != null)
            {
                SetConfigurationSourceTheme();
            }
        }

        private void SetConfigurationSourceTheme()
        {
            switch (((FrameworkElement)this.Content).ActualTheme)
            {
                case ElementTheme.Dark: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Dark; break;
                case ElementTheme.Light: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Light; break;
                case ElementTheme.Default: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Default; break;
            }
        }

        #endregion

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
                ViewModel.IsSettingsPage = false;
                if (nav.SelectedItem as MenuItem == null)
                {
                    ViewModel.IsSettingsPage = true;
                    InfoCards.Visibility = Visibility.Collapsed;
                    AboutCards.Visibility = Visibility.Visible;
                    return;
                }
                string item = (nav.SelectedItem as MenuItem).Title;
                NavigateFrame(item);
                SaveMenuItem(item);
                SetInfoCards(item);
                InfoCards.Visibility = Visibility.Visible;
                AboutCards.Visibility = Visibility.Collapsed;
            }
        }

        private void SetInfoCards(string item)
        {
            string tag = item switch
            {
                "Free-Space Path Loss" => "PathLoss",
                "Link Budget" => "LinkBudget",
                "Standing Waves" => "VSWR",
                "Fresnel Zones" => "FresnelZone",
                "Resonance" => "Resonance",
                "Skin Depth" => "SkinDepth",
                "Wavelength & Frequency Conversion" => "Wavelength",
                "Delta-Wye Conversion" => "DeltaWye",
                "Power, Energy, & Charge Conversion" => "PEC",
                _ => "PathLoss"
            };

            SetToolDefinition(tag);
            SetToolLinks(tag);
        }

        private void SetToolLinks(string tool)
        {
            // Clear linksPanel
            linksPanel.Children.Clear();

            // Add card title to stackpanel
            linksPanel.Children.Add(new TextBlock()
            {
                Text = "Useful Links",
                Style = (Style)Application.Current.Resources["CardTitleTextStyle"]
            });

            // Get links
            List<ToolLink> links = ToolLinks.UriLinks;
            if (links != null && links.Count != 0 && links.Any(x => x.Tool == tool))
            {
                foreach (ToolLink link in links.Where(x => x.Tool == tool))
                {
                    linksPanel.Children.Add(new HyperlinkButton()
                    {
                        Content = link.Title,
                        NavigateUri = link.Link
                    });
                }

                linksPanel.Visibility = Visibility.Visible;
            }
            else
            {
                linksPanel.Visibility = Visibility.Collapsed;
                return;
            }
        }

        private void SetToolDefinition(string resourceName)
        {          
            string localizedMessage = (string)Application.Current.Resources[resourceName];
            txbDefinition.Text = localizedMessage;
        }

        private static void SaveMenuItem(string item)
        {
            Debug.WriteLine($"Saving {item}");
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["navigation"] = item;
        }

        private void NavigateFrame(string item)
        {
            if (mainFrame == null || item == null) return;
            foreach (var menuItem in ViewModel.MenuItems)
            {
                if (item == menuItem.Title)
                {
                    mainFrame.Navigate(menuItem.Page);
                    break;
                }
            }
        }



        private async void ChangelogMD_Loaded(object sender, RoutedEventArgs e)
        {
            var markdown = (sender as MarkdownTextBlock);
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            var file = @"CHANGELOG.md";
            var path = Path.Combine(directory, file);
            markdown.Text = await File.ReadAllTextAsync(path);
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
