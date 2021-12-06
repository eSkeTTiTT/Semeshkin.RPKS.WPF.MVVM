using Semeshkin.WPF.MVVM.Core.Converter;
using System;
using System.Globalization;
using System.Windows;


namespace Semeshkin.WPF.MVVM.Converters
{
    public sealed class FromNullBoolConverter : MultiConverterBase<FromNullBoolConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(values[0] is bool) && values[0] != null)
            {
                throw new ArgumentException($"Type of values[0] is not {typeof(bool).FullName} or null");
            }

            if (values.Length != 4)
            {
                return new ArgumentException("Values length is not 4");
            }

            if (values[0] == DependencyProperty.UnsetValue ||
                values[1] == DependencyProperty.UnsetValue ||
                values[2] == DependencyProperty.UnsetValue ||
                values[3] == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            if (values[0] == null)
            {
                return values[1];
            }
            else return (bool)values[0] ? values[2] : values[3];
        }
    }
}
