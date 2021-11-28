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
    /// Логика взаимодействия для NumericKeyboard.xaml
    /// </summary>
    public partial class NumericKeyboard : UserControl
    {
        public NumericKeyboard()
        {
            InitializeComponent();
        }

        static NumericKeyboard()
        {
            NumeralCommandProperty = DependencyProperty.Register(
                nameof(NumeralCommand),
                typeof(ICommand),
                typeof(NumericKeyboard));

            BackspaceCommandProperty = DependencyProperty.Register(
                nameof(BackspaceCommand),
                typeof(ICommand),
                typeof(NumericKeyboard));
        }

        public static readonly DependencyProperty NumeralCommandProperty;
        public static readonly DependencyProperty BackspaceCommandProperty;

        public ICommand NumeralCommand
        {
            get => (ICommand)GetValue(NumeralCommandProperty);
            set => SetValue(NumeralCommandProperty, value);
        }

        public ICommand BackspaceCommand
        {
            get => (ICommand)GetValue(BackspaceCommandProperty);
            set => SetValue(BackspaceCommandProperty, value);
        }
    }
}
