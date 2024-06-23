using System;
using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RFTools.Uwp.Helpers;
using RFTools.Uwp.Models;

namespace RFTools.Uwp.ViewModels;

public class PathLossViewModel : ObservableObject
{
    public List<SelectorModels.Flex> Distance { get; private set; } = Selectors.Distance;
    public List<SelectorModels.Flex> Frequency { get; private set; } = Selectors.LargeFrequency;
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