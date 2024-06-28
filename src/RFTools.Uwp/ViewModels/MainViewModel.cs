using System;
using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RFTools.Uwp.Helpers;
using RFTools.Uwp.Models;
using RFTools.Uwp.Views;

namespace RFTools.Uwp.ViewModels;

public class MainViewModel : ObservableObject
{
    public List<MenuItem> MenuItems = new()
    {
        new MenuItem("Free-Space Path Loss", typeof(FreeSpacePathLossPage)),
        new MenuItem("Link Budget", typeof(LinkBudgetPage)),
        new MenuItem("Standing Waves", typeof(StandingWavesPage)),
        new MenuItem("Fresnel Zones", typeof(FresnelZonesPage)),
        //new MenuItem("Coaxial Cable Loss", typeof(CoaxialCableLossPage)),
        new MenuItem("Resonance", typeof(ResonancePage)),
        new MenuItem("Two-Wire Transmission Lines", typeof(TransmissionLinesPage)),
        new MenuItem("Skin Depth", typeof(SkinDepthPage)),
        new MenuItem("Wavelength & Frequency Conversion", typeof(WavelengthFrequencyPage)),
        new MenuItem("Delta-Wye Conversion", typeof(DeltaWyePage)),
        new MenuItem("Power, Energy, & Charge Conversion", typeof(PECConversionPage)),
    };


    private bool _isSettingsPage;

    public bool IsSettingsPage
    {
        get => _isSettingsPage;
        set
        {
                SetProperty(ref _isSettingsPage, !value);
            }
    }

}