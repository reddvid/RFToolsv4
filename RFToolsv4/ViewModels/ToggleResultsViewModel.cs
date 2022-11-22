using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFToolsv4.ViewModels
{
    public class ToggleResultsViewModel : ObservableObject
    {
        public void ToggleVisibility(bool isVisible = false)
        {
            IsResultsVisible = isVisible;
            OnPropertyChanged(nameof(IsResultsVisible));
        }

        private bool _isResultsVisible;

        public bool IsResultsVisible
        {
            get => _isResultsVisible;
            private set
            {
                SetProperty(ref _isResultsVisible, value);
            }
        }
    }
}
