using InventarizationWPF.Data;
using InventarizationWPF.Infrastructure;
using InventarizationWPF.Infrastructure.Commands;
using InventarizationWPF.Models;
using InventarizationWPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InventarizationWPF.ViewModels
{
    internal class InventoryViewModel : ViewModel
    {
        /// <summary>Обработчик события закрытия окна</summary>
        public event EventHandler CloseRequest;

        /// <summary>Закрывает окно</summary>
        protected void RaiseCloseRequest()
        {
            var handler = CloseRequest;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private FullyObservableCollection<InventarizationEquipment> _equipments = new FullyObservableCollection<InventarizationEquipment>();
        public FullyObservableCollection<InventarizationEquipment> Equipments
        {
            get => _equipments;
            set 
            {
                Set(ref _equipments, value);
            } 
        }


        private bool _access = true;
        public bool Access
        {
            get => _access;
            set => Set(ref _access, value);
        }

        /// <summary>Дата проведения инвентаризации</summary>
        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get => _date;
            set => Set(ref _date, value);
        }

        /// <summary>Ответственное лицо</summary>
        private string _responsiblePerson;
        public string ResponsiblePerson
        {
            get => _responsiblePerson;
            set => Set(ref _responsiblePerson, value);
        }

        private InventarizationEquipment _selectedEquipments;
        public InventarizationEquipment SelectedEquipments
        {
            get => _selectedEquipments;
            set
            {
                Set(ref _selectedEquipments, value);
            }
        }

        private int _inventarizationSumActual;
        public int InventarizationSumActual
        {
            get => _inventarizationSumActual;
            set => Set(ref _inventarizationSumActual, value);
        }

        private int _inventarizationSum;
        public int InventarizationSum
        {
            get => _inventarizationSum;
            set => Set(ref _inventarizationSum, value);
        }


        #region Добавление оборудования

        /// <summary>Вызывает окно для добавления оборудования</summary>
        public ICommand AddEquipmentCommand { get; set; }

        /// <summary>Вызывает окно для добавления оборудования</summary>
        private void OnAddEquipmentCommandExecute(object parameter)
        {
            try
            {
                InventoryViewAddEquipmentWindow chooseEquipmentWindow = new InventoryViewAddEquipmentWindow();
                chooseEquipmentWindow.ShowDialog();
                InventoryViewAddEquipmentWindowViewModel chooseEquipmentVM = (InventoryViewAddEquipmentWindowViewModel)chooseEquipmentWindow.DataContext;
                if (chooseEquipmentVM.SelectedEquipment != null)
                {
                    var selected = chooseEquipmentVM.SelectedEquipment;
                    var item = new InventarizationEquipment() {Id = selected.Id, Name = selected.Name, Count = selected.Count, Price = selected.Price, Sum = selected.Sum };
                    Equipments.Add(item);
                }

                if (Equipments.Count > 0)
                {
                    int inventarizationSumActual = 0;
                    int inventarizationSum = 0;
                    foreach (var equipment in Equipments)
                    {
                        inventarizationSumActual += equipment.SumActual;
                        inventarizationSum += equipment.Sum;
                    }

                    InventarizationSum = inventarizationSum;
                    InventarizationSumActual = inventarizationSumActual;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Произошла непредвиденная ошибка при добавлении оборудования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>Проверяет можно ли вызвать окно для добавления оборудования</summary>
        private bool CanAddEquipmentCommandExecuted(object parameter) => true;

        #endregion

        #region Удаление оборудования

        public ICommand RemoveEquipmentCommand { get; set; }

        private void OnRemoveEquipmentCommandExecute(object parameter)
        {
            if (SelectedEquipments != null)
            {
                Equipments.Remove(SelectedEquipments);
            }
        }

        private bool CanRemoveEquipmentCommandExecuted(object parameter) => true;

        #endregion

        #region Сохранение инвентаризации

        public ICommand SaveInventarizationCommand { get; set; }

        private void OnSaveInventarizationCommandExecute(object parameter)
        {
            try
            {
                string infoMessage = null;
                if (string.IsNullOrWhiteSpace(ResponsiblePerson))
                {
                    infoMessage = "Необходимо ввести ответственное лицо";
                }
                else if (Equipments.Count == 0)
                {
                    infoMessage = "Вы не добавили оборудование в список";
                }

                if (infoMessage != null)
                {
                    MessageBox.Show(infoMessage, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                
                if (infoMessage == null && Equipments.Count > 0)
                {
                    using (InventarizationContext db = new InventarizationContext())
                    {
                        Inventarization inventarization = new Inventarization();
                        foreach (InventarizationEquipment invEqip in Equipments)
                        {
                            inventarization.InventarizationEquipment = new List<InventarizationEquipment>();
                            inventarization.InventarizationEquipment.Add(invEqip);
                        }

                        inventarization.Date = this.Date;
                        inventarization.ResponsiblePerson = ResponsiblePerson;
                        inventarization.Sum = InventarizationSum;
                        inventarization.SumActual = InventarizationSumActual;

                        db.Inventarizations.Add(inventarization);
                        db.SaveChanges();
                        MessageBox.Show("Инвентаризация успешно произведена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        Access = false;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Произошла непредвиденная ошибка при проведении инвентаризации оборудования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool CanSaveInventarizationCommandExecuted(object parameter) => true;

        #endregion

        public void InitCommands()
        {
            AddEquipmentCommand = new RelayCommand(OnAddEquipmentCommandExecute, CanAddEquipmentCommandExecuted);
            RemoveEquipmentCommand = new RelayCommand(OnRemoveEquipmentCommandExecute, CanRemoveEquipmentCommandExecuted);
            SaveInventarizationCommand = new RelayCommand(OnSaveInventarizationCommandExecute, CanSaveInventarizationCommandExecuted);
        }

        public InventoryViewModel()
        {
            InitCommands();
        }
    }
}
