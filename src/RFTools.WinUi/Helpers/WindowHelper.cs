using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using WinRT.Interop;

namespace RFTools.WinUi.Helpers;

public class WindowHelper
{
    public static Window CreateWindow()
    {
        Window newWindow = new Window
        {
            SystemBackdrop = new MicaBackdrop()
        };
        TrackWindow(newWindow);
        return newWindow;
    }

    private static void TrackWindow(Window window)
    {
        window.Closed += (sender, args) => { _activeWindows.Remove(window); };
        _activeWindows.Add(window);
    }

    public static AppWindow GetAppWindow(Window window)
    {
        IntPtr hWnd = WindowNative.GetWindowHandle(window);
        WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
        return AppWindow.GetFromWindowId(wndId);
    }

    public static Window GetWindowForElement(UIElement element)
    {
        if (element.XamlRoot != null)
        {
            foreach (Window window in _activeWindows)
            {
                if (element.XamlRoot == window.Content.XamlRoot)
                {
                    return window;
                }
            }
        }

        return null;
    }

    // get dpi for an element
    public static double GetRasterizationScaleForElement(UIElement element)
    {
        if (element.XamlRoot != null)
        {
            foreach (Window window in _activeWindows)
            {
                if (element.XamlRoot == window.Content.XamlRoot)
                {
                    return element.XamlRoot.RasterizationScale;
                }
            }
        }

        return 0.0;
    }

    public static List<Window> ActiveWindows
    {
        get { return _activeWindows; }
    }

    private static List<Window> _activeWindows = new List<Window>();

    public static StorageFolder GetAppLocalFolder()
    {
        StorageFolder localFolder;
        if (!NativeHelper.IsAppPackaged)
        {
            localFolder = Task.Run(async () =>
                await StorageFolder.GetFolderFromPathAsync(System.AppContext.BaseDirectory)).Result;
        }
        else
        {
            localFolder = ApplicationData.Current.LocalFolder;
        }

        return localFolder;
    }
}