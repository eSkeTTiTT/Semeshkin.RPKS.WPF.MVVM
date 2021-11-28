using Semeshkin.WPF.MVVM.Core.Command;
using Semeshkin.WPF.MVVM.Core.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Semeshkin.RPKS.WPF.MVVM.ViewModels
{
    internal sealed class NumericKeyboardViewModel : ViewModelBase
    {
        #region Constructors

        public NumericKeyboardViewModel()
        {
            StrBlock = string.Empty;
        }

        #endregion

        #region Fields

        private string _strBlock;
        private ICommand _numeralCommand;
        private ICommand _backspaceCommand;

        private ICommand example;

        #endregion

        #region Properties

        public string StrBlock
        {
            get => _strBlock;

            private set
            {
                _strBlock = value;
                OnPropertyChanged(nameof(StrBlock));
            }
        }

        public ICommand NumeralCommand => _numeralCommand ??= new RelayCommand(numeral => NumeralCommandAdd((numeral as string)[0]));
        public ICommand BackspaceCommand => _backspaceCommand ??= new RelayCommand(_ => RemoveNumeral(), _ => IsRemoveNumeral());

        public ICommand Example => example ??= new RelayCommand(a => ExampleButton((Button)a));

        #endregion

        #region Methods

        private void NumeralCommandAdd(char num)
        {
            StrBlock += num;
        }

        private void RemoveNumeral()
        {
            StrBlock = StrBlock.Remove(StrBlock.Length - 1, 1);
        }

        private bool IsRemoveNumeral()
        {
            return StrBlock != string.Empty;
        }

        private void ExampleButton(Button e)
        {
            MessageBox.Show(e.Name);
        }

        #endregion

    }
}
