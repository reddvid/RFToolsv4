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
    public class PECViewModel : ObservableObject
    {
        public List<string> ConversionInput { get; private set; }
        public PECViewModel()
        {
            ConversionInput = new()
            {
                "Power",
                "Energy",
                "Charge"
            };
            SelectedInput = ConversionInput[0];

            CalculateCommand = new RelayCommand(Calculate);
        }

        public string HeaderText => $"{SelectedInput}:";

        private string _selectedInput;

        public string SelectedInput
        {
            get => _selectedInput;
            set
            {
                SetProperty(ref _selectedInput, value);
                if (_selectedInput != null)
                {
                    OnPropertyChanged(nameof(HeaderText));
                    SetComboSelector();
                    SetSelectedMultiplier();
                }
            }
        }

        private void SetComboSelector()
        {
            ComboSelector = HeaderText switch
            {
                "Energy:" => Selectors.Energy,
                "Charge:" => Selectors.Charge,
                "Power:" => Selectors.Power,
                _ => Selectors.Power
            };
            OnPropertyChanged(nameof(ComboSelector));
        }

        private void SetSelectedMultiplier()
        {
            SelectedMultiplier = HeaderText switch
            {
                "Charge" => ComboSelector[1],
                _ => ComboSelector[0],
            };
            OnPropertyChanged(nameof(SelectedMultiplier));
        }

        private Flex _selectedMultiplier;

        public Flex SelectedMultiplier
        {
            get => _selectedMultiplier;
            set
            {
                SetProperty(ref _selectedMultiplier, value);
            }
        }

        private List<Flex> _comboSelector;

        public List<Flex> ComboSelector
        {
            get => _comboSelector;
            set
            {
                SetProperty(ref _comboSelector, value);
            }
        }

        private double _inputValue;

        public double InputValue
        {
            get => _inputValue;
            set
            {
                SetProperty(ref _inputValue, value);
                OnPropertyChanged(nameof(CanCalculate));
            }
        }


        public bool CanCalculate => InputValue != 0;

        public ICommand CalculateCommand { get; }

        private void Calculate()
        {
            Results = Calculator.ConvertPEC(
                input: InputValue * SelectedMultiplier.Multiplier,
                setting: HeaderText);
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
