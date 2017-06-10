using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        private static IDictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();

        public event EventHandler CanExecuteChanged = delegate { };

        public bool CanExecute(object parameter)
        {
            Tuple<string, object> parameters = parameter as Tuple<string, object>;
            string commandName = parameters.Item1;
            object parameterToPass = parameters.Item2;
            ICommand command;
            if (!_commands.TryGetValue(commandName, out command))
            {
                return false;
            }

            return command.CanExecute(parameterToPass);
        }

        public void Execute(object parameter)
        {
            Tuple<string, object> parameters = parameter as Tuple<string, object>;
            string commandName = parameters.Item1;
            object parameterToPass = parameters.Item2;
            ICommand command;
            if (!_commands.TryGetValue(commandName, out command))
            {
                throw new ArgumentException("Invalid Command: " + commandName + " not found");
            }

            command.Execute(parameterToPass);
        }

        public ICommand RegisterCommand(string commandName, ICommand command)
        {
            if (_commands.ContainsKey(commandName))
            {
                throw new NotSupportedException("Action Name: " + commandName + " already exists!");
            }
            
            _commands.Add(commandName, command);

            return command;
        }

        public ICommand RegisterAction<TParam>(string commandName, Action<TParam> action)
        {
            return RegisterCommand(commandName, new RelayCommand<TParam>(action));
        }

        public ICommand RegisterActionAction(string commandName, Action action)
        {
            return RegisterCommand(commandName, new RelayCommand(action));
        }
    }

}
