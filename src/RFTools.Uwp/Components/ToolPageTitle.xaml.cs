﻿using Microsoft.UI.Xaml;
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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RFTools.Uwp.Components
{
    public sealed partial class ToolPageTitle : UserControl
    {
        public ToolPageTitle()
        {
            this.InitializeComponent();
        }


        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register(nameof(HeaderText), typeof(string), typeof(ToolPageTitle), new PropertyMetadata(string.Empty));

        public string HeaderText
        {
            get => (string)GetValue(HeaderTextProperty);
            set
            {
                SetValue(HeaderTextProperty, value);
            }
        }
    }
}
