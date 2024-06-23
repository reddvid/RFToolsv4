using System;
using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RFTools.Uwp.Helpers;
using RFTools.Uwp.Models;

namespace RFTools.Uwp.ViewModels;

public class TransmissionLinesViewModel : ObservableObject
{
    public List<SelectorModels.Preset> DielectricConstants { get; private set; }
    public List<SelectorModels.Flex> Diameters { get; private set; }
    public ToggleResultsViewModel ToggleResultsViewModel { get; } = new ToggleResultsViewModel();

    public TransmissionLinesViewModel()
    {
            DielectricConstants = Selectors.DielectricConstants;
            Diameters = Selectors.Diamters;

            SelectedPreset = DielectricConstants[1];
            DiameterMultiplier = Diameters[2];

            CalculateCommand = new RelayCommand(Calculate);
        }

    private SelectorModels.Preset _selectedPreset;

    public SelectorModels.Preset SelectedPreset
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

    private SelectorModels.Flex _diameterMultiplier;

    public SelectorModels.Flex DiameterMultiplier
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
    public bool CanCalculate => ValueTester.NaNTest(new double[] { ImpedanceValue, DiameterValue });

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