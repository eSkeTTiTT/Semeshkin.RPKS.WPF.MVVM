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
    internal enum SelectedLanguage
    {
        RUS,
        ENG
    }

    internal sealed class LetterKeyboardViewModel : ViewModelBase
    {
        #region Constructors

        public LetterKeyboardViewModel()
        {
            StrBlock = string.Empty;
            Language = SelectedLanguage.ENG;
        }

        #endregion

        #region Fields

        private string _strBlock;
        private ICommand _letterCommand;
        private ICommand _backspaceCommand;
        private ICommand _capslkCommand;
        private ICommand _changeLanguageCommand;

        #endregion

        #region Properties

        public SelectedLanguage Language { get; private set; }

        public string StrBlock
        {
            get => _strBlock;
            set
            {
                _strBlock = value;
                OnPropertyChanged(nameof(StrBlock));
            }
        }

        public ICommand BackspaceCommand => _backspaceCommand ??= new RelayCommand(_ => RemoveLetter(), _ => IsRemoveNumeral());
        public ICommand LetterCommand => _letterCommand ??= new RelayCommand(letter => LetterCommandAdd((letter as string)[0]));
        public ICommand ChangeLanguageCommand => _changeLanguageCommand ??= new RelayCommand(combo => ChangeLanguage(combo as ChangeableButtonAndGrids));
        public ICommand CapslkCommand => _capslkCommand ??= new RelayCommand(combo => CapslkChange(combo as ChangeableButtonAndGrids));

        #endregion

        #region Methods

        private void LetterCommandAdd(char let)
        {
            StrBlock += let;
        }

        private void RemoveLetter()
        {
            StrBlock = StrBlock.Remove(StrBlock.Length - 1, 1);
        }

        private bool IsRemoveNumeral()
        {
            return StrBlock != string.Empty;
        }

        private void ChangeLanguage(ChangeableButtonAndGrids combo)
        {
            if (Language == SelectedLanguage.ENG)
            {
                if (combo.CapslkButton.Foreground == Brushes.Red)
                {
                    combo.EngCapslkGrid.Visibility = Visibility.Hidden;
                    combo.RusCapslkGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    combo.EngNoCapslkGrid.Visibility = Visibility.Hidden;
                    combo.RusNoCapslkGrid.Visibility = Visibility.Visible;
                }
                Language = SelectedLanguage.RUS;
            }
            else if (Language == SelectedLanguage.RUS)
            {
                if (combo.CapslkButton.Foreground == Brushes.Red)
                {
                    combo.RusCapslkGrid.Visibility = Visibility.Hidden;
                    combo.EngCapslkGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    combo.RusNoCapslkGrid.Visibility = Visibility.Hidden;
                    combo.EngNoCapslkGrid.Visibility = Visibility.Visible;
                }
                Language = SelectedLanguage.ENG;
            }
            else
            {
                throw new ArgumentException("Wrong selected language", nameof(Language));
            }
        }

        private void CapslkChange(ChangeableButtonAndGrids combo)
        {
            if (Language == SelectedLanguage.ENG)
            {
                if (combo.CapslkButton.Foreground == Brushes.Red)
                {
                    combo.CapslkButton.Foreground = Brushes.Black;
                    combo.EngCapslkGrid.Visibility = Visibility.Hidden;
                    combo.EngNoCapslkGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    combo.CapslkButton.Foreground = Brushes.Red;
                    combo.EngNoCapslkGrid.Visibility = Visibility.Hidden;
                    combo.EngCapslkGrid.Visibility = Visibility.Visible;
                }
            }
            else if (Language == SelectedLanguage.RUS)
            {
                if (combo.CapslkButton.Foreground == Brushes.Red)
                {
                    combo.CapslkButton.Foreground = Brushes.Black;
                    combo.RusCapslkGrid.Visibility = Visibility.Hidden;
                    combo.RusNoCapslkGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    combo.CapslkButton.Foreground = Brushes.Red;
                    combo.RusNoCapslkGrid.Visibility = Visibility.Hidden;
                    combo.RusCapslkGrid.Visibility = Visibility.Visible;
                }
            }
            else
            {
                throw new ArgumentException("Wrong selected language", nameof(Language));
            }
        }

        #endregion
    }
}
