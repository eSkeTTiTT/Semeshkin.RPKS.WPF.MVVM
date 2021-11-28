using Semeshkin.WPF.MVVM.Core.Converter;
using System;
using System.Globalization;
using System.Windows;

namespace Semeshkin.WPF.MVVM.Converters
{
    public sealed class ArithmeticConverter : MultiConverterBase<ArithmeticConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            //check type of parameter - need string
            if (!(parameter is string operation))
            {
                throw new ArgumentException("Type of parametr is not string!", nameof(parameter));
            }

            //also need to check the lenght of values
            if (values.Length != 2)
            {
                throw new ArgumentException("Values lenght is not 2!", nameof(parameter));
            }

            if (values[0] == DependencyProperty.UnsetValue ||
                values[1] == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            var leftOperand = (dynamic)values[0];
            var rightOperand = (dynamic)values[1];

            return operation switch
            {
                "+" => leftOperand + rightOperand,
                "-" => leftOperand - rightOperand,
                "*" => leftOperand * rightOperand,
                "/" => leftOperand / rightOperand,
                "%" => leftOperand % rightOperand,
                _ => throw new ArgumentException($"Invalid operation {operation}", nameof(operation))
            };

        }
    }
}
