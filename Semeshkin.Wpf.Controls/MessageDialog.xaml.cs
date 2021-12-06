using Semeshkin.WPF.MVVM.Data;
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
    /// Логика взаимодействия для MessageDialog.xaml
    /// </summary>
    public partial class MessageDialog : UserControl
    {
        public MessageDialog()
        {
            InitializeComponent();
        }

        #region Dependency Properties

        public static readonly DependencyProperty ButtonStyleProperty = DependencyProperty.Register(
            nameof(ButtonStyle),
            typeof(TypesOfButtons),
            typeof(MessageDialog),
            new PropertyMetadata(TypesOfButtons.Ok),
            value =>
            {
                return value is TypesOfButtons;
            });

        #endregion


        #region CLR Properties

        public TypesOfButtons ButtonStyle
        {
            get => (TypesOfButtons)GetValue(ButtonStyleProperty);
            set => SetValue(ButtonStyleProperty, value);
        }

        #endregion
    }
}
