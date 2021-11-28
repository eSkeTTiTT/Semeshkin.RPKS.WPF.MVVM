using Semeshkin.WPF.MVVM.Core.Converter;
using System;
using System.Globalization;
using System.Windows;


namespace Semeshkin.WPF.MVVM.Converters
{
    public sealed class NullCheckConverter : MultiConverterBase<NullCheckConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 3)
            {
                return new ArgumentException("Values length is not 3!", nameof(values));
            }

            if (values[0] == DependencyProperty.UnsetValue ||
                values[1] == DependencyProperty.UnsetValue ||
                values[2] == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            return values[0] == null ? values[1] : values[2];
        }
    }
}
