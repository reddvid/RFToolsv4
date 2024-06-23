using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RFTools.Uwp.Helpers;
using RFTools.Uwp.Models;

namespace RFTools.Uwp.ViewModels;

public class FresnelZonesViewModel : ObservableObject
{
    public List<SelectorModels.Flex> Distance { get; private set; } = Selectors.Distance;
    public List<SelectorModels.Flex> Frequency { get; private set; } = Selectors.LargeFrequency;
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

    private SelectorModels.Flex _distanceMultiplier;

    public SelectorModels.Flex DistanceMultiplier
    {
        get => _distanceMultiplier;
        set
        {
                SetProperty(ref _distanceMultiplier, value);
                OnPropertyChanged(nameof(CanCalculate));
                ToggleResultsViewModel.ToggleVisibility();
            }
    }

    private SelectorModels.Flex _frequencyMultiplier;

    public SelectorModels.Flex FrequencyMultiplier
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
        ? ValueTester.NaNTest(new double[] { DistanceValue, FrequencyValue })
        : ValueTester.NaNTest(new double[] { DistanceValue, FrequencyValue })
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