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
    public class DeltaWyeViewModel : ObservableObject
    {
        public List<string> ConversionInput { get; private set; }

        public DeltaWyeViewModel()
        {
            ConversionInput = new()
            {
                "Delta to Wye",
                "Wye to Delta"
            };

            SelectedInput = ConversionInput[0];

            CalculateCommand = new RelayCommand(Calculate);

            SetSelectedMultipliers();
        }

        private string _selectedInput;
        public string SelectedInput
        {
            get => _selectedInput;
            set
            {
                SetProperty(ref _selectedInput, value);
                if (_selectedInput != null)
                {
                    SetInputBoxHeaders();
                    OnPropertyChanged(nameof(ComboSelector));
                    OnPropertyChanged(nameof(CanCalculate));
                }
            }
        }

        public List<Flex> ComboSelector => Selectors.Resistance;

        private void SetSelectedMultipliers()
        {
            FirstSelectedMultiplier = ComboSelector[1];
            SecondSelectedMultiplier = ComboSelector[1];
            ThirdSelectedMultiplier = ComboSelector[1];

            OnPropertyChanged(nameof(FirstSelectedMultiplier));
            OnPropertyChanged(nameof(SecondSelectedMultiplier));
            OnPropertyChanged(nameof(ThirdSelectedMultiplier));
        }

        private Flex _firstSelectedMultiplier;
        public Flex FirstSelectedMultiplier
        {
            get => _firstSelectedMultiplier;
            set
            {
                SetProperty(ref _firstSelectedMultiplier, value);
                OnPropertyChanged(nameof(CanCalculate));
            }
        }

        private Flex _secondSelectedMultiplier;
        public Flex SecondSelectedMultiplier
        {
            get => _secondSelectedMultiplier;
            set
            {
                SetProperty(ref _secondSelectedMultiplier, value);
                OnPropertyChanged(nameof(CanCalculate));
            }
        }

        private Flex _thirdSelectedMultiplier;
        public Flex ThirdSelectedMultiplier
        {
            get => _thirdSelectedMultiplier;
            set
            {
                SetProperty(ref _thirdSelectedMultiplier, value);
                OnPropertyChanged(nameof(CanCalculate));
            }
        }

        private void SetInputBoxHeaders()
        {
            OnPropertyChanged(nameof(FirstHeader));
            OnPropertyChanged(nameof(SecondHeader));
            OnPropertyChanged(nameof(ThirdHeader));
        }

        public string FirstHeader => SelectedInput.Equals("Delta to Wye") ? "Rᴀ" : "R₁";
        public string SecondHeader => SelectedInput.Equals("Delta to Wye") ? "Rʙ" : "R₂";
        public string ThirdHeader => SelectedInput.Equals("Delta to Wye") ? "Rᴄ" : "R₃";

        private double _firstInputValue;

        public double FirstInputValue
        {
            get => _firstInputValue;
            set
            {
                SetProperty(ref _firstInputValue, value);
                OnPropertyChanged(nameof(CanCalculate));
            }
        }

        private double _secondInputValue;

        public double SecondInputValue
        {
            get => _secondInputValue;
            set
            {
                SetProperty(ref _secondInputValue, value);
                OnPropertyChanged(nameof(CanCalculate));
            }
        }

        private double _thirdInputValue;

        public double ThirdInputValue
        {
            get => _thirdInputValue;
            set
            {
                SetProperty(ref _thirdInputValue, value);
                OnPropertyChanged(nameof(CanCalculate));
            }
        }

        public bool CanCalculate => (FirstInputValue != 0 && SecondInputValue != 0 && ThirdInputValue != 0);

        public ICommand CalculateCommand { get; }

        private void Calculate()
        {
            Results = Calculator.DeltaWye(
                first: FirstInputValue * FirstSelectedMultiplier.Multiplier,
                second: SecondInputValue * SecondSelectedMultiplier.Multiplier,
                third: ThirdInputValue * ThirdSelectedMultiplier.Multiplier,
                setting: SelectedInput);
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
