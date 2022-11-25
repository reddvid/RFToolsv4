using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RFToolsv4.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RFToolsv4.ViewModels
{
    public class StandingWavesViewModel : ObservableObject
    {
        public List<string> InputSelection { get; private set; }
        public ToggleResultsViewModel ToggleResultsViewModel { get; } = new ToggleResultsViewModel();

        public StandingWavesViewModel()
        {
            InputSelection = new()
            {
                "VSWR",
                "Reflection Coefficient (Γ)",
                "Return Loss"
            };

            SelectedInput = InputSelection[0];

            CalculateCommand = new RelayCommand(Calculate);
        }

        private string _selectedInput;

        public string SelectedInput
        {
            get => _selectedInput;
            set
            {
                SetProperty(ref _selectedInput, value);
                OnPropertyChanged(nameof(CanCalculate));
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


        public bool CanCalculate
        {
            get
            {
                return SelectedInput switch
                {
                    "VSWR" => InputValue > 1,
                    "Reflection Coefficient (Γ)" => InputValue > 0 && InputValue < 1,
                    "Return Loss" => InputValue > 0,
                    _ => false,
                };
            }
        }

        public ICommand CalculateCommand { get; }

        private void Calculate()
        {
            Results = Calculator.StandingWave(InputValue, SelectedInput);
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
