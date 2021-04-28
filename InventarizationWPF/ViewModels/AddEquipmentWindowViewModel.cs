using InventarizationWPF.Data;
using InventarizationWPF.Infrastructure.Commands;
using InventarizationWPF.Models;
using InventarizationWPF.Services;
using System;
using System.Windows;
using System.Windows.Input;

namespace InventarizationWPF.ViewModels
{
    internal class AddEquipmentWindowViewModel : ViewModel, ICloseable
    {
        /// <summary>Обработчик события закрытия окна</summary>
        public event EventHandler CloseRequest;

        /// <summary>Закрывает окно</summary>
        protected void RaiseCloseRequest()
        {
            var handler = CloseRequest;
            if (handler != null) handler(this, EventArgs.Empty);
        }


        #region Наименование оборудования
        /// <summary>Наименование оборудования</summary>
        private string _name;

        /// <summary>Наименование оборудования</summary>
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        #endregion

        #region Количество оборудования
        /// <summary>Количество оборудования</summary>
        private int _count;

        /// <summary>Количество оборудования</summary>
        public int Count
        {
            get => _count;
            set
            {
                Set(ref _count, value);
                Sum = Count * Price;
            }

        }
        #endregion

        #region Цена оборудования
        /// <summary>Цена оборудования</summary>
        private int _price;

        /// <summary>Цена оборудования</summary>
        public int Price
        {
            get => _price;
            set
            {
                Set(ref _price, value);
                Sum = Count * Price;
            }
        }
        #endregion

        #region  Сумма оборудования
        /// <summary>Сумма оборудования</summary>
        private int _sum;

        /// <summary>Сумма оборудования</summary>
        public int Sum
        {
            get => _sum;
            set => Set(ref _sum, value);
        }
        #endregion

        #region Команды

        /// <summary>Добавляет оборудование в БД</summary>
        public ICommand AddEquipmentCommand { get; set; }

        /// <summary>Добавляет оборудование в БД</summary>
        private void OnAddEquipmentCommandExecute(object parameter)
        {
            if (string.IsNullOrEmpty(Name))
            {
                MessageBox.Show("Наименование оборудования не может быть пустым", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Equipment equipment = new Equipment(Name, Count, Price, Sum);
            using (InventarizationContext db = new InventarizationContext())
            {
                db.Equipment.Add(equipment);
                db.SaveChanges();
            }
            CloseRequest(this, EventArgs.Empty);
        }

        /// <summary>Проверяет возможно ли добавить оборудование в БД</summary>
        private bool CanAddEquipmentCommandExecuted(object parameter) => true;

        /// <summary>Закрывает окно</summary>
        public ICommand CloseViewModelCommand { get; set; }

        /// <summary>Закрывает окно</summary>
        private void OnCloseViewModelCommandExecute(object parameter)
        {
            CloseRequest(this, EventArgs.Empty);
        }

        /// <summary>Проверяет возможно ли закрыть окон</summary>
        private bool CanCloseViewModelCommandExecuted(object parameter) => true;

        #endregion

        public AddEquipmentWindowViewModel()
        {
            AddEquipmentCommand = new RelayCommand(OnAddEquipmentCommandExecute, CanAddEquipmentCommandExecuted);
            CloseViewModelCommand = new RelayCommand(OnCloseViewModelCommandExecute, CanCloseViewModelCommandExecuted);
        }
    }
}
