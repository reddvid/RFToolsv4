﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RFTools.Uwp.Helpers;
using RFTools.Uwp.Models;

namespace RFTools.Uwp.ViewModels;

public class SkinDepthViewModel : ObservableObject
{
    public List<SelectorModels.Material> Materials { get; private set; }
    public List<SelectorModels.Flex> LargeFrequency { get; private set; }
    public ToggleResultsViewModel ToggleResultsViewModel { get; } = new ToggleResultsViewModel();

    public SkinDepthViewModel()
    {
            Materials = Selectors.Materials;
            SelectedMaterial = Materials[2];
            LargeFrequency = Selectors.LargeFrequency;
            SelectedFrequency = LargeFrequency[0];

            CalculateCommand = new RelayCommand(Calculate);
        }

    private SelectorModels.Flex _selectedFrequency;
    public SelectorModels.Flex SelectedFrequency
    {
        get => _selectedFrequency;
        set
        {
                SetProperty(ref _selectedFrequency, value);
                OnPropertyChanged(nameof(CanCalculate));
                ToggleResultsViewModel.ToggleVisibility();
            }
    }


    private SelectorModels.Material _selectedMaterial;
    public SelectorModels.Material SelectedMaterial
    {
        get => _selectedMaterial;
        set
        {
                SetProperty(ref _selectedMaterial, value);
                OnPropertyChanged(nameof(IsEnabled));
                ResistivityValue = _selectedMaterial.Resistivity;
                OnPropertyChanged(nameof(ResistivityValue));
                PermeabilityValue = _selectedMaterial.Permeability;
                OnPropertyChanged(nameof(PermeabilityValue));
                ToggleResultsViewModel.ToggleVisibility();
            }
    }

    public bool IsEnabled => SelectedMaterial.Caption.Equals("Custom");

    private double _resistivity;

    public double ResistivityValue
    {
        get => _resistivity;
        set
        {
                SetProperty(ref _resistivity, value);
                if (_resistivity != 0)
                {
                    OnPropertyChanged(nameof(ResistivityValue));
                    OnPropertyChanged(nameof(CanCalculate));
                    ToggleResultsViewModel.ToggleVisibility();
                }
            }
    }

    private double _permeability;

    public double PermeabilityValue
    {
        get => _permeability;
        set
        {
                SetProperty(ref _permeability, value);
                if (_permeability != 0)
                {
                    OnPropertyChanged(nameof(PermeabilityValue));
                    OnPropertyChanged(nameof(CanCalculate));
                    ToggleResultsViewModel.ToggleVisibility();
                }
            }
    }

    private double _frequency;
    public double FrequencyValue
    {
        get => _frequency;
        set
        {
                SetProperty(ref _frequency, value);
                if (_frequency != 0)
                {
                    OnPropertyChanged(nameof(FrequencyValue));
                    OnPropertyChanged(nameof(CanCalculate));
                    ToggleResultsViewModel.ToggleVisibility();
                }
            }
    }

    //public double ResistivityValue => double.Parse(Resistivity);
    //public double PermeabilityValue => double.Parse(Permeability);
    //public double FrequencyValue => double.Parse(Frequency);

    public bool CanCalculate => ValueTester.NaNTest(new double[] { PermeabilityValue, ResistivityValue, FrequencyValue });

    public ICommand CalculateCommand { get; }

    private void Calculate()
    {
            Results = Calculator.SkinDepth(
                resistivity: ResistivityValue * Math.Pow(10, -8),
                permeability: PermeabilityValue,
                frequency: FrequencyValue * SelectedFrequency.Multiplier);

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