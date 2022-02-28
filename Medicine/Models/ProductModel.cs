using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.Models
{
    public class ProductModel : Model
    {
        /// <summary>
        /// Конструктор товара
        /// </summary>
        /// <param name="id">ID товара</param>
        /// <param name="name">Название товара</param>
        public ProductModel(Guid id, string name)
            : base(id, name)
        {

        }
    }
}
