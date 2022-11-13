using CommunityToolkit.Mvvm.ComponentModel;
using RFToolsv4.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RFToolsv4.Models.SelectorModels;

namespace RFToolsv4.ViewModels
{
    public class TransmissionLinesViewModel : ObservableObject
    {
        public List<Preset> DielectricConstants { get; private set; }

        public TransmissionLinesViewModel()
        {
            DielectricConstants = Selectors.DielectricConstants;
        }

        private Preset _selectedPreset;

        public Preset SelectedPreset
        {
            get => _selectedPreset;
            set
            {
                SetProperty(ref _selectedPreset, value);
                if (_selectedPreset != null)
                {
                    OnPropertyChanged(nameof(PresetValueText));
                }
            }
        }

        public string PresetValueText => SelectedPreset.Value.ToString("N0");

    }
}
