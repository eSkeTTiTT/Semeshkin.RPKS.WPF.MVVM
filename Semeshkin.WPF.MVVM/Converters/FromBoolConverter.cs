using Semeshkin.WPF.MVVM.Core.Converter;
using System;
using System.Globalization;
using System.Windows;

namespace Semeshkin.WPF.MVVM.Converters
{
    public sealed class FromBoolConverter : MultiConverterBase<FromBoolConverter>
    {

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(values[0] is bool))
            {
                throw new ArgumentException("Type of values[0] is not bool!", nameof(values));
            }

            if (values.Length != 3)
            {
                return new ArgumentException("Values length is not 3!", nameof(parameter));
            }

            if (values[0] == DependencyProperty.UnsetValue ||
                values[1] == DependencyProperty.UnsetValue ||
                values[2] == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            return (bool)values[0] ? values[1] : values[2];
        }
    }
}
