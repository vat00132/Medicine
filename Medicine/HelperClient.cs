using Medicine.DBHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine
{
    public class HelperClient
    {
        private ProductDBHelper ProductDBHelper;
        private PharmacyDBHelper PharmacyDBHelper;
        private WarehouseDBHelper WarehouseDBHelper;
        private BatchDBHelper BatchDBHelper;

        public HelperClient()
        {
            DBProvider provider = new DBProvider();
            ProductDBHelper = new ProductDBHelper(provider);
            PharmacyDBHelper = new PharmacyDBHelper(provider);
            WarehouseDBHelper = new WarehouseDBHelper(provider);
            BatchDBHelper = new BatchDBHelper(provider);
        }
    }
}
