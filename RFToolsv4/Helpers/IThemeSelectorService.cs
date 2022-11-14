using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFToolsv4.Helpers
{
    public interface IThemeSelectorService
    {
        ElementTheme GetTheme();
        void SetTheme(ElementTheme theme);
    }
}
