﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AngkorMoon.Desktop.Services
{
    public interface ICommandHandler : ICommand
    {
        ICommand RegisterCommand(string commandName, ICommand command);
        ICommand RegisterAction<TParam>(string commandName, Action<TParam> action);
        ICommand RegisterAction(string commandName, Action action);
    }
}