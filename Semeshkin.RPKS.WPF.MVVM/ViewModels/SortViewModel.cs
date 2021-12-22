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
        #region Fields

        private int _numericValue = 1;
        private int _sliderValue = 0;
        private bool _pauseEnabled = true;
        private int _selectedIndex;
        private bool _diagramIsVisible = false;
        private PauseOrResume _pauseResumeContent = PauseOrResume.Pause;
        private Dispatcher _dispatcher;
        private StringBuilder _arrayText = new StringBuilder();
        private NumericUpDownViewModel _numericUpDownViewModel;
        private readonly SortModel _model = new SortModel();
        private ICommand _createArrayCommand;
        private ICommand _refreshArrayCommand;
        private ICommand _startSortCommand;
        private ICommand _pauseCommand;


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

        public ICommand 


        public NumericUpDownViewModel NumericUpDownViewModel => _numericUpDownViewModel ??= new NumericUpDownViewModel();

        public string ArrayText => _arrayText.ToString();

        public bool DiagramIsVisible
        {
            get => _diagramIsVisible;
            set
            {
                _diagramIsVisible = value;
                OnPropertiesChanged(nameof(DiagramIsVisible));
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
                
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }

        #endregion

        #region Methods

  
        private async void StartSort()
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

        #endregion
    }
}
