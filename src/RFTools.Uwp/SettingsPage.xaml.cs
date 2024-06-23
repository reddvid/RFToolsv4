// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using RFTools.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using RFTools.Uwp.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RFTools.Uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
       public SettingsViewModel ViewModel { get; }
        public SettingsPage()
        {
            this.InitializeComponent();
            ViewModel = Ioc.Default.GetRequiredService<SettingsViewModel>();
        }

        /*
        private void ShareTextHandler(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.Properties.Title = "Share RF Tools for Windows";
            request.Data.Properties.Description = "Share RF Tools";
            request.Data.Properties.ApplicationName = "RF Tools";
            request.Data.Properties.PackageFamilyName = "32760RedDavid.RFTools_7nbw6tjv9ct6w";

            request.Data.SetWebLink(new Uri("	https://www.microsoft.com/store/apps/9nblggh41btt"));
            request.Data.SetApplicationLink(new Uri("	https://www.microsoft.com/store/apps/9nblggh41btt"));
            request.Data.SetText("Download RF Tools for Windows 10. #RFTools.Uwp");
        }
        */

        

        private void ThemeBox_Loaded(object sender, RoutedEventArgs e)
        {
            var themeComboBox = sender as ComboBox;
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            var theme = localSettings.Values["theme"] as string;
            themeComboBox.SelectedItem = theme;
        }

        private void ThemeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var theme = (sender as ComboBox).SelectedItem as string;
            ChangeTheme(theme);
            SaveTheme(theme);
        }

        private void SaveTheme(string theme)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["theme"] = theme;
        }

        private void ChangeTheme(string theme)
        {
            ViewModel.SelectedTheme = theme;
        }

        private async void MoreAppsButton_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store://publisher/?name=Red David"));
        }

        private void ShareApp_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

        private async void RateApp_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store:REVIEW?PFN=32760RedDavid.RFTools_7nbw6tjv9ct6w"));
        }

        private async void RequestFeature_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://github.com/reddvid/RFTools.Uwp/issues/new?labels=enhancement"));
        }

        private async void SendFeedback_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://github.com/reddvid/RFTools.Uwp/issues/new?labels=bug"));
        }
    }
}
