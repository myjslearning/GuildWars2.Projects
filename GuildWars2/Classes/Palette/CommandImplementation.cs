﻿using System;
using System.Windows.Input;

namespace GuildWars2.Classes.Palette
{
    /// <summary>
    /// No WPF project is complete without it's own version of this.
    /// </summary>
    public class CommandImplementation : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public CommandImplementation(Action<object> execute) : this(execute, null) {
        }

        public CommandImplementation(Action<object> execute, Func<object, bool> canExecute) {
            if(execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute ?? (x => true);
        }

        public bool CanExecute(object parameter) => _canExecute(parameter);

        public void Execute(object parameter) {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Refresh() {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
