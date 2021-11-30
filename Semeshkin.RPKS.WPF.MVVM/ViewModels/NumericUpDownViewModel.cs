using Semeshkin.WPF.MVVM.Core.Command;
using Semeshkin.WPF.MVVM.Core.ViewModel;
using Semeshkin.WPF.MVVM.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Semeshkin.RPKS.WPF.MVVM.ViewModels
{
    internal sealed class NumericUpDownViewModel : ViewModelBase
    {
        #region Fields

        private ICommand _incrementStepCommand;
        private ICommand _decrementStepCommand;

        #endregion

        #region Constructors

        public NumericUpDownViewModel()
        {

        }

        #endregion

        #region Properties

        public ICommand IncrementStepCommand => _incrementStepCommand ??= new RelayCommand(tb => IncrementFunc(tb as TextBox), tb => CheckForExecute(tb as TextBox));
        public ICommand DecrementStepCommand => _decrementStepCommand ??= new RelayCommand(tb => DecrementFunc(tb as TextBox), tb => CheckForExecute(tb as TextBox));

        #endregion

        #region Methods

        private void IncrementFunc(TextBox tb)
        {
            tb.Text = (int.Parse(tb.Text) + 1).ToString();
        }

        private void DecrementFunc(TextBox tb)
        {
            tb.Text = (int.Parse(tb.Text) - 1).ToString();
        }

        private bool CheckForExecute(TextBox tb)
        {
            return int.TryParse(tb.Text, out _);
            //return true;
        }

        #endregion
    }
}
