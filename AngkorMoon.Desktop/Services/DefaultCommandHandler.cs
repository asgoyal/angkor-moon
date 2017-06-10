using System;
using System.Collections.Generic;
using System.Windows.Input;
using AngkorMoon.Desktop.Utils.Commands;

namespace AngkorMoon.Desktop.Services
{
    public class DefaultCommandHandler : ICommandHandler
    {
        private IDictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();

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

        public ICommand RegisterAction(string commandName, Action action)
        {
            return RegisterCommand(commandName, new RelayCommand(action));
        }
    }
}
