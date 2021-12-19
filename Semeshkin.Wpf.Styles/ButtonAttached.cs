using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Semeshkin.Wpf.Styles
{
    public class ButtonAttached : Button
    {
        public static DependencyProperty EnabledBackgroundProperty;
        public static DependencyProperty NotEnabledBackgroundProperty;
        public static DependencyProperty EnabledForegroundProperty;
        public static DependencyProperty NotEnabledForegroundProperty;

        static ButtonAttached()
        {
            EnabledBackgroundProperty = DependencyProperty.RegisterAttached(
                "EnabledBackground",
                typeof(Brush),
                typeof(ButtonAttached),
                new PropertyMetadata(Brushes.Black));

            NotEnabledBackgroundProperty = DependencyProperty.RegisterAttached(
                "NotEnabledBackground",
                typeof(Brush),
                typeof(ButtonAttached),
                new PropertyMetadata(Brushes.Gray));

            EnabledForegroundProperty = DependencyProperty.RegisterAttached(
                "EnabledForeground",
                typeof(Brush),
                typeof(ButtonAttached),
                new PropertyMetadata(Brushes.Red));

            NotEnabledForegroundProperty = DependencyProperty.RegisterAttached(
                "NotEnabledForeground",
                typeof(Brush),
                typeof(ButtonAttached),
                new PropertyMetadata(Brushes.Beige));
        }

        public static Brush GetEnabledBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(EnabledBackgroundProperty);
        }

        public static void SetEnabledBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(EnabledBackgroundProperty, value);
        }

        public static Brush GetNotEnabledBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(NotEnabledBackgroundProperty);
        }

        public static void SetNotEnabledBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(NotEnabledBackgroundProperty, value);
        }

        public static Brush GetEnabledForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(EnabledForegroundProperty);
        }

        public static void SetEnabledForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(EnabledForegroundProperty, value);
        }

        public static Brush GetNotEnabledForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(NotEnabledForegroundProperty);
        }

        public static void SetNotEnabledForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(NotEnabledForegroundProperty, value);
        }
    }
}
