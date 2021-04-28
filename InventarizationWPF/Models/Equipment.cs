namespace InventarizationWPF.Models
{
    internal class Equipment
    {
        /// <summary>Id оборудования</summary>
        public int Id { get; set; }

        /// <summary>Наименование оборудования</summary>
        public string Name { get; set; }

        /// <summary>Количество оборудования</summary>
        public int Count { get; set; }

        /// <summary>Цена оборудования</summary>
        public int Price { get; set; }

        /// <summary>Сумма оборудования</summary>
        public int Sum { get; set; }
    }
}
