using Semeshkin.WPF.MVVM.Core.Converter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Semeshkin.WPF.MVVM.Converters
{
    public sealed class IsLargeArcConverter : ConverterBase<IsLargeArcConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            return (bool)((double)value > 180);
        }
    }
}
