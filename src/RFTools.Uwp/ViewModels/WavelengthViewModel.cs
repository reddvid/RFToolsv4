using System;
using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RFTools.Uwp.Helpers;
using RFTools.Uwp.Models;

namespace RFTools.Uwp.ViewModels;

public class WavelengthViewModel : ObservableObject
{
    public List<string> ConversionInput { get; private set; }
    public ToggleResultsViewModel ToggleResultsViewModel { get; } = new ToggleResultsViewModel();
    public WavelengthViewModel()
    {
        ConversionInput = new()
        {
            "Wavelength to Frequency",
            "Frequency to Wavelength"
        };
        SelectedInput = "Wavelength to Frequency";

        Presets = Selectors.Media;
        SelectedPreset = Presets[0];

        CalculateCommand = new RelayCommand(Calculate);
    }

    public List<SelectorModels.Preset> Presets { get; private set; }

    public List<SelectorModels.Flex> ComboSelector => HeaderText.Equals("Wavelength:") ? Selectors.Wavelengths : Selectors.Frequencies;

    public string HeaderText => $"{SelectedInput.Split(' ')[0]}:";

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
                    OnPropertyChanged(nameof(ComboSelector));
                    SelectedMultiplier = HeaderText.Equals("Wavelength:") ? ComboSelector[2] : ComboSelector[1];
                    OnPropertyChanged(nameof(SelectedMultiplier));
                    OnPropertyChanged(nameof(CanCalculate));

                    ToggleResultsViewModel.ToggleVisibility();
                }
            }
    }

    private SelectorModels.Preset _selectedPreset;

    public SelectorModels.Preset SelectedPreset
    {
        get => _selectedPreset;
        set
        {
                SetProperty(ref _selectedPreset, value);
                OnPropertyChanged(nameof(PresetValueText));
                OnPropertyChanged(nameof(CanCalculate));

                ToggleResultsViewModel.ToggleVisibility();
            }
    }

    public string PresetValueText => SelectedPreset.Value.ToString("N0");

    private SelectorModels.Flex _selectedMultiplier;

    public SelectorModels.Flex SelectedMultiplier
    {
        get => _selectedMultiplier;
        set
        {
                SetProperty(ref _selectedMultiplier, value);
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


    public bool CanCalculate => ValueTester.NaNTest(InputValue);

    public ICommand CalculateCommand { get; }

    private void Calculate()
    {
            Results = Calculator.WavelengthFrequency(
                input: InputValue * SelectedMultiplier.Multiplier,
                velocity: SelectedPreset.Value,
                unknown: HeaderText.Equals("Wavelength:") ? "Frequency" : "Wavelength");

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