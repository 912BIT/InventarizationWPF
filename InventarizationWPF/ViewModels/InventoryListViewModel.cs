using InventarizationWPF.Data;
using InventarizationWPF.Infrastructure.Commands;
using InventarizationWPF.Models;
using InventarizationWPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InventarizationWPF.ViewModels
{
    internal class InventoryListViewModel : ViewModel
    {
        private ObservableCollection<Inventarization> _inventarizations = new ObservableCollection<Inventarization>();
        public ObservableCollection<Inventarization> Inventarizations
        {
            get => _inventarizations;
            set
            {
                Set(ref _inventarizations, value);
            }
        }

        private Inventarization _selectedInventarization;
        public Inventarization SelectedInventarization
        {
            get => _selectedInventarization;
            set
            {
                Set(ref _selectedInventarization, value);
            }
        }

        #region Команды

        #region Удаление инвентаризации

        public ICommand RemoveInventarizationCommand { get; set; }

        private void OnRemoveInventarizationCommandExecute(object parameter)
        {
            MessageBoxResult dialogResult = MessageBox.Show($"Вы действительно хотите удалить инвентаризацию {SelectedInventarization.Id}?", "Удаление инвентаризации", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.OK)
            {
                using (InventarizationContext db = new InventarizationContext())
                {
                    if (SelectedInventarization != null)
                    {
                        db.Entry(SelectedInventarization).State = EntityState.Deleted;
                        db.Inventarizations.Remove(SelectedInventarization);
                        db.SaveChanges();
                    }
                }
                LoadInventarizations();
            }
        }

        private bool CanRemoveEquipmentCommandExecuted(object parameter) => true;

        #endregion

        #region Просмотр инвентаризации

        public ICommand BrowseInventarizationCommand { get; set; }

        private void OnBrowseInventarizationCommandExecute(object parameter)
        {
            InventarizationBrowsingWindowView inventarizationWindow = new InventarizationBrowsingWindowView();
            InventarizationBrowsingWindowViewModel inventarizationVM = (InventarizationBrowsingWindowViewModel)inventarizationWindow.DataContext;
            inventarizationVM.Access = false;
            inventarizationVM.Date = SelectedInventarization.Date;
            inventarizationVM.ResponsiblePerson = SelectedInventarization.ResponsiblePerson;
            inventarizationVM.Equipments.Clear();
            foreach (var inventarizationEqup in SelectedInventarization.InventarizationEquipment)
            {
                inventarizationVM.Equipments.Add(inventarizationEqup);
            }
            inventarizationVM.InventarizationSum = SelectedInventarization.Sum;
            inventarizationVM.InventarizationSumActual = SelectedInventarization.SumActual;
            inventarizationVM.SelectedEquipments = inventarizationVM.Equipments[0];
            inventarizationWindow.ShowDialog();
        }

        private bool CanBrowseEquipmentCommandExecuted(object parameter) => true;

        #endregion

        #endregion

        private void LoadInventarizations()
        {
            Inventarizations.Clear();
            List<Inventarization> inventarizations;

            using (InventarizationContext db = new InventarizationContext())
            {
                inventarizations = db.Inventarizations.ToList();
            }

            foreach (var inventarization in inventarizations)
            {
                Inventarizations.Add(inventarization);
            }

            if (Inventarizations.Count > 0)
            {
                SelectedInventarization = Inventarizations[0];
            }
        }

        public void InitCommands()
        {
            RemoveInventarizationCommand = new RelayCommand(OnRemoveInventarizationCommandExecute, CanRemoveEquipmentCommandExecuted);
            BrowseInventarizationCommand = new RelayCommand(OnBrowseInventarizationCommandExecute, CanBrowseEquipmentCommandExecuted);
        }

        public InventoryListViewModel()
        {
            InitCommands();
            LoadInventarizations();
        }
    }
}
