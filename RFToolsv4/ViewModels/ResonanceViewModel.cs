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
    public class ResonanceViewModel : ObservableObject
    {
        public List<string> InputSelector { get; private set; }
        public ToggleResultsViewModel ToggleResultsViewModel { get; } = new ToggleResultsViewModel();

        public ResonanceViewModel()
        {
            InputSelector = new()
            {
                "Resonant Frequency",
                "Capacitance",
                "Inductance"
            };

            SelectedInput = InputSelector[0];

            CalculateCommand = new RelayCommand(Calculate);
        }

        private string _selectedInput;

        public string SelectedInput
        {
            get => _selectedInput;
            set
            {
                SetProperty(ref _selectedInput, value);
                if (_selectedInput != null)
                {
                    SetInputHeaders();
                    SetComboSelectors();
                    SetSelectedMultipliers();
                    OnPropertyChanged(nameof(CalculateButtonText));
                    OnPropertyChanged(nameof(CanCalculate));
                    ToggleResultsViewModel.ToggleVisibility();
                }
            }
        }

        private void SetInputHeaders()
        {
            OnPropertyChanged(nameof(FirstHeader));
            OnPropertyChanged(nameof(SecondHeader));
        }

        private void SetSelectedMultipliers()
        {
            FirstSelectedMultiplier = SelectedInput switch
            {
                "Resonant Frequency" => FirstComboSelector[0],
                "Capacitance" => FirstComboSelector[1],
                "Inductance" => FirstComboSelector[1],
                _ => FirstComboSelector[0]
            };

            SecondSelectedMultiplier = SelectedInput switch
            {
                "Resonant Frequency" => SecondComboSelector[3],
                "Capacitance" => SecondComboSelector[3],
                "Inductance" => SecondComboSelector[0],
                _ => SecondComboSelector[3]
            };

            OnPropertyChanged(nameof(FirstSelectedMultiplier));
            OnPropertyChanged(nameof(SecondSelectedMultiplier));
        }

        private void SetComboSelectors()
        {
            FirstComboSelector = SelectedInput switch
            {
                "Resonant Frequency" => Selectors.Capacitance,
                "Capacitance" => Selectors.Frequencies,
                "Inductance" => Selectors.Frequencies,
                _ => Selectors.Capacitance
            };

            SecondComboSelector = SelectedInput switch
            {
                "Resonant Frequency" => Selectors.Inductance,
                "Capacitance" => Selectors.Inductance,
                "Inductance" => Selectors.Capacitance,
                _ => Selectors.Inductance
            };

            OnPropertyChanged(nameof(FirstComboSelector));
            OnPropertyChanged(nameof(SecondComboSelector));
        }

        public string CalculateButtonText => $"Calculate {SelectedInput}";

        private Flex _firstSelectedMultiplier;

        public Flex FirstSelectedMultiplier
        {
            get => _firstSelectedMultiplier;
            set
            {
                SetProperty(ref _firstSelectedMultiplier, value);
                ToggleResultsViewModel.ToggleVisibility();
            }
        }


        private List<Flex> _firstComboSelector;

        public List<Flex> FirstComboSelector
        {
            get => _firstComboSelector;
            set
            {
                SetProperty(ref _firstComboSelector, value);
                ToggleResultsViewModel.ToggleVisibility();
            }
        }

        public string FirstHeader
        {
            get
            {
                return SelectedInput switch
                {
                    "Resonant Frequency" => "Capacitance:",
                    "Capacitance" => "Resonant Frequency:",
                    "Inductance" => "Resonant Frequency:",
                    _ => "Capacitance:"
                };
            }
        }

        private Flex _secondSelectedMultiplier;

        public Flex SecondSelectedMultiplier
        {
            get => _secondSelectedMultiplier;
            set
            {
                SetProperty(ref _secondSelectedMultiplier, value);
                ToggleResultsViewModel.ToggleVisibility();
            }
        }

        private List<Flex> _secondComboSelector;

        public List<Flex> SecondComboSelector
        {
            get => _secondComboSelector;
            set
            {
                SetProperty(ref _secondComboSelector, value);
                ToggleResultsViewModel.ToggleVisibility();
            }
        }

        public string SecondHeader
        {
            get
            {
                return SelectedInput switch
                {
                    "Resonant Frequency" => "Inductance:",
                    "Capacitance" => "Inductance:",
                    "Inductance" => "Capacitance:",
                    _ => "Inductance:"
                };
            }
        }

        private double _firstInputValue;

        public double FirstInputValue
        {
            get => _firstInputValue;
            set
            {
                SetProperty(ref _firstInputValue, value);
                OnPropertyChanged(nameof(CanCalculate));
                ToggleResultsViewModel.ToggleVisibility();
            }
        }

        private double _secondInputValue;

        public double SecondInputValue
        {
            get => _secondInputValue;
            set
            {
                SetProperty(ref _secondInputValue, value);
                OnPropertyChanged(nameof(CanCalculate));
                ToggleResultsViewModel.ToggleVisibility();
            }
        }

        public bool CanCalculate => ValueTester.NaNTest(new double[] { FirstInputValue, SecondInputValue });

        public ICommand CalculateCommand { get; }

        private void Calculate()
        {
            Results = Calculator.Resonance(
                firstValue: FirstInputValue * FirstSelectedMultiplier.Multiplier,
                secondValue: SecondInputValue * SecondSelectedMultiplier.Multiplier,
                unknown: SelectedInput);

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
