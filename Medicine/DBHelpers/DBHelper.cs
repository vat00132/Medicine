using Medicine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.DBHelpers
{
    public abstract class DBHelper
    {
        public DBProvider DBProvider { get; private set; }

        public DBHelper(DBProvider provider)
        {
            DBProvider = provider;
        }

        protected abstract void CheckTable();
        public abstract void CreateItem(Model model);
        public abstract void DeleteItem(Guid id);
        public abstract void DeleteItem(string name);
        public abstract IEnumerable<Model> GetAllValues();
        public abstract IEnumerable<Model> GetValuesByName(string name);
    }
}
