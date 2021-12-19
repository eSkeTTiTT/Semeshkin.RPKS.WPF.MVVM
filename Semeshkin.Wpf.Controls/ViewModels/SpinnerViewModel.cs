using Semeshkin.Wpf.Controls.Models;
using Semeshkin.WPF.MVVM.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Threading;

namespace Semeshkin.Wpf.Controls.ViewModels
{
    internal sealed class SpinnerViewModel : ViewModelBase
    {
        #region Fields

        private readonly SpinnerModel _model = new SpinnerModel();
        private double _angle = default;
        private readonly DispatcherTimer _timer;

        #endregion

        #region Constructors

        public SpinnerViewModel()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(0.1);
            _timer.Tick += (o, e) =>
            {
                Angle++;
            };
            _timer.Start();
        }

        #endregion

        #region Properties

        public ReadOnlyObservableCollection<Ellipse> MyValues => _model.MyValues;

        public double Angle
        {
            get => _angle;

            private set
            {
                _angle = value;
                OnPropertyChanged(nameof(Angle));
            }
        }

        #endregion

        #region Methods

        public void AddMyValuesCollection(int count) => _model.AddCollection(count);

        public void FillMyValues(double size, Color color) => _model.FillCollection(size, color);

        public void SetActualSize(double height, double width) => _model.SetActualState(height, width);

        #endregion
    }
}
