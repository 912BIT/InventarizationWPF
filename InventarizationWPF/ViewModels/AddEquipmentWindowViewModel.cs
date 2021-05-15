using InventarizationWPF.Data;
using InventarizationWPF.Infrastructure.Commands;
using InventarizationWPF.Models;
using InventarizationWPF.Services;
using System;
using System.Linq;
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

        #region Текст кнопки

        /// <summary>Заголовок окна</summary>
        private string _buttonText = "Добавить";

        /// <summary>Заголовок окна</summary>
        public string ButtonText
        {
            get => _buttonText;
            set => Set(ref _buttonText, value);
        }

        #endregion

        #region Заголовок окна

        /// <summary>Заголовок окна</summary>
        private string _windowTitle = "Добавление оборудования";

        /// <summary>Заголовок окна</summary>
        public string WindowTitle
        {
            get => _windowTitle;
            set => Set(ref _windowTitle, value);
        }

        #endregion

        #region Id оборудования

        /// <summary>Id оборудования</summary>
        private int _id;

        /// <summary>Id оборудования</summary>
        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }

        #endregion

        #region Наименование оборудования

        /// <summary>Наименование оборудования</summary>
        private string _name = "";

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

        #region Текущая команда

        public ICommand CurrentCommand { get; set; }

        private void OnCurrentCommandExecute(object parameter)
        {
            if (ButtonText == "Добавить")
            {
                AddEquipmentCommand.Execute(null);
            }
            else if (ButtonText == "Изменить")
            {
                EditEquipmentCommand.Execute(null);
            }
        }

        bool CanCurrentCommandExecuted(object parameter) => true;

        #endregion

        #region Добавление оборудования

        /// <summary>Добавляет оборудование в БД</summary>
        public ICommand AddEquipmentCommand { get; set; }

        /// <summary>Добавляет оборудование в БД</summary>
        private void OnAddEquipmentCommandExecute(object parameter)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
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

        #endregion

        #region Изменение оборудования

        /// <summary>Изменяет оборудование в БД</summary>
        public ICommand EditEquipmentCommand { get; set; }

        /// <summary>Изменяет оборудование в БД</summary>
        private void OnEditEquipmentCommandExecute(object parameter)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show("Наименование оборудования не может быть пустым", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (InventarizationContext db = new InventarizationContext())
            {
                Equipment equipment = db.Equipment.Where(eq => eq.Id == Id).Single();
                equipment.Name = Name;
                equipment.Count = Count;
                equipment.Price = Price;
                equipment.Sum = Sum;
                db.Entry(equipment).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }
            CloseRequest(this, EventArgs.Empty);
        }

        /// <summary>Проверяет возможно ли изменить оборудование в БД</summary>
        private bool CanEditEquipmentCommandExecuted(object parameter) => true;

        #endregion

        #region Закрыть окно

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

        #endregion

        private void InitCommands()
        {
            AddEquipmentCommand = new RelayCommand(OnAddEquipmentCommandExecute, CanAddEquipmentCommandExecuted);
            EditEquipmentCommand = new RelayCommand(OnEditEquipmentCommandExecute, CanEditEquipmentCommandExecuted);
            CloseViewModelCommand = new RelayCommand(OnCloseViewModelCommandExecute, CanCloseViewModelCommandExecuted);
            CurrentCommand = new RelayCommand(OnCurrentCommandExecute, CanCurrentCommandExecuted);
        }

        public AddEquipmentWindowViewModel()
        {
            InitCommands();
        }
    }
}
