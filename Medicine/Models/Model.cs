using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.Models
{
    public abstract class Model
    {
        /// <summary>
        /// ID 
        /// </summary>
        public Guid ID { get; private set; }
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; private set; }

        public Model(Guid id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
