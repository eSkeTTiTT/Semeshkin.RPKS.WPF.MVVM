using Semeshkin.RPKS.WPF.MVVM.Models;
using Semeshkin.WPF.MVVM.Core.Command;
using Semeshkin.WPF.MVVM.Core.ViewModel;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Semeshkin.WPF.MVVM.Data;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;

namespace Semeshkin.RPKS.WPF.MVVM.ViewModels
{
    internal sealed class SortViewModel : ViewModelBase
    {
        #region Data

        public enum SortType
        {
            Inserts,
            Selects,
            Radix,
            Merge,
            Pyramid
        }

        #endregion

        #region Fields

        private int _numericValue = 1;
        private int _sliderValue = 0;
        private int _selectedIndex;
        private bool _gistIsEnabled = false;
        private bool _circleIsEnabled = true;
        private bool _menuAndSliderIsEnabled = true;
        private bool _numericAndSortsIsEnabled = true;
        private SortType _sortType;
        private PauseOrResume _pauseResumeContent = PauseOrResume.Pause;
        private Dispatcher _dispatcher;
        private StringBuilder _arrayText = new StringBuilder();
        private NumericUpDownViewModel _numericUpDownViewModel;
        private readonly SortModel _model = new SortModel();
        private ICommand _createArrayCommand;
        private ICommand _refreshArrayCommand;
        private ICommand _startSortCommand;
        private ICommand _pauseCommand;
        private ICommand _selectDiagramCommand;

        private bool IsPauseActive = false;

        private readonly object lock_obj = new object();
        private readonly object lock_path = new object();

        #endregion

        #region Constructors

        public SortViewModel()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            BindingOperations.EnableCollectionSynchronization(MyCollectionPath, lock_path);
            BindingOperations.EnableCollectionSynchronization(MyCollection, lock_obj);
        }

        #endregion

        #region Properties

        public ICommand CreateArrayCommand => _createArrayCommand ??= new RelayCommand(_ => CreateArray(), _ => CanCreateArray());
        public ICommand RefreshArrayCommand => _refreshArrayCommand ??= new RelayCommand(_ => RefreshArray(), _ => CanRefreshArray());
        public ICommand StartSortCommand => _startSortCommand ??= new RelayCommand(_ => StartSort());
        public ICommand PauseCommand => _pauseCommand ??= new RelayCommand(_ => Pause());
        public ICommand SelectDiagramCommand => _selectDiagramCommand ??= new RelayCommand(_ => SelectDiagram());


        public NumericUpDownViewModel NumericUpDownViewModel => _numericUpDownViewModel ??= new NumericUpDownViewModel();

        public string ArrayText => _arrayText.ToString();

        public bool CircleIsEnabled
        {
            get => _circleIsEnabled;
            set
            {
                _circleIsEnabled = value;
                OnPropertyChanged(nameof(CircleIsEnabled));
            }

        }

        public bool MenuAndSliderIsEnabled
        {
            get => _menuAndSliderIsEnabled;
            set
            {
                _menuAndSliderIsEnabled = value;
                OnPropertyChanged(nameof(MenuAndSliderIsEnabled));
            }
        }

        public bool NumericAndSortsIsEnabled
        {
            get => _numericAndSortsIsEnabled;
            set
            {
                _numericAndSortsIsEnabled = value;
                OnPropertyChanged(nameof(NumericAndSortsIsEnabled));
            }
        }

        public bool GistIsEnabled
        {
            get => _gistIsEnabled;
            set
            {
                _gistIsEnabled = value;
                OnPropertyChanged(nameof(GistIsEnabled));
            }

        }


        public PauseOrResume PauseResumeContent
        {
            get => _pauseResumeContent;
            set
            {
                _pauseResumeContent = value;
                OnPropertyChanged(nameof(PauseResumeContent));
            }
        }

        public ReadOnlyObservableCollection<ItemModel> MyCollection => _model.MyValues;
        public ReadOnlyObservableCollection<PathModel> MyCollectionPath => _model.MyValuesPath;

        public int NumericValue
        {
            get => _numericValue;
            set
            {
                _numericValue = value;
                OnPropertyChanged(nameof(NumericValue));
            }
        }

        public int SliderValue
        {
            get => _sliderValue;
            set
            {
                _sliderValue = value;
                OnPropertyChanged(nameof(SliderValue));
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;

                _sortType = _selectedIndex switch
                {
                    0 => SortType.Inserts,
                    1 => SortType.Selects,
                    2 => SortType.Radix,
                    3 => SortType.Merge,
                    4 => SortType.Pyramid,
                    _ => throw new ArgumentException()
                };

                OnPropertyChanged(nameof(SelectedIndex));
            }
        }

