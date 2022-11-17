using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using RFToolsv4.Helpers;
using RFToolsv4.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using static PInvoke.User32;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RFToolsv4
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            Ioc.Default.ConfigureServices(ConfigureServices());
        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>(); 
            services.AddSingleton<SettingsViewModel>();
            return services.BuildServiceProvider();
        }

        private void InitializeTheme()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            var theme = localSettings.Values["theme"] as string;

            if (string.IsNullOrWhiteSpace(theme))
            {
                localSettings.Values["theme"] = "Default";
            }

            SetTheme(theme);
        }

        private void SetTheme(string theme) 
        {
            if (App.Window?.Content is FrameworkElement frameworkElement)
            {
                frameworkElement.RequestedTheme = theme switch
                {
                    "Dark" => ElementTheme.Dark,
                    "Light" => ElementTheme.Light,
                    _ => ElementTheme.Default,
                }; 
            };
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            Window = new MainWindow();

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(Window);

            SetWindowDetails(hwnd, 1200, 600);

            InitializeTheme();

            Window.Activate();
        }


        public static Window Window { get; private set; }

        private static void SetWindowDetails(IntPtr hwnd, int width, int height)
        {
            var dpi = GetDpiForWindow(hwnd);
            float scalingFactor = (float)dpi / 96;
            width = (int)(width * scalingFactor);
            height = (int)(height * scalingFactor);

            _ = SetWindowPos(hwnd, SpecialWindowHandles.HWND_TOP,
                                        0, 0, width, height,
                                        SetWindowPosFlags.SWP_NOMOVE);
            _ = SetWindowLong(hwnd,
                   WindowLongIndexFlags.GWL_STYLE,
                   (SetWindowLongFlags)(GetWindowLong(hwnd,
                      WindowLongIndexFlags.GWL_STYLE) &
                      ~(int)SetWindowLongFlags.WS_MINIMIZEBOX &
                      ~(int)SetWindowLongFlags.WS_MAXIMIZEBOX));
        }
    }
}
