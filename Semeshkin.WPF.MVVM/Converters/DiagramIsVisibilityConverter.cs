using Semeshkin.WPF.MVVM.Core.Converter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;

namespace Semeshkin.WPF.MVVM.Converters
{
    public sealed class DiagramIsVisibilityConverter : ConverterBase<DiagramIsVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
