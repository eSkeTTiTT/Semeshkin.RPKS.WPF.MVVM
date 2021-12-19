using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Semeshkin.RPKS.WPF.MVVM.Models
{
    public sealed class SortModel
    {
        #region Data

        private ObservableCollection<int> _myValues = new ObservableCollection<int>();
        private ObservableCollection<int> _myValuesCopy;
        private StringBuilder _text;
        public readonly ReadOnlyObservableCollection<int> MyValues;

        #endregion

        public SortModel()
        {
            MyValues = new ReadOnlyObservableCollection<int>(_myValues);
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

            int temp = _myValuesCopy[index_1];
            _myValuesCopy[index_1] = _myValuesCopy[index_2];
            _myValuesCopy[index_2] = temp;

            _text = new StringBuilder(_myValuesCopy.Count);

            for (int i = 0; i < _myValuesCopy.Count; i++)
            {
                _text = _text.Append(_myValuesCopy[i]).Append(' ');
            }

            return _text;
        }

        public StringBuilder CreateArray(int length)
        {
            _myValues.Clear();
            _text = new StringBuilder(length);

            for (int i = 1; i < length + 1; i++)
            {
                _myValues.Add(i);
                _text = _text.Append(i).Append(' ');
            }

            _myValuesCopy = new ObservableCollection<int>(_myValues);
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

        #endregion
    }
}
