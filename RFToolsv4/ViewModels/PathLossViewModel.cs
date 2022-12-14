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
    public class PathLossViewModel : ObservableObject
    {
        public List<Flex> Distance { get; private set; } = Selectors.Distance;
        public List<Flex> Frequency { get; private set; } = Selectors.LargeFrequency;
        public ToggleResultsViewModel ToggleResultsViewModel { get; } = new ToggleResultsViewModel();

        public PathLossViewModel()
        {
            CalculateCommand = new RelayCommand(Calculate);

            SetSelectedMultipliers();
        }

        private void SetSelectedMultipliers()
        {
            DistanceMultiplier = Distance[1];
            FrequencyMultiplier = Frequency[2];

            OnPropertyChanged(nameof(DistanceMultiplier));
            OnPropertyChanged(nameof(FrequencyMultiplier));
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

        public bool CanCalculate => TransmitterGainValue >= 0
                                    && ReceiverGainValue >= 0
                                    && ValueTester.NaNTest(new double[] {
                                        DistanceValue, FrequencyValue
                                    });

        public ICommand CalculateCommand { get; }

        private void Calculate()
        {
            Results = Calculator.PathLoss(txGain: TransmitterGainValue,
                                          rxGain: ReceiverGainValue,
                                          distance: DistanceValue * DistanceMultiplier.Multiplier / 1_000,
                                          frequency: FrequencyValue * FrequencyMultiplier.Multiplier / 1_000_000);

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
