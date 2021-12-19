using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Semeshkin.Wpf.Styles
{
    public class ScrollViewerAttached : ScrollViewer
    {
        public static readonly DependencyProperty BackgroundColorProperty;

        static ScrollViewerAttached()
        {
            BackgroundColorProperty = DependencyProperty.RegisterAttached(
                "BackgroundColor",
                typeof(Brush),
                typeof(ScrollViewerAttached),
                new PropertyMetadata(Brushes.Black));
        }

        public static Brush GetBackgroundColor(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BackgroundColorProperty);
        }

        public static void SetBackgroundColor(DependencyObject obj, Brush value)
        {
            obj.SetValue(BackgroundColorProperty, value);
        }
    }
}
