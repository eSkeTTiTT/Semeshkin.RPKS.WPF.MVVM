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

        #endregion

        #region Properties

        public ICommand CloseDialogCommand => _closeDialogCommand ??= new RelayCommand(border => CloseDialog(border as Border));

        #endregion

        #region Methods

        private void CloseDialog(Border border)
        {
            border.Visibility = border.Visibility == Visibility.Visible
                                ? Visibility.Hidden
                                : Visibility.Visible;
        }

        #endregion

    }
}
