using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Semeshkin.Wpf.Controls
{

    public partial class NumericUpDown : UserControl
    {
        public NumericUpDown()
        {
            InitializeComponent();
        }

        static NumericUpDown()
        {
            ValueProperty = DependencyProperty.Register(
                nameof(Value),
                typeof(int),
                typeof(NumericUpDown),
                new PropertyMetadata(default(int), (d, e) => { },
                (d, value) =>
                {
                    if (!(d is NumericUpDown userControl))
                    {
                        throw new ArgumentException($"Parameter \"{d}\") should be of type \"{typeof(NumericUpDown).FullName}\"");
                    }

                    if (!(value is int valueInt))
                    {
                        throw new ArgumentException($"Parameter \"{value}\") should be of type \"{typeof(int).FullName}\"");
                    }

                    return valueInt < userControl.MinValue
                        ? userControl.MinValue
                        : valueInt > userControl.MaxValue ? userControl.MaxValue : (object)valueInt;
                }), value =>
                {
                    return !(value is int valueInt)
                        ? throw new ArgumentException($"Parameter \"{value}\") should be of type \"{typeof(int).FullName}\"")
                        : true;
                });

            MinValueProperty = DependencyProperty.Register(
                nameof(MinValue),
                typeof(int),
                typeof(NumericUpDown),
                new PropertyMetadata(int.MinValue, (d, e) => { },
                (d, value) =>
                {
                    if (!(d is NumericUpDown userControl))
                    {
                        throw new ArgumentException($"Parameter \"{d}\") should be of type \"{typeof(NumericUpDown).FullName}\"");
                    }

                    if (!(value is int valueInt))
                    {
                        throw new ArgumentException($"Parameter \"{value}\") should be of type \"{typeof(int).FullName}\"");
                    }

                    return valueInt > userControl.MaxValue
                        ? int.MinValue
                        : valueInt;
                }));

            MaxValueProperty = DependencyProperty.Register(
                nameof(MaxValue),
                typeof(int),
                typeof(NumericUpDown),
                new PropertyMetadata(int.MaxValue, (d, e) => { },
                (d, value) =>
                {
                    if (!(d is NumericUpDown userControl))
                    {
                        throw new ArgumentException($"Parameter \"{d}\") should be of type \"{typeof(NumericUpDown).FullName}\"");
                    }

                    if (!(value is int valueInt))
                    {
                        throw new ArgumentException($"Parameter \"{value}\") should be of type \"{typeof(int).FullName}\"");
                    }

                    return valueInt < userControl.MinValue
                        ? int.MaxValue
                        : valueInt;
                }));

            IncrementStepCommandProperty = DependencyProperty.Register(
                nameof(IncrementStepCommand),
                typeof(ICommand),
                typeof(NumericUpDown));

            DecrementStepCommandProperty = DependencyProperty.Register(
                nameof(DecrementStepCommand),
                typeof(ICommand),
                typeof(NumericUpDown));
        }

        public static readonly DependencyProperty ValueProperty;
        public static readonly DependencyProperty MinValueProperty;
        public static readonly DependencyProperty MaxValueProperty;
        public static readonly DependencyProperty IncrementStepCommandProperty;
        public static readonly DependencyProperty DecrementStepCommandProperty;


        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public int MinValue
        {
            get => (int)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        public int MaxValue
        {
            get => (int)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        public ICommand IncrementStepCommand
        {
            get => (ICommand)GetValue(IncrementStepCommandProperty);
            set => SetValue(IncrementStepCommandProperty, value);
        }

        public ICommand DecrementStepCommand
        {
            get => (ICommand)GetValue(DecrementStepCommandProperty);
            set => SetValue(DecrementStepCommandProperty, value);
        }
    }
}
