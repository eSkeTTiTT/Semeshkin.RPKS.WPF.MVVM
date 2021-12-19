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

namespace Semeshkin.RPKS.WPF.MVVM.ViewModels
{
    internal sealed class SortViewModel : ViewModelBase
    {
        #region Fields

        private int _numericValue = 1;
        private int _sliderValue = 0;
        private Dispatcher _dispatcher;
        private StringBuilder _arrayText = new StringBuilder();
        private NumericUpDownViewModel _numericUpDownViewModel;
        private readonly SortModel _model = new SortModel();
        private ICommand _createArrayCommand;
        private ICommand _refreshArrayCommand;

        #endregion

        #region Constructors

        public SortViewModel()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        #endregion

        #region Properties

        public ICommand CreateArrayCommand => _createArrayCommand ??= new RelayCommand(_ => CreateArray(), _ => CanCreateArray());
        public ICommand RefreshArrayCommand => _refreshArrayCommand ??= new RelayCommand(_ => RefreshArray(), _ => CanRefreshArray());

        public NumericUpDownViewModel NumericUpDownViewModel => _numericUpDownViewModel ??= new NumericUpDownViewModel();

        public string ArrayText => _arrayText.ToString();

        public ReadOnlyObservableCollection<int> MyCollection => _model.MyValues;

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

        #endregion

        #region Methods

        private async void RefreshArray()
        {
            for (int i = 0; i < _model.MyValues.Count; i++)
            {
                await _dispatcher.BeginInvoke(new Action(() =>
                {
                    _arrayText = _model.RefreshArray();
                }));
                OnPropertyChanged(nameof(ArrayText));
                await Task.Delay(105 - _sliderValue);
            }

            _model.CopyToCurrent();
        }

        private bool CanRefreshArray()
        {
            return _model.MyValues.Count > 1;
        }

        private void CreateArray()
        {
            _arrayText = _model.CreateArray(_numericValue);
            OnPropertyChanged(nameof(ArrayText));
        }

        private bool CanCreateArray()
        {
            return true;
        }

        #endregion
    }
}
