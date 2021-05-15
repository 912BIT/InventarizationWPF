using InventarizationWPF.Data;
using InventarizationWPF.Infrastructure.Commands;
using InventarizationWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InventarizationWPF.ViewModels
{
    internal class InventoryViewAddEquipmentWindowViewModel : ViewModel
    {
        /// <summary>Обработчик события закрытия окна</summary>
        public event EventHandler CloseRequest;

        /// <summary>Закрывает окно</summary>
        protected void RaiseCloseRequest()
        {
            var handler = CloseRequest;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        bool IsChoosed { get; set; } = false;

        #region Список оборудования

        /// <summary>Список оборудования</summary>
        private ObservableCollection<Equipment> _equipments = new ObservableCollection<Equipment>();

        /// <summary>Список оборудования</summary>
        public ObservableCollection<Equipment> Equipments
        {
            get => _equipments;
            set => Set(ref _equipments, value);
        }

        #endregion

        #region Выбранное оборудование

        /// <summary>Выбранное оборудования</summary>
        private Equipment _selectedEquipment;

        /// <summary>Выбранное оборудования</summary>
        public Equipment SelectedEquipment
        {
            get => _selectedEquipment;
            set => Set(ref _selectedEquipment, value);
        }

        #endregion

        #region Закрыть окно

        /// <summary>Закрывает окно</summary>
        public ICommand CloseViewModelCommand { get; set; }

        /// <summary>Закрывает окно</summary>
        private void OnCloseViewModelCommandExecute(object parameter)
        {
            if (SelectedEquipment != null)
            {
                IsChoosed = true;
                CloseRequest(this, EventArgs.Empty);
            }
        }

        /// <summary>Проверяет возможно ли закрыть окон</summary>
        private bool CanCloseViewModelCommandExecuted(object parameter) => true;

        #endregion

        private void InitCommands()
        {
            CloseViewModelCommand = new RelayCommand(OnCloseViewModelCommandExecute, CanCloseViewModelCommandExecuted);
        }

        private void LoadEquipment()
        {
            Equipments.Clear();
            List<Equipment> equipments;

            using (InventarizationContext db = new InventarizationContext())
            {
                equipments = db.Equipment.ToList();
            }

            foreach (var equipment in equipments)
            {
                Equipments.Add(equipment);
            }

            if (Equipments.Count > 0)
            {
                SelectedEquipment = Equipments[0];
            }
        }

        /// <summary>Инициализирует вью-модель офиса</summary>
        public InventoryViewAddEquipmentWindowViewModel()
        {
            InitCommands();
            LoadEquipment();
        }
    }
}
