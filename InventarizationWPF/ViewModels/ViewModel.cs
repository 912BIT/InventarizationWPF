using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InventarizationWPF.ViewModels
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Уведомляет об изменении свойства property.
        /// </summary>
        /// <param name="property">Имя свойства.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Присваивает полю field значение value без зацикливания.
        /// </summary>
        /// <typeparam name="T">Тип поля field.</typeparam>
        /// <param name="field">Поле.</param>
        /// <param name="value">Значение.</param>
        /// <param name="property">Имя свойства, которое использует поле field.</param>
        /// <returns></returns>
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string property = null)
        {
            if (field.Equals(value)) return false;
            field = value;
            OnPropertyChanged(property);
            return true;
        }

    }
}
