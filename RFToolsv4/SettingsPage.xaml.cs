// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using CommunityToolkit.WinUI.UI.Controls;
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
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RFToolsv4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
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
            request.Data.SetText("Download RF Tools for Windows 10. #RFTools");
        }
        */

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
            await Launcher.LaunchUriAsync(new Uri("https://github.com/reddvid/RFToolsv4/issues/new?labels=enhancement"));
        }

        private async void SendFeedback_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://github.com/reddvid/RFToolsv4/issues/new?labels=bug"));
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
}
