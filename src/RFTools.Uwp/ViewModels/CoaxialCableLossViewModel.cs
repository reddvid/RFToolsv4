using System;
using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RFTools.Uwp.Helpers;
using RFTools.Uwp.Models;

namespace RFTools.Uwp.ViewModels;

public class CoaxialCableLossViewModel
{
    public List<string> Manufacturers { get; private set; } = new List<string>
    {
        "Andrew",
        "Belden",
        "David RF",
        "Radio Shack",
        "Times Microwave System",
        "Wireman (Coaxial)",
        "Wireman (Ladder Line)",
        "Others"
    };
}