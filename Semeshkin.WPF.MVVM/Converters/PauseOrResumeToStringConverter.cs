using Semeshkin.WPF.MVVM.Core.Converter;
using Semeshkin.WPF.MVVM.Data;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media;

namespace Semeshkin.WPF.MVVM.Converters
{
    public sealed class PauseOrResumeToStringConverter : ConverterBase<PauseOrResumeToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PauseOrResume pauseOrResume = (PauseOrResume)value;

            return pauseOrResume switch
            {
                PauseOrResume.Pause => "Пауза",
                PauseOrResume.Resume => "Возобновить",
                _ => throw new ArgumentException($"Invalid operation {nameof(pauseOrResume)}")
            };
        }
    }
}
