namespace InventarizationWPF.Models
{
    /// <summary>Модель оборудования</summary>
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

        /// <summary>Инициализирует оборудования</summary>
        /// <param name="name">Наименование оборудования</param>
        /// <param name="count">Количество оборудования</param>
        /// <param name="price">Цена оборудования</param>
        /// <param name="sum">Сумма оборудования</param>
        public Equipment(string name, int count, int price, int sum)
        {
            Name = name;
            Count = count;
            Price = price;
            Sum = sum;
        }

        public Equipment()
        {
        }
    }
}
