using InventarizationWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventarizationWPF.Views
{
    /// <summary>
    /// Interaction logic for InventoryView.xaml
    /// </summary>
    public partial class InventoryView : UserControl
    {
        public InventoryView()
        {
            InitializeComponent();
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var vm = ((InventoryViewModel)this.DataContext);

            if (vm.Equipments.Count > 0)
            {
                int inventarizationSumActual = 0;
                int inventarizationSum = 0;
                foreach (var equipment in vm.Equipments)
                {
                    inventarizationSumActual += equipment.SumActual;
                    inventarizationSum += equipment.Sum;
                }

                vm.InventarizationSum = inventarizationSum;
                vm.InventarizationSumActual = inventarizationSumActual;
            }
        }
    }
}
