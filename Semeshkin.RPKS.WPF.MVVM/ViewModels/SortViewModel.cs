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
            Merges,
            Pyramid
        }

        #endregion

        #region Fields

        private int _numericValue = 1;
        private int _sliderValue = 0;
        private bool _pauseEnabled = true;
        private int _selectedIndex;
        private bool _gistIsEnabled = false;
        private bool _circleIsEnabled = true;
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

        public bool PauseEnabled
        {
            get => _pauseEnabled;
            set
            {
                _pauseEnabled = value;
                OnPropertyChanged(nameof(PauseEnabled));
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
                    3 => SortType.Merges,
                    4 => SortType.Pyramid,
                    _ => throw new ArgumentException()
                };

                OnPropertyChanged(nameof(SelectedIndex));
            }
        }

        #endregion

        #region Methods

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


        private async void StartSort()
        {
            switch(_sortType)
            {
                case SortType.Inserts:
                    await InsertsSort();
                    break;
                case SortType.Selects:
                    await SelectsSort();
                    break;
                case SortType.Radix:
                    await RadixSort();
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        private void Pause()
        {
            PauseResumeContent = _pauseResumeContent == PauseOrResume.Pause
                                ? PauseOrResume.Resume
                                : PauseOrResume.Pause;

            PauseEnabled = _pauseResumeContent == PauseOrResume.Pause;
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


        public async Task CountSort(int exp)
        {

            int[] output = new int[_model.MyValues.Count];
            int i;
            var copy = new ObservableCollection<ItemModel>(_model.MyValues);
            int[] count = new int[10];
            int index;

            if (copy.Count <= 1) return;
            for (i = 0; i < 10; i++)
                count[i] = 0;

            for (i = 0; i < copy.Count; i++)
                count[(copy[i].Value / exp) % 10]++;

            for (i = 1; i < 10; i++)
                count[i] += count[i - 1];



            // Создаем выходной массив

            for (i = copy.Count - 1; i >= 0; i--)
            {
                
                index = count[(copy[i].Value / exp) % 10] - 1;
                output[index] = copy[i].Value;
                _model.ChangeColor(i, Brushes.Red);
                _model.ChangeColor(index, Brushes.Red);
                _model.Swap(i, copy.Count - 1, false);
                _model.Swap(index, copy.Count - 1, false);
                await Task.Delay(105 - _sliderValue);

                _model.Set(index, copy[i]);
                _model.ChangeColor(i, Brushes.Green);
                _model.ChangeColor(index, Brushes.Green);
                _model.Swap(i, copy.Count - 1, false);
                _model.Swap(index, copy.Count - 1, false);
                await Task.Delay(105 - _sliderValue);

                count[(copy[i].Value / exp) % 10]--;

            }


        }

        #region Sorts
        public async Task RadixSort()
        {
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
                for (int j = i + 1; j < MyCollection.Count; j++)
                {
                    _model.ChangeColor(j, Brushes.Red);
                    _model.Swap(j, j - 1, false);
                    _model.ChangeColorPath(j, j - 1, true);
                    await Task.Delay(105 - _sliderValue);
                    if (MyCollection[j].Value < MyCollection[min].Value)
                    {
                        min = j;
                    }
                    _model.ChangeColor(j, Brushes.Green);
                    _model.Swap(j - 1, j, false);
                    _model.ChangeColorPath(j, j - 1, false);
                    await Task.Delay(105 - _sliderValue);
                }
                _model.Swap(i, min, true);
                _model.FillingArrayPath();
                await Task.Delay(105 - _sliderValue);

                _model.ChangeColor(i, Brushes.Green);
                _model.Swap(i, _model.MyValues.Count - 1, false);
                await Task.Delay(105 - _sliderValue);
            }

            _arrayText = _model.GetText();
            OnPropertyChanged(nameof(ArrayText));
        }
        private async Task InsertsSort()
        {
            for (int i = 1; i < MyCollection.Count; i++)
            {
                int key = MyCollection[i].Value;
                int j = i;
                _model.ChangeColor(j, Brushes.Red);
                _model.Swap(j - 1, j, false);
                _model.ChangeColorPath(j, j - 1, true);
                await Task.Delay(105 - _sliderValue);
                while ((j >= 1) && (MyCollection[j - 1].Value > key))
                {
                    _model.ChangeColor(j - 1, Brushes.Red);
                    _model.Swap(j - 1, j, false);
                    _model.ChangeColorPath(j - 1, j, true);
                    await Task.Delay(105 - _sliderValue);
                    _model.Swap(j - 1, j, true);
                    _model.FillingArrayPath();
                    await Task.Delay(105 - _sliderValue);
                    _model.ChangeColor(j, Brushes.Green);
                    _model.Swap(j - 1, j, false);
                    await Task.Delay(105 - _sliderValue);
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

            }

            _arrayText = _model.GetText();
            OnPropertyChanged(nameof(ArrayText));
        }

        #endregion

        #endregion
    }
}
