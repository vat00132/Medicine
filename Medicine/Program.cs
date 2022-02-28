using Medicine.DBHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine
{
    class Program
    {
        private static HelperClient Client;
        static void Main(string[] args)
        {
            Client = new HelperClient();

            Console.ReadKey();
        }
    }
}
