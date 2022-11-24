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
    public class LinkBudgetViewModel : ObservableObject
    {
        public List<Flex> OutputPower { get; private set; } = Selectors.OutputPower;
        public List<Flex> Distance { get; private set; } = Selectors.Distance;
        public List<Flex> Frequency { get; private set; } = Selectors.LargeFrequency;
        public ToggleResultsViewModel ToggleResultsViewModel { get; } = new ToggleResultsViewModel();

        public LinkBudgetViewModel()
        {
            CalculateCommand = new RelayCommand(Calculate);

            SetSelectedMultipliers();
        }

        private void SetSelectedMultipliers()
        {
            DistanceMultiplier = Distance[1];
            FrequencyMultiplier = Frequency[2];
            PowerMultiplier = OutputPower[3];

            OnPropertyChanged(nameof(DistanceMultiplier));
            OnPropertyChanged(nameof(FrequencyMultiplier));
            OnPropertyChanged(nameof(PowerMultiplier));
        }

        private Flex _powerMultiplier;

        public Flex PowerMultiplier
        {
            get => _powerMultiplier;
            set
            {
                SetProperty(ref _powerMultiplier, value);
                OnPropertyChanged(nameof(CanCalculate));
                ToggleResultsViewModel.ToggleVisibility();
            }
        }

        private Flex _distanceMultiplier;

        public Flex DistanceMultiplier
        {
            get => _distanceMultiplier;
            set
            {
                SetProperty(ref _distanceMultiplier, value);
                OnPropertyChanged(nameof(CanCalculate));
                ToggleResultsViewModel.ToggleVisibility();
            }
        }

        private Flex _frequencyMultiplier;

        public Flex FrequencyMultiplier
        {
            get => _frequencyMultiplier;
            set
            {
                SetProperty(ref _frequencyMultiplier, value);
                OnPropertyChanged(nameof(CanCalculate));
                ToggleResultsViewModel.ToggleVisibility();
            }
        }

        private double _distanceValue;

        public double DistanceValue
        {
            get => _distanceValue;
            set
            {
                SetProperty(ref _distanceValue, value);
                OnPropertyChanged(nameof(CanCalculate));
                ToggleResultsViewModel.ToggleVisibility();
            }
        }

        private double _frequencyValue;

        public double FrequencyValue
        {
            get => _frequencyValue;
            set
            {
                SetProperty(ref _frequencyValue, value);
                OnPropertyChanged(nameof(CanCalculate));
                ToggleResultsViewModel.ToggleVisibility();
            }
        }

        private double _outputPowerValue;

        public double OutputPowerValue
        {
            get => _outputPowerValue;
            set
            {
                SetProperty(ref _outputPowerValue, value);
                OnPropertyChanged(nameof(CanCalculate));
                ToggleResultsViewModel.ToggleVisibility();
            }
        }

        private double _transmitLossValue;

        public double TransmitLossValue
        {
            get => _transmitLossValue;
            set
            {
                SetProperty(ref _transmitLossValue, value);
                OnPropertyChanged(nameof(CanCalculate));
                ToggleResultsViewModel.ToggleVisibility();
            }
        }

        private double _receiveLossValue;

        public double ReceiveLossValue
        {
            get => _receiveLossValue;
            set
            {
                SetProperty(ref _receiveLossValue, value);
                OnPropertyChanged(nameof(CanCalculate));
                ToggleResultsViewModel.ToggleVisibility();
            }
        }

        private double _transmitterGainValue;

        public double TransmitterGainValue
        {
            get => _transmitterGainValue;
            set
            {
                SetProperty(ref _transmitterGainValue, value);
                OnPropertyChanged(nameof(CanCalculate));
                ToggleResultsViewModel.ToggleVisibility();
            }
        }

        private double _receiverGainValue;

        public double ReceiverGainValue
        {
            get => _receiverGainValue;
            set
            {
                SetProperty(ref _receiverGainValue, value);
                OnPropertyChanged(nameof(CanCalculate));
                ToggleResultsViewModel.ToggleVisibility();
            }
        }

        public bool CanCalculate => OutputPowerValue != 0.0
                                    && TransmitterGainValue >= 0
                                    && TransmitLossValue >= 0
                                    && ReceiverGainValue >= 0
                                    && ReceiveLossValue >= 0
                                    && DistanceValue != 0
                                    && FrequencyValue != 0;

        public ICommand CalculateCommand { get; }

        private void Calculate()
        {
            Results = Calculator.LinkBudget(txPower: ConvertTodBm(OutputPowerValue),
                                            txGain: TransmitterGainValue,
                                            txLoss: TransmitLossValue,
                                            rxGain: ReceiverGainValue,
                                            rxLoss: ReceiveLossValue,
                                            distance: DistanceValue * DistanceMultiplier.Multiplier / 1_000,
                                            frequency: FrequencyValue * FrequencyMultiplier.Multiplier / 1_000_000);

            ToggleResultsViewModel.ToggleVisibility(true);
        }


        private double ConvertTodBm(double outputPowerValue)
        {
            return PowerMultiplier.Caption switch
            {
                "dBm" => outputPowerValue * 1,
                "mW" => 10 * Math.Log10(outputPowerValue),
                "W" => 10 * Math.Log10(1_000 * outputPowerValue),
                "kW" => 10 * Math.Log10(outputPowerValue * 1_000_000),
                _ => outputPowerValue,
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
