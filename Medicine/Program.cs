﻿using Medicine.DBHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu(new HelperClient());

            Console.ReadKey();
        }
    }
}
