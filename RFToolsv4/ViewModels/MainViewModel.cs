using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using RFToolsv4.Helpers;
using RFToolsv4.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RFToolsv4.Models.SelectorModels;

namespace RFToolsv4.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public List<Material> Materials;

        public MainViewModel()
        {
            Materials = Selectors.Materials;
            SelectedMaterial = Materials[1];
        }

        private Material _selectedMaterial;
        public Material SelectedMaterial
        {
            get => _selectedMaterial;
            set
            {
                SetProperty(ref _selectedMaterial, value);
                OnPropertyChanged(nameof(IsReadOnly));
                Resistivity = _selectedMaterial.Resistivity.ToString();
                OnPropertyChanged(nameof(Resistivity));
                Permeability = _selectedMaterial.Permeability.ToString();
                OnPropertyChanged(nameof(Permeability));
            }
        }

        public bool IsReadOnly => !SelectedMaterial.Caption.Equals("Custom");

        private string _resistivity;

        public string Resistivity
        {
            get => _resistivity;
            set
            {
                SetProperty(ref _resistivity, value);
                if (!string.IsNullOrWhiteSpace(_resistivity))
                {
                    OnPropertyChanged(nameof(ResistivityValue));
                    OnPropertyChanged(nameof(CanCalculate));
                }
            }
        }

        private string _permeability;

        public string Permeability
        {
            get => _permeability;
            set
            {
                SetProperty(ref _permeability, value);
                if (!string.IsNullOrWhiteSpace(_permeability))
                {
                    OnPropertyChanged(nameof(PermeabilityValue));
                    OnPropertyChanged(nameof(CanCalculate));
                }
            }
        }

        public double ResistivityValue => double.Parse(Resistivity);
        public double PermeabilityValue => double.Parse(Permeability);

        public bool CanCalculate => (PermeabilityValue != 0 && ResistivityValue != 0);


    }
}
