using Semeshkin.WPF.MVVM.Core.Converter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Media;

namespace Semeshkin.WPF.MVVM.Converters
{
    public sealed class RgbColorFromArrayConverter : MultiConverterBase<RgbColorFromArrayConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush(Color.FromRgb((byte)(int)values[0], (byte)(int)values[1], (byte)(int)values[2]));
        }
    }
}
