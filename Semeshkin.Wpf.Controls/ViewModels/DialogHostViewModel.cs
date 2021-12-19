using Semeshkin.WPF.MVVM.Core.Command;
using Semeshkin.WPF.MVVM.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Semeshkin.Wpf.Controls.ViewModels
{
    internal sealed class DialogHostViewModel : ViewModelBase
    {

        #region Fields

        private ICommand _closeDialogCommand;
        private ICommand _buttonCommand;

        #endregion

        #region Properties

        public ICommand CloseDialogCommand => _closeDialogCommand ??= new RelayCommand(border => CloseDialog(border as Border));
        public ICommand ButtonCommand => _buttonCommand ??= new RelayCommand(grid => CloseControl(grid as Grid));

        #endregion

        #region Methods

        private void CloseDialog(Border border)
        {
            border.Visibility = border.Visibility == Visibility.Visible
                                ? Visibility.Hidden
                                : Visibility.Visible;
        }

        private void CloseControl(Grid grid)
        {
            grid.Visibility = Visibility.Collapsed;
        }

        #endregion

    }
}