        #endregion

        #region Methods

        private void WaitIsPauseActive()
        {
            while (IsPauseActive)
            {

            }
        }

        private void SelectDiagram()
        {
            if (_gistIsEnabled)
            {
                GistIsEnabled = false;
                CircleIsEnabled = true;
            }
            else
            {
                GistIsEnabled = true;
                CircleIsEnabled = false;
            }
        }


        private async Task StartSort()
        {
            NumericAndSortsIsEnabled = false;
            MenuAndSliderIsEnabled = false;

            switch (_sortType)
            {
                case SortType.Inserts:
                    await Task.Run(InsertsSort);
                    break;
                case SortType.Selects:
                    await Task.Run(SelectsSort);
                    break;
                case SortType.Radix:
                    await Task.Run(RadixSort);
                    break;
                case SortType.Merge:
                    await Task.Run(MergeSort);
                    break;
                case SortType.Pyramid:
                    await Task.Run(PyramidSort);
                    break;
                default:
                    throw new ArgumentException();
            }

            NumericAndSortsIsEnabled = true;
            MenuAndSliderIsEnabled = true;
        }

        private void Pause()
        {
            PauseResumeContent = _pauseResumeContent == PauseOrResume.Pause
                                ? PauseOrResume.Resume
                                : PauseOrResume.Pause;

            IsPauseActive = IsPauseActive == false;

            MenuAndSliderIsEnabled = _pauseResumeContent == PauseOrResume.Resume;
        }

        private async void RefreshArray()
        {
            for (int i = 0; i < _model.MyValues.Count; i++)
            {
                _arrayText = await _model.RefreshArrayAsync();
                OnPropertyChanged(nameof(ArrayText));
                await Task.Delay(105 - _sliderValue);
            }
            
            _model.CopyToCurrent();
        }

        private bool CanRefreshArray()
        {
            return _model.MyValues.Count > 1;
        }

        private async void CreateArray()
        {
            _arrayText = await _model.CreateArray(_numericValue);
            OnPropertyChanged(nameof(ArrayText));
        }

        private bool CanCreateArray()
        {
            return true;
        }

        public int GetMax()
        {

            int mx = _model.MyValues[0].Value;

            for (int i = 1; i < _model.MyValues.Count; i++)

                if (_model.MyValues[i].Value > mx)

                    mx = _model.MyValues[i].Value;

            return mx;

        }

        #region Sorts

        private async Task<int> Add2Pyramid(int i, int N)
        {
            int imax;
            if ((2 * i + 2) < N)
            {
                imax = _model.MyValues[2 * i + 1].Value < _model.MyValues[2 * i + 2].Value ? (2 * i + 2) : (2 * i + 1);
            }
            else
            {
                imax = 2 * i + 1;
            }

            if (imax >= N)
            {
                return i;
            }

            if (_model.MyValues[i].Value < _model.MyValues[imax].Value)
            {
                _model.ChangeColor(i, Brushes.Red);
                _model.ChangeColor(imax, Brushes.Red);
                _model.Swap(i, imax, false);
                _model.ChangeColorPath(i, imax, true);
                _model.ChangeColorPath(imax, i, true);
                await Task.Delay(105 - _sliderValue);
                WaitIsPauseActive();

                _model.Swap(i, imax, true);
                _model.FillingArrayPath();

                _model.ChangeColor(i, Brushes.Green);
                _model.ChangeColor(imax, Brushes.Green);
                _model.Swap(i, imax, false);
                await Task.Delay(105 - _sliderValue);
                WaitIsPauseActive();

                if (imax < N / 2)
                {
                    i = imax;
                }
            }
            return i;
        }

