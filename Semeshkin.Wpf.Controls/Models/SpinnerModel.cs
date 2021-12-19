using Semeshkin.WPF.MVVM.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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

        public double ActualH { get; private set; }
        public double ActualW { get; private set; }

        #region Methods

        //the function that is responsible for the arrangement of circles
        private void PlaceOnCanvas()
        {
            double diameter = ActualH / 2.0 ;
            _myValues[0].SetValue(Canvas.LeftProperty, 10.0);
            _myValues[0].SetValue(Canvas.TopProperty, 25.0);
            //_myValues[0].Margin = new Thickness(10);
            _myValues[1].SetValue(Canvas.LeftProperty, 15.0);
            _myValues[1].SetValue(Canvas.TopProperty, 20.0);
            //_myValues[1].Margin = new Thickness(10);
        }

        public void AddCollection(int count)
        {
            int values_count = _myValues.Count;

            if (values_count == 0)
            {
                for (int i = 0; i < count; i++)
                {
                    _myValues.Add(new Ellipse());
                }
            }
            else if (values_count > count)
            {
                int index = values_count - 1;
                int iter = values_count - count;

                while (iter-- != 0)
                {
                    _myValues.RemoveAt(index--);
                }
            }
            else
            {
                double size = _myValues[0].Height;
                Brush color = _myValues[0].Fill;

                for (int i = values_count; i < count; i++)
                {
                    _myValues.Add(new Ellipse()
                    {
                        Height = size,
                        Width = size,
                        Fill = color
                    });
                }
            }

            PlaceOnCanvas();
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

        public void SetActualState(double height, double width)
        {
            ActualH = height;
            ActualW = width;
        }

        public void Refresh()
        {
            
        }

        #endregion
    }
}
