using System;

namespace InventarizationWPF.Infrastructure.Commands
{
    internal class RelayCommand : Command
    {
        /// <summary>
        /// Ссылка на метод, который необходимо выполнить.
        /// </summary>
        private readonly Action<object> _execute;

        /// <summary>
        /// Ссылка на метод, который проверяет возможно ли выполнить команду.
        /// </summary>
        private readonly Func<object, bool> _canExecute;

        /// <summary>
        /// Проверяет возможно ли выполнить команду.
        /// </summary>
        /// <param name="parameter">параметр команды</param>
        /// <returns></returns>
        public override bool CanExecute(object parameter) => _canExecute == null ? true : _canExecute.Invoke(parameter);

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns>True если команду можно выполнить, иначе False.</returns>
        public override void Execute(object parameter) => _execute?.Invoke(parameter);

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
    }
}
