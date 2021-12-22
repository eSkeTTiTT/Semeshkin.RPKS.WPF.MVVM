using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Semeshkin.RPKS.WPF.MVVM.Models
{
    public enum SortType
    {
        lox,
        mox
    }

    public class ItemModel
    {
        public int Value { get; set; }
        public Brush ValueColor { get; set; }
    }

    public class PathModel
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public int RgbRed { get; set; }
        public int RgbGreen { get; set; }
        public int RgbBlue { get; set; }
        
        public double AngleDeg { get; set; }

    }



    public sealed class SortModel
    {
        #region Data

        private ObservableCollection<ItemModel> _myValues = new ObservableCollection<ItemModel>();
        private ObservableCollection<ItemModel> _myValuesCopy;
        private ObservableCollection<PathModel> _myValuesPath = new ObservableCollection<PathModel>();
        private StringBuilder _text;
        public readonly ReadOnlyObservableCollection<ItemModel> MyValues;
        public readonly ReadOnlyObservableCollection<PathModel> MyValuesPath;

        #endregion

        public SortModel()
        {
            MyValues = new ReadOnlyObservableCollection<ItemModel>(_myValues);
            MyValuesPath = new ReadOnlyObservableCollection<PathModel>(_myValuesPath);
        }

        #region Methods

        public async Task FillingArrayPathAsync()
        {
            await Task.Run(FillingArrayPath);
        }


        public void FillingArrayPath()
        {
            _myValuesPath.Clear();

            int sum = 0;
            int[] arrayValues = new int[_myValues.Count];
            var radius = 100;
            var startAngle = 4.71;
            var centerPoint = new Point(radius, radius);
            var xyradius = new Size(radius, radius);


            for (int j = 0; j < _myValues.Count; j++)
            {
                arrayValues[j] = _myValues[j].Value;
                sum += _myValues[j].Value;
            }

            var angles = arrayValues.Select(d => d * 2.0 * Math.PI / sum);
            int i = 0;

            foreach (var angle in angles)
            {
                var endAngle = startAngle + angle;

                var startPoint = centerPoint;
                startPoint.Offset(radius * Math.Cos(startAngle), radius * Math.Sin(startAngle));

                var endPoint = centerPoint;
                endPoint.Offset(radius * Math.Cos(endAngle), radius * Math.Sin(endAngle));

                var angleDeg = angle * 180.0 / Math.PI;

                _myValuesPath.Add(new PathModel
                {
                    StartPoint = startPoint,
                    EndPoint = endPoint,
                    AngleDeg = angleDeg,
                    RgbRed = 0,
                    RgbGreen = 255,
                    RgbBlue = 0
                });
               
                startAngle = endAngle;
            }
        }

        public async Task<StringBuilder> RefreshArrayAsync()
        {
            return await Task.Run(RefreshArray);
        }

        public StringBuilder RefreshArray()
        {
            Random rand = new Random();
            int index_1 = 0, index_2 = 0;

            while (index_1 == index_2)
            {
                index_1 = rand.Next(0, _myValuesCopy.Count);
                index_2 = rand.Next(0, _myValuesCopy.Count);
            }

            ItemModel temp = _myValuesCopy[index_1];
            _myValuesCopy[index_1] = _myValuesCopy[index_2];
            _myValuesCopy[index_2] = temp;

            _text = new StringBuilder(_myValuesCopy.Count);

            for (int i = 0; i < _myValuesCopy.Count; i++)
            {
                _text = _text.Append(_myValuesCopy[i].Value).Append(' ');
            }

            return _text;
        }

        public async Task<StringBuilder> CreateArray(int length)
        {
            _myValues.Clear();
            _text = new StringBuilder(length);

            for (int i = 1; i < length + 1; i++)
            {
                _myValues.Add(new ItemModel { Value = i, ValueColor = Brushes.Green });
                _text = _text.Append(i).Append(' ');
            }

            await FillingArrayPathAsync();
            _myValuesCopy = new ObservableCollection<ItemModel>(_myValues);
            return _text;
        }
  
        // myValues = myValuesCopy
        public async void CopyToCurrent()
        {
            _myValues.Clear();
            _myValuesPath.Clear();

            for (int i = 0; i < _myValuesCopy.Count; i++)
            {
                _myValues.Add(_myValuesCopy[i]);
            }
            await FillingArrayPathAsync();
        }

        public void ChangeColor(int i, int j, Brush color)
        {
            _myValues[i].ValueColor = color;
            _myValues[j].ValueColor = color;
        }

        public void ChangeColor(int i, Brush color)
        {
            _myValues[i].ValueColor = color;
        }
        public void ChangeColorPath(int i, int j, bool isRed)
        {

            if (isRed)
            {
                _myValuesPath[i].RgbRed = 255;
                _myValuesPath[i].RgbGreen = 0;
                _myValuesPath[i].RgbBlue = 0;
            }
            else
            {
                
                _myValuesPath[i].RgbRed = 0;
                _myValuesPath[i].RgbGreen = 255;
                _myValuesPath[i].RgbBlue = 0;
            }

            var temp = _myValuesPath[j];
            _myValuesPath[j] = _myValuesPath[i];
            _myValuesPath[i] = temp;
            temp = _myValuesPath[j];
            _myValuesPath[j] = _myValuesPath[i];
            _myValuesPath[i] = temp;
        }

        public void Swap(int i, int j, bool update)
        {
            if (update)
            {
                var temp = _myValues[j];
                _myValues[j] = _myValues[i];
                _myValues[i] = temp;
            }
            else
            {
                var temp = _myValues[j];
                _myValues[j] = _myValues[i];
                _myValues[i] = temp;
                temp = _myValues[j];
                _myValues[j] = _myValues[i];
                _myValues[i] = temp;
            }
        }

        public void Add(int index, int num)
        {
            _myValues[index].Value = num;
        }

        public StringBuilder GetText()
        {
            _text = new StringBuilder(_myValues.Count);

            for (int i = 0; i < _myValues.Count; i++)
            {
                _text = _text.Append(_myValues[i].Value).Append(' ');
            }

            return _text;
        }

        #endregion
    }
}
