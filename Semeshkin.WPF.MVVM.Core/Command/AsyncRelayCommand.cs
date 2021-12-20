using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Semeshkin.WPF.MVVM.Core.Command
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }

    public sealed class AsyncRelayCommand : IAsyncCommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;


        public AsyncRelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }


        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }


        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }


        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public Task ExecuteAsync(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