        private async Task PyramidSort()
        {
            //step 1: building the pyramid
            for (int i = _model.MyValues.Count / 2 - 1; i >= 0; --i)
            {
                long prev_i = i;
                i = await Add2Pyramid(i, _model.MyValues.Count);
                if (prev_i != i) ++i;
            }

            //step 2: sorting
            for (int k = _model.MyValues.Count - 1; k > 0; --k)
            {
                _model.ChangeColor(0, Brushes.Red);
                _model.ChangeColor(k, Brushes.Red);
                _model.Swap(0, k, false);
                _model.ChangeColorPath(0, k, true);
                _model.ChangeColorPath(k, 0, true);
                await Task.Delay(105 - _sliderValue);
                WaitIsPauseActive();

                _model.Swap(0, k, true);
                _model.FillingArrayPath();

                _model.ChangeColor(0, Brushes.Green);
                _model.ChangeColor(k, Brushes.Green);
                _model.Swap(0, k, false);
                await Task.Delay(105 - _sliderValue);
                WaitIsPauseActive();

                int i = 0, prev_i = -1;
                while (i != prev_i)
                {
                    prev_i = i;
                    i = await Add2Pyramid(i, k);
                }
            }

            _arrayText = _model.GetText();
            OnPropertyChanged(nameof(ArrayText));
        }
        static int[] MergeSort(int[] array)
        {
            if (array.Length == 1)
            {
                return array;
            }

            int middle = array.Length / 2;
            return Merge(MergeSort(array.Take(middle).ToArray()), MergeSort(array.Skip(middle).ToArray()));
        }

        static int[] Merge(int[] mass1, int[] mass2)
        {
            int a = 0, b = 0;
            int[] merged = new int[mass1.Length + mass2.Length];
            for (int i = 0; i < mass1.Length + mass2.Length; i++)
            {
                if (b < mass2.Length && a < mass1.Length)
                    if (mass1[a] > mass2[b])
                        merged[i] = mass2[b++];
                    else //if int go for
                        merged[i] = mass1[a++];
                else
                    if (b < mass2.Length)
                    merged[i] = mass2[b++];
                else
                    merged[i] = mass1[a++];
            }
            return merged;
        }

        private async Task MergeSort()
        {
            if (_model.MyValues.Count <= 1) return;
            int[] array = new int[_model.MyValues.Count];
            for (int i = 0; i < _model.MyValues.Count; i++)
            {
                array[i] = _model.MyValues[i].Value;
            }
            array = MergeSort(array);
            for (int i = 0; i < _model.MyValues.Count; i++)
            {
                _model.ChangeColor(i, Brushes.Red);
                if (i == 0)
                {
                    _model.ChangeColorPath(i, i + 1, true);
                    _model.Swap(i, i + 1, false);
                }
                else
                {
                    _model.ChangeColorPath(i, i - 1, true);
                    _model.Swap(i, i - 1, false);
                }
                await Task.Delay(105 - _sliderValue);
                WaitIsPauseActive();

                _model.MyValues[i].Value = array[i];
                _model.FillingArrayPath();

                _model.ChangeColor(i, Brushes.Green);
                if (i == 0)
                {
                    _model.Swap(i, i + 1, false);
                }
                else
                {
                    _model.Swap(i, i - 1, false);
                }
                await Task.Delay(105 - _sliderValue);
                WaitIsPauseActive();
            }

            _arrayText = _model.GetText();
            OnPropertyChanged(nameof(ArrayText));
        }

