using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RFToolsv4.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static RFToolsv4.Models.SelectorModels;

namespace RFToolsv4.ViewModels
{
    public class TransmissionLinesViewModel : ObservableObject
    {
        public List<Preset> DielectricConstants { get; private set; }
        public List<Flex> Diameters { get; private set; }
        public ToggleResultsViewModel ToggleResultsViewModel { get; } = new ToggleResultsViewModel();

        public TransmissionLinesViewModel()
        {
            DielectricConstants = Selectors.DielectricConstants;
            Diameters = Selectors.Diamters;

            SelectedPreset = DielectricConstants[1];
            DiameterMultiplier = Diameters[2];

            CalculateCommand = new RelayCommand(Calculate);
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
                    OnPropertyChanged(nameof(CanCalculate));
                    ToggleResultsViewModel.ToggleVisibility();
                }
            }
        }

        private Flex _diameterMultiplier;

        public Flex DiameterMultiplier
        {
            get => _diameterMultiplier;
            set
            {
                SetProperty(ref _diameterMultiplier, value);
                if (_diameterMultiplier != null)
                {
                    OnPropertyChanged(nameof(PresetValueText));
                    OnPropertyChanged(nameof(CanCalculate));
                    ToggleResultsViewModel.ToggleVisibility();
                }
            }
        }

        public string PresetValueText => SelectedPreset.Value.ToString("N0");

        private double _impedanceValue;

        public double ImpedanceValue
        {
            get => _impedanceValue;
            set
            {
                SetProperty(ref _impedanceValue, value);
                if (_impedanceValue != 0)
                {
                    OnPropertyChanged(nameof(ImpedanceValue));
                    OnPropertyChanged(nameof(CanCalculate));
                    ToggleResultsViewModel.ToggleVisibility();
                }
            }
        }

        private double _diameterValue;

        public double DiameterValue
        {
            get => _diameterValue;
            set
            {
                SetProperty(ref _diameterValue, value);
                if (_diameterValue != 0)
                {
                    OnPropertyChanged(nameof(DiameterValue));
                    OnPropertyChanged(nameof(CanCalculate));
                    ToggleResultsViewModel.ToggleVisibility();
                }
            }
        }
        public bool CanCalculate => ImpedanceValue != 0 && DiameterValue != 0;

        public ICommand CalculateCommand { get; }

        private void Calculate()
        {
            Results = Calculator.TwoWireLine(ImpedanceValue, DiameterValue, SelectedPreset.Value, DiameterMultiplier.Caption);

            ToggleResultsViewModel.ToggleVisibility(true);
        }

        private string _results;

        public string Results
        {
            get => _results;
            private set
            {
                SetProperty(ref _results, value);
            }
        }
    }
}
