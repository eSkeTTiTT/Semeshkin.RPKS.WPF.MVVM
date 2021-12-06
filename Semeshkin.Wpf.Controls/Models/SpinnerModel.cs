using Semeshkin.WPF.MVVM.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Semeshkin.Wpf.Controls.Models
{
    public sealed class SpinnerModel
    {
        private readonly ObservableCollection<Ellipse> _myValues = new ObservableCollection<Ellipse>();

        public readonly ReadOnlyObservableCollection<Ellipse> MyValues;

        public SpinnerModel()
        {
            MyValues = new ReadOnlyObservableCollection<Ellipse>(_myValues);
        }

        #region Methods

        public void AddCollection(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _myValues.Add(new Ellipse());
            }
        }

        public void FillCollection(double size, Color color)
        {
            foreach (Ellipse item in _myValues)
            {
                item.Width = size;
                item.Height = size;
                item.Fill = new SolidColorBrush(color);
            }
        }

        public void Refresh()
        {
            
        }

        #endregion
    }
}
