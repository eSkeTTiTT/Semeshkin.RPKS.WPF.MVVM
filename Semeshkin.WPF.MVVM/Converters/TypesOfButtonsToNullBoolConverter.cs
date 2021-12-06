using Semeshkin.WPF.MVVM.Core.Converter;
using Semeshkin.WPF.MVVM.Data;
using System;
using System.Globalization;
using System.Windows;


namespace Semeshkin.WPF.MVVM.Converters
{
    public sealed class TypesOfButtonsToNullBoolConverter : ConverterBase<TypesOfButtonsToNullBoolConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TypesOfButtons res))
            {
                throw new ArgumentException($"Type of  is not {typeof(TypesOfButtons).FullName}", nameof(value));
            }

            if (value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            return res switch
            {
                TypesOfButtons.Ok => null,
                TypesOfButtons.OkCancel => true,
                TypesOfButtons.YesNo => false,
                _ => throw new ArgumentException($"Invalid value {res}", nameof(res))
            };
        }
    }
}
