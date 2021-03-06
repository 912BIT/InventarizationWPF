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
using System.Windows.Shapes;

namespace InventarizationWPF.Views
{
    /// <summary>
    /// Interaction logic for InventoryViewAddEquipmentWindow.xaml
    /// </summary>
    public partial class InventoryViewAddEquipmentWindow : Window
    {
        public InventoryViewAddEquipmentWindow()
        {
            InitializeComponent();
            ((InventoryViewAddEquipmentWindowViewModel)this.DataContext).CloseRequest += (s, e) => this.Close();
        }
    }
}
