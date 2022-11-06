using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization.NumberFormatting;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RFToolsv4.Components
{
    public sealed partial class NumberBoxUnit : UserControl
    {
       

        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register(nameof(HeaderText), typeof(string), typeof(NumberBoxUnit), new PropertyMetadata(string.Empty));

        public string HeaderText
        {
            get => (string)GetValue(HeaderTextProperty);
            set
            {
                SetValue(HeaderTextProperty, value);
            }
        }

        public static readonly DependencyProperty NumberBoxFormatterProperty =
            DependencyProperty.Register(nameof(NumberBoxFormatter), typeof(INumberFormatter2), typeof(NumberBoxUnit), new PropertyMetadata(new DecimalFormatter() { FractionDigits = 2 }));

        public INumberFormatter2 NumberBoxFormatter
        {
            get => (INumberFormatter2)GetValue(NumberBoxFormatterProperty);
            set
            {
                SetValue(NumberBoxFormatterProperty, value);
            }
        }

        public static readonly DependencyProperty NumberBoxValueProperty =
            DependencyProperty.Register(nameof(NumberBoxValue), typeof(double), typeof(NumberBoxUnit), new PropertyMetadata(0));

        public double NumberBoxValue
        {
            get => (double)GetValue(NumberBoxValueProperty);
            set
            {
                SetValue(NumberBoxValueProperty, value);
            }
        }

        public static readonly DependencyProperty ComboBoxWidthProperty =
           DependencyProperty.Register(nameof(ComboBoxWidth), typeof(double), typeof(NumberBoxUnit), new PropertyMetadata(120.0));

        public double ComboBoxWidth
        {
            get => (double)GetValue(ComboBoxWidthProperty);
            set
            {
                SetValue(ComboBoxWidthProperty, value);
            }
        }

        public static readonly DependencyProperty ComboBoxDisplayMemberPathProperty =
           DependencyProperty.Register(nameof(ComboBoxDisplayMemberPath), typeof(string), typeof(NumberBoxUnit), new PropertyMetadata(string.Empty));

        public string ComboBoxDisplayMemberPath
        {
            get => (string)GetValue(ComboBoxDisplayMemberPathProperty);
            set
            {
                SetValue(ComboBoxDisplayMemberPathProperty, value);
            }
        }

        public static readonly DependencyProperty ComboBoxItemsSourceProperty =
           DependencyProperty.Register(nameof(ComboBoxItemsSource), typeof(IEnumerable), typeof(NumberBoxUnit), new PropertyMetadata(null));

        public IEnumerable ComboBoxItemsSource
        {
            get => (IEnumerable)GetValue(ComboBoxItemsSourceProperty);
            set
            {
                SetValue(ComboBoxItemsSourceProperty, value);
            }
        }

        public static readonly DependencyProperty ComboBoxSelectedItemProperty =
          DependencyProperty.Register(nameof(ComboBoxSelectedItem), typeof(object), typeof(NumberBoxUnit), new PropertyMetadata(null));

        public object ComboBoxSelectedItem
        {
            get => (object)GetValue(ComboBoxSelectedItemProperty);
            set
            {
                SetValue(ComboBoxSelectedItemProperty, value);
            }
        }

        public NumberBoxUnit()
        {
            this.InitializeComponent();
        }

    }
}
