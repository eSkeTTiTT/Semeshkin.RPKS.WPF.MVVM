using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Semeshkin.Wpf.Controls
{
    /// <summary>
    /// Логика взаимодействия для LetterKeyboard.xaml
    /// </summary>
    public partial class LetterKeyboard : UserControl
    {
        public LetterKeyboard()
        {
            InitializeComponent();
        }

        static LetterKeyboard()
        {
            LetterCommandProperty = DependencyProperty.Register(
                nameof(LetterCommand),
                typeof(ICommand),
                typeof(LetterKeyboard));

            BackspaceCommandProperty = DependencyProperty.Register(
                nameof(BackspaceCommand),
                typeof(ICommand),
                typeof(LetterKeyboard));

            CapslkCommandProperty = DependencyProperty.Register(
                nameof(CapslkCommand),
                typeof(ICommand),
                typeof(LetterKeyboard));

            ChangeLanguageCommandProperty = DependencyProperty.Register(
                nameof(ChangeLanguageCommand),
                typeof(ICommand),
                typeof(LetterKeyboard));
        }

        public static readonly DependencyProperty LetterCommandProperty;
        public static readonly DependencyProperty BackspaceCommandProperty;
        public static readonly DependencyProperty CapslkCommandProperty;
        public static readonly DependencyProperty ChangeLanguageCommandProperty;


        public ICommand LetterCommand
        {
            get => (ICommand)GetValue(LetterCommandProperty);
            set => SetValue(LetterCommandProperty, value);
        }

        public ICommand BackspaceCommand
        {
            get => (ICommand)GetValue(BackspaceCommandProperty);
            set => SetValue(BackspaceCommandProperty, value);
        }

        public ICommand CapslkCommand
        {
            get => (ICommand)GetValue(CapslkCommandProperty);
            set => SetValue(CapslkCommandProperty, value);
        }

        public ICommand ChangeLanguageCommand
        {
            get => (ICommand)GetValue(ChangeLanguageCommandProperty);
            set => SetValue(ChangeLanguageCommandProperty, value);
        }
    }
}
