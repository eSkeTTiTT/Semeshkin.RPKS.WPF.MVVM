using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace Semeshkin.Wpf.Styles
{
    public class ScrollViewerAttached : ScrollViewer
    {
        public static readonly DependencyProperty BackgroundColorProperty;

        static ScrollViewerAttached()
        {
            BackgroundColorProperty = DependencyProperty.RegisterAttached(
                "BackgroundColor",
                typeof(Color),
                typeof(ScrollViewerAttached),
                new PropertyMetadata(Color.Black));
        }

        public static Color GetBackgroundColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(BackgroundColorProperty);
        }

        public static void SetBackgroundColor(DependencyObject obj, Color value)
        {
            obj.SetValue(BackgroundColorProperty, value);
        }
    }
}
