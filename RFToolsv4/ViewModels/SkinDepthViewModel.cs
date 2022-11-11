﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RFToolsv4.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RFToolsv4.Models.SelectorModels;
using System.Windows.Input;

namespace RFToolsv4.ViewModels
{
    public class SkinDepthViewModel : ObservableObject
    {
        public List<Material> Materials;
        public List<Flex> LargeFrequency;

        public SkinDepthViewModel()
        {
            Materials = Selectors.Materials;
            SelectedMaterial = Materials[2];
            LargeFrequency = Selectors.LargeFrequency;
            SelectedFrequency = LargeFrequency[0];

            CalculateCommand = new RelayCommand(Calculate);
        }

        private Flex _selectedFrequency;
        public Flex SelectedFrequency
        {
            get => _selectedFrequency;
            set
            {
                SetProperty(ref _selectedFrequency, value);
                OnPropertyChanged(nameof(CanCalculate));
            }
        }


        private Material _selectedMaterial;
        public Material SelectedMaterial
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
                }
            }
        }

        //public double ResistivityValue => double.Parse(Resistivity);
        //public double PermeabilityValue => double.Parse(Permeability);
        //public double FrequencyValue => double.Parse(Frequency);

        public bool CanCalculate => (PermeabilityValue != 0 && ResistivityValue != 0 && FrequencyValue != 0);

        public ICommand CalculateCommand { get; }

        private void Calculate()
        {
            Results = Calculator.SkinDepth(
                resistivity: ResistivityValue * Math.Pow(10, -8),
                permeability: PermeabilityValue,
                frequency: FrequencyValue * SelectedFrequency.Multiplier);
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
