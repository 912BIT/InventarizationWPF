using InventarizationWPF.Models;
using System.Data.Entity;

namespace InventarizationWPF.Data
{
    /// <summary>Контекст данных для доступа к БД</summary>
    internal class InventarizationContext : DbContext
    {
        /// <summary>Инициализация контекста данных БД</summary>
        public InventarizationContext() : base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=inventarization;Integrated Security=True;")
        {

        }

        /// <summary>Список оборудования</summary>
        public DbSet<Equipment> Equipment { get; set; }
    }
}