        private async Task CountSort(int exp)
        {

            int[] output = new int[_model.MyValues.Count];
            int i;
            var copy = new ObservableCollection<ItemModel>(_model.MyValues);
            int[] count = new int[10];
            int index;


            for (i = 0; i < 10; i++)
                count[i] = 0;

            for (i = 0; i < copy.Count; i++)
                count[(copy[i].Value / exp) % 10]++;

            for (i = 1; i < 10; i++)
                count[i] += count[i - 1];


            for (i = copy.Count - 1; i >= 0; i--)
            {

                index = count[(copy[i].Value / exp) % 10] - 1;
                output[index] = copy[i].Value;
                _model.ChangeColor(i, Brushes.Red);
                _model.ChangeColor(index, Brushes.Red);
                if (i == 0)
                {
                    _model.Swap(i, i + 1, false);
                    _model.ChangeColorPath(i, i + 1, true);
                }
                else
                {
                    _model.Swap(i, i - 1, false);
                    _model.ChangeColorPath(i, i - 1, true);
                }

                if (index == 0)
                {
                    _model.Swap(index, index + 1, false);
                    _model.ChangeColorPath(index, index + 1, true);
                }
                else
                {
                    _model.Swap(index, index - 1, false);
                    _model.ChangeColorPath(index, index - 1, true);
                }
                await Task.Delay(105 - _sliderValue);
                WaitIsPauseActive();

                _model.ChangeColor(i, Brushes.Green);
                _model.ChangeColor(index, Brushes.Green);

                if (i == 0)
                {
                    _model.Swap(i, i + 1, false);
                    _model.ChangeColorPath(i, i + 1, false);
                }
                else
                {
                    _model.Swap(i, i - 1, false);
                    _model.ChangeColorPath(i, i - 1, false);
                }

                if (index == 0)
                {
                    _model.Swap(index, index + 1, false);
                    _model.ChangeColorPath(index, index + 1, false);
                }
                else
                {
                    _model.Swap(index, index - 1, false);
                    _model.ChangeColorPath(index, index - 1, false);
                }
                await Task.Delay(105 - _sliderValue);
                WaitIsPauseActive();

                _model.Set(index, copy[i]);
                _model.FillingArrayPath();

                await Task.Delay(105 - _sliderValue);
                WaitIsPauseActive();

                count[(copy[i].Value / exp) % 10]--;

            }


        }
        private async Task RadixSort()
        {
            if (_model.MyValues.Count <= 1) return;
            int m = GetMax();
            

            for (int exp = 1; m / exp > 0; exp *= 10)
                await CountSort(exp);
            

            _arrayText = _model.GetText();
            OnPropertyChanged(nameof(ArrayText));
        }

   
        private async Task SelectsSort()
        {
            for (int i = 0; i < MyCollection.Count - 1; i++)
            {
                int min = i;
                _model.ChangeColor(i, Brushes.Red);
                _model.Swap(i, i + 1, false);
                _model.ChangeColorPath(i, i + 1, true);
                await Task.Delay(105 - _sliderValue);
                WaitIsPauseActive();
                for (int j = i + 1; j < MyCollection.Count; j++)
                {
                    _model.ChangeColor(j, Brushes.Red);
                    _model.Swap(j, j - 1, false);
                    _model.ChangeColorPath(j, j - 1, true);
                    await Task.Delay(105 - _sliderValue);
                    WaitIsPauseActive();
                    if (MyCollection[j].Value < MyCollection[min].Value)
                    {
                        min = j;
                    }
                    _model.ChangeColor(j, Brushes.Green);
                    _model.Swap(j - 1, j, false);
                    _model.ChangeColorPath(j, j - 1, false);
                    await Task.Delay(105 - _sliderValue);
                    WaitIsPauseActive();
                }
                _model.Swap(i, min, true);
                _model.ChangeColor(min, Brushes.Green);
                _model.Swap(min, _model.MyValues.Count - 1, false);
                _model.FillingArrayPath();
                await Task.Delay(105 - _sliderValue);
                WaitIsPauseActive();

                _model.ChangeColor(_model.MyValues.Count - 1, Brushes.Green);
                _model.ChangeColor(i, Brushes.Green);
                _model.Swap(i, _model.MyValues.Count - 1, false);
                await Task.Delay(105 - _sliderValue);
                WaitIsPauseActive();
            }

            _arrayText = _model.GetText();
            OnPropertyChanged(nameof(ArrayText));
        }
        private async Task InsertsSort()
        {
            for (int i = 1; i < MyCollection.Count; i++)
            {
                WaitIsPauseActive();

                int key = MyCollection[i].Value;
                int j = i;
                _model.ChangeColor(j, Brushes.Red);
                _model.Swap(j - 1, j, false);
                _model.ChangeColorPath(j, j - 1, true);
                await Task.Delay(105 - _sliderValue);
                WaitIsPauseActive();
                while ((j >= 1) && (MyCollection[j - 1].Value > key))
                {
                    _model.ChangeColor(j - 1, Brushes.Red);
                    _model.Swap(j - 1, j, false);
                    _model.ChangeColorPath(j - 1, j, true);
                    await Task.Delay(105 - _sliderValue);
                    WaitIsPauseActive();
                    _model.Swap(j - 1, j, true);
                    _model.FillingArrayPath();
                    await Task.Delay(105 - _sliderValue);
                    WaitIsPauseActive();
                    _model.ChangeColor(j, Brushes.Green);
                    _model.Swap(j - 1, j, false);
                    await Task.Delay(105 - _sliderValue);
                    WaitIsPauseActive();
                    j--;
                }

                _model.ChangeColor(j, Brushes.Green);


                if (j == 0)
                {
                    _model.Swap(j, j + 1, false);
                    _model.ChangeColorPath(j, j + 1, false);
                }
                else
                {
                    _model.Swap(j - 1, j, false);
                    _model.ChangeColorPath(j, j - 1, false);
                }
                await Task.Delay(105 - _sliderValue);
                WaitIsPauseActive();
            }

            _arrayText = _model.GetText();
            OnPropertyChanged(nameof(ArrayText));
        }

        #endregion

        #endregion
    }
}
