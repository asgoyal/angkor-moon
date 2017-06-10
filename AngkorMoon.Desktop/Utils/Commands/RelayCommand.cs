using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AngkorMoon.Desktop.Services;

namespace AngkorMoon.Desktop.Utils.Commands
{
    public class GeneralizedRelayCommand<TAction, T> : ICommand
    {
        protected TAction TargetExecuteMethod;
        protected Func<bool> TargetCanExecuteMethod;

        public GeneralizedRelayCommand(TAction executeMethod, Func<bool> canExecuteMethod = null)
        {
            TargetExecuteMethod = executeMethod;
            TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public bool CanExecute(object parameter)
        {
            if (TargetCanExecuteMethod != null)
            {
                return TargetCanExecuteMethod();
            }

            if (TargetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }

        public virtual void Execute(object parameter)
        {
            if (TargetExecuteMethod == null)
            {
                return;
            }

            if (typeof(TAction) == typeof(Action))
            {
                var action = TargetExecuteMethod as Action;
                action();
            }
            else
            {
                var action = TargetExecuteMethod as Action<T>;
                action((T)parameter);
            }
        }
    }

    public class RelayCommand : GeneralizedRelayCommand<Action, object>
    {
        public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod = null) 
            : base(executeMethod, canExecuteMethod)
        {
        }
    }

    public class RelayCommand<T> : GeneralizedRelayCommand<Action<T>, T>
    {
        public RelayCommand(Action<T> executeMethod, Func<bool> canExecuteMethod = null) 
            : base(executeMethod, canExecuteMethod)
        {
        }
    }

    public class InterpretingCommand : ICommand
    {
        private ICommandHandler _commandHandler;

        public event EventHandler CanExecuteChanged = delegate { };

        public InterpretingCommand(ICommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        public bool CanExecute(object parameter)
        {
            return _commandHandler.CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _commandHandler.Execute(parameter);
        }
    }

}
