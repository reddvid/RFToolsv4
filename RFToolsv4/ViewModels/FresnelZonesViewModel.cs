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
    public class FresnelZonesViewModel : ObservableObject
    {
        public List<Flex> Distance { get; private set; } = Selectors.Distance;
        public List<Flex> Frequency { get; private set; } = Selectors.LargeFrequency;
        public ToggleResultsViewModel ToggleResultsViewModel { get; } = new ToggleResultsViewModel();

        public FresnelZonesViewModel()
        {
            CalculateCommand = new RelayCommand(Calculate);

            SetSelectedMultipliers();
        }

        private void SetSelectedMultipliers()
        {
            DistanceMultiplier = Distance[1];
            FrequencyMultiplier = Frequency[1];

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

        private double _obstructionValue;

        public double ObstructionValue
        {
            get => _obstructionValue;
            set
            {
                SetProperty(ref _obstructionValue, value);
                OnPropertyChanged(nameof(CanCalculate));
                ToggleResultsViewModel.ToggleVisibility();
            }
        }

        private double _antennaeHeightValue;

        public double AntennaeHeightValue
        {
            get => _antennaeHeightValue;
            set
            {
                SetProperty(ref _antennaeHeightValue, value);
                OnPropertyChanged(nameof(CanCalculate));
                ToggleResultsViewModel.ToggleVisibility();
            }
        }

        private bool _isObstructed;

        public bool IsObstructed
        {
            get => _isObstructed;
            set
            {
                SetProperty(ref _isObstructed, value);
                OnPropertyChanged(nameof(CanCalculate));
                ToggleResultsViewModel.ToggleVisibility();
            }
        }



        public bool CanCalculate => (!IsObstructed)
                                    ? DistanceValue != 0 && FrequencyValue != 0
                                    : DistanceValue != 0 && FrequencyValue != 0
                                    && ObstructionValue <= DistanceValue && ObstructionValue >= 0;

        public ICommand CalculateCommand { get; }

        private void Calculate()
        {
            Results = Calculator.FresnelZone(DistanceValue * DistanceMultiplier.Multiplier, FrequencyValue * FrequencyMultiplier.Multiplier, ObstructionValue * DistanceMultiplier.Multiplier);

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
