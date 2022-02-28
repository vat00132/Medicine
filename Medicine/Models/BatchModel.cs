using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.Models
{
    public class BatchModel : Model
    {
        /// <summary>
        /// id товара
        /// </summary>
        public Guid IDProduct { get; private set; }
        //id склада
        public Guid IDWarehouse { get; private set; }
        /// <summary>
        /// Количество товара в партии
        /// </summary>
        public int Count { get; private set; }
        /// <summary>
        /// Конструктор для партии
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="idProduct">id товара</param>
        /// <param name="idWarehouse">id склада</param>
        /// <param name="count">Количсетво товара в партий</param>
        /// <param name="name">Название</param>
        public BatchModel(Guid id, Guid idProduct, Guid idWarehouse, int count, string name)
            : base(id, name)
        {
            IDProduct = idProduct;
            IDWarehouse = idWarehouse;
            Count = count;
        }
    }
}
