﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        public ToggleResultsViewModel ToggleResultsViewModel { get; } = new ToggleResultsViewModel();
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
                    ToggleResultsViewModel.ToggleVisibility();
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
            ToggleResultsViewModel.ToggleVisibility();
        }

        private void SetSelectedMultiplier()
        {
            SelectedMultiplier = HeaderText switch
            {
                "Charge:" => ComboSelector[1],
                _ => ComboSelector[0],
            };
            OnPropertyChanged(nameof(SelectedMultiplier));
            ToggleResultsViewModel.ToggleVisibility();
        }

        private Flex _selectedMultiplier;

        public Flex SelectedMultiplier
        {
            get => _selectedMultiplier;
            set
            {
                SetProperty(ref _selectedMultiplier, value);
                ToggleResultsViewModel.ToggleVisibility();
            }
        }

        private List<Flex> _comboSelector;

        public List<Flex> ComboSelector
        {
            get => _comboSelector;
            set
            {
                SetProperty(ref _comboSelector, value);
                ToggleResultsViewModel.ToggleVisibility();
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
                ToggleResultsViewModel.ToggleVisibility();
            }
        }


        public bool CanCalculate => InputValue != 0;

        public ICommand CalculateCommand { get; }

        private void Calculate()
        {
            Results = Calculator.ConvertPEC(
                input: HeaderText.Contains("Power") 
                ? ConvertTodBm(InputValue)
                : InputValue * SelectedMultiplier.Multiplier,
                setting: HeaderText);

            ToggleResultsViewModel.ToggleVisibility(true);
        }

        private double ConvertTodBm(double inputPower)
        {
            return SelectedMultiplier.Caption switch
            {
                "mW" => 10 * Math.Log10(inputPower),
                "W" => 10 * Math.Log10(1_000 * inputPower),
                "dBm" => inputPower * 1,
                "dBW" => inputPower + 30,
                _ => inputPower
            };
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
