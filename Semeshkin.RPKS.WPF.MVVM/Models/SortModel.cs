using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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

    public sealed class SortModel
    {
        #region Data

        private ObservableCollection<ItemModel> _myValues = new ObservableCollection<ItemModel>();
        private ObservableCollection<ItemModel> _myValuesCopy;
        private StringBuilder _text;
        public readonly ReadOnlyObservableCollection<ItemModel> MyValues;

        #endregion

        public SortModel()
        {
            MyValues = new ReadOnlyObservableCollection<ItemModel>(_myValues);
        }

        #region Methods

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

        public StringBuilder CreateArray(int length)
        {
            _myValues.Clear();
            _text = new StringBuilder(length);

            for (int i = 1; i < length + 1; i++)
            {
                _myValues.Add(new ItemModel { Value = i, ValueColor = Brushes.Green });
                _text = _text.Append(i).Append(' ');
            }

            _myValuesCopy = new ObservableCollection<ItemModel>(_myValues);
            return _text;
        }

        // myValues = myValuesCopy
        public void CopyToCurrent()
        {
            _myValues.Clear();
            for (int i = 0; i < _myValuesCopy.Count; i++)
            {
                _myValues.Add(_myValuesCopy[i]);
            }
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
