using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarizationWPF.Models
{
    internal class Inventarization
    {
        public int Id { get; set; }

        /// <summary>Дата проведения инвентаризации</summary>
        public DateTime Date { get; set; }

        /// <summary>Ответственное лицо</summary>
        public string ResponsiblePerson { get; set; }

        public int SumActual { get; set; }

        public int Sum { get; set; }

        public List<InventarizationEquipment> InventarizationEquipment { get; set; }

        public Inventarization()
        {

        }
    }
}
