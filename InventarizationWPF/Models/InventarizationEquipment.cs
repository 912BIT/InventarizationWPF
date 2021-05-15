using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InventarizationWPF.Models
{
    internal class InventarizationEquipment : INotifyPropertyChanged
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
            if (field != null && field.Equals(value)) return false;
            field = value;
            OnPropertyChanged(property);
            return true;
        }

        /// <summary>Id оборудования</summary>
        public int Id { get; set; }

        /// <summary>Наименование оборудования</summary>
        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        /// <summary>Количество оборудования фактическое</summary>
        private int _countActual;
        public int CountActual
        {
            get => _countActual;
            set
            { 
                Set(ref _countActual, value);
                Deviation = Count - CountActual;
                SumActual = _countActual * Price;
            }

        }

        /// <summary>Количество оборудования</summary>
        private int _count;
        public int Count
        {
            get => _count;
            set => Set(ref _count, value);
        }

        /// <summary>Отклонение</summary>
        private int _deviation;
        public int Deviation
        {
            get => _deviation;
            set => Set(ref _deviation, value);
        }

        /// <summary>Цена оборудования</summary>
        private int _price;
        public int Price
        {
            get => _price;
            set => Set(ref _price, value);
        }

        /// <summary>Сумма оборудования фактическое</summary>
        private int _sumActual;
        public int SumActual
        {
            get => _sumActual;
            set => Set(ref _sumActual, value);
        }

        /// <summary>Сумма оборудования</summary>
        private int _sum;
        public int Sum
        {
            get => _sum;
            set => Set(ref _sum, value);
        }

        /// <summary>Инициализирует оборудования</summary>
        /// <param name="name">Наименование оборудования</param>
        /// <param name="count">Количество оборудования</param>
        /// <param name="price">Цена оборудования</param>
        /// <param name="sum">Сумма оборудования</param>
        public InventarizationEquipment(string name, int countActual, int count, int deviation,  int price, int sumActual, int sum)
        {
            Name = name;
            CountActual = countActual;
            Count = count;
            Deviation = deviation;
            Price = price;
            SumActual = sumActual;
            Sum = sum;
        }

        public InventarizationEquipment()
        {
        }
    }
}
