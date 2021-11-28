using Semeshkin.WPF.MVVM.Core.Converter;
using Semeshkin.WPF.MVVM.Data;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media;

namespace Semeshkin.WPF.MVVM.Converters
{
    public sealed class ChangeLanguageAndCapslkConverter : MultiConverterBase<ChangeLanguageAndCapslkConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return !(values[0] is Button capslkButton)
                || !(values[1] is Grid engNoCapslk)
                || !(values[2] is Grid engCapslk)
                || !(values[3] is Grid rusNoCapslk)
                || !(values[4] is Grid rusCapslk)
                ? new ArgumentException("Wrong type of control")
                : (object)new ChangeableButtonAndGrids
                {
                    CapslkButton = capslkButton,
                    EngNoCapslkGrid = engNoCapslk,
                    EngCapslkGrid = engCapslk,
                    RusNoCapslkGrid = rusNoCapslk,
                    RusCapslkGrid = rusCapslk
                };
        }
    }
}
