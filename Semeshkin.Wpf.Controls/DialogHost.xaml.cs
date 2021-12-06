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
    /// Логика взаимодействия для DialogHost.xaml
    /// </summary>
    public partial class DialogHost : UserControl
    {
        public DialogHost()
        {
            InitializeComponent();
        }

        #region Dependency Properties

        public static readonly DependencyProperty WhiteCornerRadiusProperty = DependencyProperty.Register(
            nameof(WhiteCornerRadius),
            typeof(double),
            typeof(DialogHost),
            new PropertyMetadata(default(double)),
            value =>
            {
                if (!(value is double num) || num < 0.0)
                {
                    return false;
                }

                return true;
            });

        public static readonly DependencyProperty BlackCornerRadiusProperty = DependencyProperty.Register(
            nameof(BlackCornerRadius),
            typeof(double),
            typeof(DialogHost),
            new PropertyMetadata(default(double)),
            value =>
            {
                if (!(value is double num) || num < 0.0)
                {
                    return false;
                }

                return true;
            });

        public static readonly DependencyProperty WhiteOpacityProperty = DependencyProperty.Register(
            nameof(WhiteOpacity),
            typeof(double),
            typeof(DialogHost),
            new PropertyMetadata(0.4),
            value =>
            {
                if (!(value is double num) || num < 0.0 || num > 1.0)
                {
                    return false;
                }

                return true;
            });

        public static readonly DependencyProperty BlackOpacityProperty = DependencyProperty.Register(
            nameof(BlackOpacity),
            typeof(double),
            typeof(DialogHost),
            new PropertyMetadata(0.4),
            value =>
            {
                if (!(value is double num) || num < 0.0 || num > 1.0)
                {
                    return false;
                }

                return true;
            });

        #endregion

        #region CLR Properties

        public double WhiteCornerRadius
        {
            get => (double)GetValue(WhiteCornerRadiusProperty);
            set => SetValue(WhiteCornerRadiusProperty, value);
        }

        public double BlackCornerRadius
        {
            get => (double)GetValue(BlackCornerRadiusProperty);
            set => SetValue(BlackCornerRadiusProperty, value);
        }

        public double WhiteOpacity
        {
            get => (double)GetValue(WhiteOpacityProperty);
            set => SetValue(WhiteOpacityProperty, value);
        }

        public double BlackOpacity
        {
            get => (double)GetValue(BlackOpacityProperty);
            set => SetValue(BlackOpacityProperty, value);
        }

        #endregion
    }
}
