using Semeshkin.Wpf.Controls.ViewModels;
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
    /// <summary>
    /// Логика взаимодействия для Spinner.xaml
    /// </summary>
    public partial class Spinner : UserControl
    {
        private SpinnerViewModel _vm;

        public Spinner()
        {
            InitializeComponent();
            _vm = (SpinnerViewModel)DataContext;
        }


        #region Dependency Properties

        public static readonly DependencyProperty SpeedProperty = DependencyProperty.Register(
            nameof(Speed),
            typeof(double),
            typeof(Spinner));

        public static readonly DependencyProperty CircleSizeProperty = DependencyProperty.Register(
            nameof(CircleSize),
            typeof(double),
            typeof(Spinner),
            new PropertyMetadata(default(double),
                (d, e) =>
                {
                    Spinner userControl = d as Spinner;
                    userControl._vm.FillMyValues((double)e.NewValue, userControl.CircleFill);
                },
                (d, value) =>
                {
                    if (!(d is Spinner userControl))
                    {
                        throw new ArgumentException($"Parameter \"{d}\") should be of type \"{typeof(NumericUpDown).FullName}\"");
                    }

                    if (!(value is double valueDouble))
                    {
                        throw new ArgumentException($"Parameter \"{value}\") should be of type \"{typeof(double).FullName}\"");
                    }

                    if (valueDouble < 0.0)
                    {
                        return 0.0;
                    }
                    else if (valueDouble > 100.0)
                    {
                        return 100.0;
                    }
                    else
                    {
                        return valueDouble;
                    }
                }));

        public static readonly DependencyProperty CircleCountProperty = DependencyProperty.Register(
            nameof(CircleCount),
            typeof(int),
            typeof(Spinner),
            new PropertyMetadata(1,
                (d, e) =>
                {
                    Spinner userControl = d as Spinner;
                    userControl._vm.AddMyValuesCollection((int)e.NewValue);
                },
                (d, value) =>
                {
                    if (!(d is Spinner userControl))
                    {
                        throw new ArgumentException($"Parameter \"{d}\") should be of type \"{typeof(NumericUpDown).FullName}\"");
                    }

                    if (!(value is int valueInt))
                    {
                        throw new ArgumentException($"Parameter \"{value}\") should be of type \"{typeof(int).FullName}\"");
                    }

                    if (valueInt < 1)
                    {
                        return 1;
                    }
                    else if (valueInt > 20)
                    {
                        return 20;
                    }
                    else
                    {
                        return valueInt;
                    }
                }));

        public static readonly DependencyProperty CircleFillProperty = DependencyProperty.Register(
            nameof(CircleFill),
            typeof(Color),
            typeof(Spinner),
            new PropertyMetadata(Colors.Black,
                (d, e) =>
                {
                    Spinner userControl = d as Spinner;
                    userControl._vm.FillMyValues(userControl.CircleSize, (Color)e.NewValue);
                },
                (d, value) =>
                {
                    if (!(d is Spinner userControl))
                    {
                        throw new ArgumentException($"Parameter \"{d}\") should be of type \"{typeof(NumericUpDown).FullName}\"");
                    }

                    if (!(value is Color valueColor))
                    {
                        throw new ArgumentException($"Parameter \"{value}\") should be of type \"{typeof(int).FullName}\"");
                    }

                    return valueColor;
                }));

        #endregion

        #region CLR Properties

        public double Speed
        {
            get => (double)GetValue(SpeedProperty);
            set => SetValue(SpeedProperty, value);
        }

        public int CircleCount
        {
            get => (int)GetValue(CircleCountProperty);
            set => SetValue(CircleCountProperty, value);
        }

        public double CircleSize
        {
            get => (double)GetValue(CircleSizeProperty);
            set => SetValue(CircleSizeProperty, value);
        }

        public Color CircleFill
        {
            get => (Color)GetValue(CircleFillProperty);
            set => SetValue(CircleFillProperty, value);
        }

        #endregion
    }
}
