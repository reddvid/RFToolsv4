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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RFToolsv4.Components
{
    public sealed partial class ButtonResultControl : UserControl
    {
        public ButtonResultControl()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty ButtonContentProperty =
            DependencyProperty.Register(nameof(ButtonContent), typeof(string), typeof(ButtonResultControl), new PropertyMetadata(string.Empty));
    
    
        public string ButtonContent
        {
            get => (string)GetValue(ButtonContentProperty);
            set
            {
                SetValue(ButtonContentProperty, value);
            }
        }

        public static readonly DependencyProperty CardResultsProperty =
            DependencyProperty.Register(nameof(CardResults), typeof(string), typeof(ButtonResultControl), new PropertyMetadata(string.Empty));

        public string CardResults
        {
            get => (string)GetValue(CardResultsProperty);
            set
            {
                SetValue(CardResultsProperty, value);
            }
        }
    }

}
