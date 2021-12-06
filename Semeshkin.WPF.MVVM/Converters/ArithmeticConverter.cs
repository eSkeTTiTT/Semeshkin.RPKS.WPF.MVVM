using Semeshkin.WPF.MVVM.Core.Converter;
using System;
using System.Globalization;
using System.Windows;

namespace Semeshkin.WPF.MVVM.Converters
{
    public sealed class ArithmeticConverter : MultiConverterBase<ArithmeticConverter>
    {
        public enum Operators
        {
            Addition,
            Subtraction,
            Multiplication,
            Division,
            DivisionRemainder
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            //check type of parameter - need string
            if (!(parameter is Operators operation))
            {
                throw new ArgumentException($"Type of parameter is not {typeof(Operators).FullName}", nameof(parameter));
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
                Operators.Addition => leftOperand + rightOperand,
                Operators.Subtraction => leftOperand - rightOperand,
                Operators.Multiplication => leftOperand * rightOperand,
                Operators.Division => leftOperand / rightOperand,
                Operators.DivisionRemainder => leftOperand % rightOperand,
                _ => throw new ArgumentException($"Invalid operation {operation}", nameof(operation))
            };

        }
    }
}
