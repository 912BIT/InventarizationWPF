using InventarizationWPF.Data;
using InventarizationWPF.Infrastructure.Commands;
using InventarizationWPF.Models;
using InventarizationWPF.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace InventarizationWPF.ViewModels
{
    internal class OfficeViewModel : ViewModel
    {
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

        #region Команды

        #region Добавление оборудования

        /// <summary>Вызывает окно для добавления оборудования</summary>
        public ICommand AddEquipmentCommand { get; set; }

        /// <summary>Вызывает окно для добавления оборудования</summary>
        private void OnAddEquipmentCommandExecute(object parameter)
        {            
            AddEquipmentWindowView addEquipmentWindow = new AddEquipmentWindowView();
            addEquipmentWindow.ShowDialog();
            LoadEquipment();
        }

        /// <summary>Проверяет можно ли вызвать окно для добавления оборудования</summary>
        private bool CanAddEquipmentCommandExecuted(object parameter) => true;

        #endregion

        #region Изменение оборудования

        /// <summary>Вызывает окно для изменения оборудования</summary>
        public ICommand EditEquipmentCommand { get; set; }

        /// <summary>Вызывает окно для изменения оборудования</summary>
        private void OnEditEquipmentCommandExecute(object parameter)
        {
            AddEquipmentWindowView addEquipmentWindow = new AddEquipmentWindowView();
            AddEquipmentWindowViewModel equipmentVM = (AddEquipmentWindowViewModel)addEquipmentWindow.DataContext;
            equipmentVM.CurrentCommand = equipmentVM.EditEquipmentCommand;
            equipmentVM.ButtonText = "Изменить";
            equipmentVM.WindowTitle = "Изменение оборудования";
            equipmentVM.Id = SelectedEquipment.Id;
            equipmentVM.Name = SelectedEquipment.Name;
            equipmentVM.Count = SelectedEquipment.Count;
            equipmentVM.Price = SelectedEquipment.Price;
            equipmentVM.Sum = SelectedEquipment.Sum;
            addEquipmentWindow.ShowDialog();
            LoadEquipment();
        }

        /// <summary>Проверяет можно ли вызывать окно для изменения оборудования</summary>
        private bool CanEditEquipmentCommandExecuted(object parameter)
        {
            if (SelectedEquipment == null)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Удаление оборудования

        /// <summary>Вызывает окно для удаления оборудования</summary>
        public ICommand RemoveEquipmentCommand { get; set; }

        /// <summary>Вызывает окно для удаления оборудования</summary>
        private void OnRemoveEquipmentCommandExecute(object parameter)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Вы действительно хотите удалить оборудование?", "Удаление оборудования", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.OK)
            {
                using (InventarizationContext db = new InventarizationContext())
                {
                    if (SelectedEquipment != null)
                    {
                        db.Entry(SelectedEquipment).State = EntityState.Deleted;
                        db.Equipment.Remove(SelectedEquipment);
                        db.SaveChanges();
                    }
                }
                LoadEquipment();
            }
        }

        /// <summary>Проверяет можно ли вызывать окно для изменения оборудования</summary>
        private bool CanRemoveEquipmentCommandExecuted(object parameter)
        {
            if (SelectedEquipment == null)
            {
                return false;
            }

            return true;
        }
        #endregion

        #endregion

        private void InitCommands()
        {
            AddEquipmentCommand = new RelayCommand(OnAddEquipmentCommandExecute, CanAddEquipmentCommandExecuted);
            EditEquipmentCommand = new RelayCommand(OnEditEquipmentCommandExecute, CanEditEquipmentCommandExecuted);
            RemoveEquipmentCommand = new RelayCommand(OnRemoveEquipmentCommandExecute, CanRemoveEquipmentCommandExecuted);
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
        public OfficeViewModel()
        {
            InitCommands();
            LoadEquipment();
        }
    }
}
