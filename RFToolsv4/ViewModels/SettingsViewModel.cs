using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml;
using RFToolsv4.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFToolsv4.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        private readonly IThemeSelectorService _themeSelectorService;

        public SettingsViewModel(IThemeSelectorService themeSelectorService)
        {
            _themeSelectorService = themeSelectorService;
        }

        private string _selectedTheme;

        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                SetProperty(ref _selectedTheme, value);
                if (_selectedTheme != null)
                {
                    UpdateTheme(_selectedTheme);
                }
            }
        }


        private void UpdateTheme(string themeName)
        {
            if (Enum.TryParse(themeName, out ElementTheme theme) is true)
            {
                _themeSelectorService.SetTheme(theme);
            }
        }
    }
}
