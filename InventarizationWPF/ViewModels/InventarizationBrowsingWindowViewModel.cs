using InventarizationWPF.Infrastructure;
using InventarizationWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarizationWPF.ViewModels
{
    internal class InventarizationBrowsingWindowViewModel : ViewModel
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

    }
}
