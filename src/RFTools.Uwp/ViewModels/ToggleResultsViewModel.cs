using CommunityToolkit.Mvvm.ComponentModel;

namespace RFTools.Uwp.ViewModels;

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