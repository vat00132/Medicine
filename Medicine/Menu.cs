using Medicine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine
{
    public class Menu
    {
        private HelperClient Client;
        public Menu(HelperClient client)
        {
            Client = client;

            MainMenu();
        }

        /// <summary>
        /// Главное меню
        /// </summary>
        private void MainMenu()
        {
            int selectedNumber = -1;

            while (selectedNumber != 0)
            {
                Console.WriteLine(Messages.MainMenu);
                if (int.TryParse(Console.ReadLine(), out selectedNumber))
                {
                    if (selectedNumber < 0 || selectedNumber > 4)
                    {
                        Console.WriteLine("Введен неверный номер!");
                        continue;
                    }
                    //Спуск в меню по иерархий
                    switch (selectedNumber)
                    {
                        case 1: //Товары
                            ProductMenu();
                            break;
                        case 2: //Аптеки
                            PharmacyMenu();
                            break;
                        case 3: //Склады
                            WarehouseMenu();
                            break;
                        case 4: //Партии
                            BatchMenu();
                            break;
                        case 0: //Выход
                            return;
                    }
                }
                else
                {
                    Console.WriteLine("Введен неверный номер!");
                }
            }
        }

        #region Products
        /// <summary>
        /// Меню товаров
        /// </summary>
        private void ProductMenu()
        {
            int selectedNumber = -1;

            while (selectedNumber != 0)
            {
                Console.WriteLine("Меню товаров:\n" + Messages.Menu + Messages.EndMenu);
                if (int.TryParse(Console.ReadLine(), out selectedNumber))
                {
                    if (selectedNumber < 0 || selectedNumber > 3)
                    {
                        Console.WriteLine("Введен неверный номер!");
                        continue;
                    }
                    //Спуск в меню по иерархий
                    switch (selectedNumber)
                    {
                        case 1: //Вывести все записи
                            WriteAllProducts();
                            break;
                        case 2: //Добавить новую запись
                            AddProduct();
                            break;
                        case 3: //Удалить запись
                            DeleteProduct();
                            break;
                        case 0: //Назад
                            return;
                    }
                }
                else
                {
                    Console.WriteLine("Введен неверный номер!");
                }
            }
        }

        private void WriteAllProducts()
        {
            Console.WriteLine("Вывод всех товаров:");
            for (int i = 0; i < Client.Products.Count; i++)
            {
                Console.WriteLine(
                    (i + 1) + 
                    ".   Название: " + Client.Products[i].Name);
            }
        }

        private void AddProduct()
        {
            Console.WriteLine("Добавление нового товара:");
            Console.WriteLine("Напишите название:");
            string name = Console.ReadLine().Replace(" ", "");
            if (name.Length == 0)
            {
                Console.WriteLine("Название введено неверно!");
            }
            else
            {
                Client.AddProduct(name);
            }
        }

        private void DeleteProduct()
        {
            Console.WriteLine("Удаление товара:");
            Console.WriteLine("Напишите номер удаляемого товара:");
            int number = -1;
            if (!int.TryParse(Console.ReadLine(), out number) ||
                number < 1 || 
                number > Client.Products.Count)
            {
                Console.WriteLine("Введен неверный номер!");
                return;
            }

            Client.DeleteProduct(number - 1);
        }
        #endregion

        #region Pharmacies
        /// <summary>
        /// Меню аптек
        /// </summary>
        private void PharmacyMenu()
        {
            int selectedNumber = -1;

            while (selectedNumber != 0)
            {
                Console.WriteLine("Меню аптек:\n" + Messages.Menu + Messages.PharmacyMenu + Messages.EndMenu);
                if (int.TryParse(Console.ReadLine(), out selectedNumber))
                {
                    if (selectedNumber < 0 || selectedNumber > 4)
                    {
                        Console.WriteLine("Введен неверный номер!");
                        continue;
                    }
                    //Спуск в меню по иерархий
                    switch (selectedNumber)
                    {
                        case 1: //Вывести все записи
                            WriteAllPharmacies();
                            break;
                        case 2: //Добавить новую запись
                            AddPharmacy();
                            break;
                        case 3: //Удалить запись
                            DeletePharmacy();
                            break;
                        case 4: //Доп. задание
                            GetAllProductsForSelectedPharmacy();
                            break;
                        case 0: //Назад
                            return;
                    }
                }
                else
                {
                    Console.WriteLine("Введен неверный номер!");
                }
            }
        }

        private void GetAllProductsForSelectedPharmacy()
        {
            Console.WriteLine("Вывод всего списка товаров и его количества для выбранной аптеки:");
            Console.WriteLine("Напишите номер аптеки:");

            int number = -1;
            if (!int.TryParse(Console.ReadLine(), out number) ||
                number < 1 ||
                number > Client.Pharmacies.Count)
            {
                Console.WriteLine("Введен неверный номер!");
                return;
            }

            Dictionary<ProductModel, int> products = Client.GetAllProductsForSelectedPharmacy(number - 1);
            foreach (var product in products)
            {
                Console.WriteLine("1.   Название: " + product.Key.Name + ",     Количество: " + product.Value);
            }
        }

        private void WriteAllPharmacies()
        {
            Console.WriteLine("Вывод всех аптек:");
            for (int i = 0; i < Client.Pharmacies.Count; i++)
            {
                Console.WriteLine(
                    (i + 1) +
                    ".  Название:  " + Client.Pharmacies[i].Name +
                    ",  адрес: " + Client.Pharmacies[i].Address +
                    ",   телефон:" + Client.Pharmacies[i].Phone);
            }
        }

        private void AddPharmacy()
        {
            Console.WriteLine("Добавление новой аптеки:");

            Console.WriteLine("Напишите название:");
            string name = Console.ReadLine().Replace(" ", "");
            if (name.Length == 0)
            {
                Console.WriteLine("Название введено неверно!");
                return;
            }

            Console.WriteLine("Напишите адрес:");
            string address = Console.ReadLine().Replace(" ", "");
            if (address.Length == 0)
            {
                Console.WriteLine("Адресс введен неверно!");
                return;
            }

            Console.WriteLine("Напишите телефон:");
            string phone = Console.ReadLine().Replace(" ", "");
            if (address.Length == 0 || address.Length > 20)
            {
                Console.WriteLine("Номер введен неверно!");
                return;
            }

            Client.AddPharmacy(name, address, phone);
        }

        private void DeletePharmacy()
        {
            Console.WriteLine("Удаление аптеки:");
            Console.WriteLine("Напишите номер удаляемой аптеки:");
            int number = -1;
            if (!int.TryParse(Console.ReadLine(), out number) ||
                number < 1 || 
                number > Client.Pharmacies.Count)
            {
                Console.WriteLine("Введен неверный номер!");
                return;
            }
            Client.DeletePharmacy(number - 1);
        }
        #endregion

        #region Warehouses
        /// <summary>
        /// Меню складов
        /// </summary>
        private void WarehouseMenu()
        {
            int selectedNumber = -1;

            while (selectedNumber != 0)
            {
                Console.WriteLine("Меню складов:\n" + Messages.Menu + Messages.EndMenu);
                if (int.TryParse(Console.ReadLine(), out selectedNumber))
                {
                    if (selectedNumber < 0 || selectedNumber > 3)
                    {
                        Console.WriteLine("Введен неверный номер!");
                        continue;
                    }
                    //Спуск в меню по иерархий
                    switch (selectedNumber)
                    {
                        case 1: //Вывести все записи
                            WriteAllWarehouses();
                            break;
                        case 2: //Добавить новую запись
                            AddWarehouse();
                            break;
                        case 3: //Удалить запись
                            DeleteWarehouse();
                            break;
                        case 0: //Назад
                            return;
                    }
                }
                else
                {
                    Console.WriteLine("Введен неверный номер!");
                }
            }
        }

        private void WriteAllWarehouses()
        {
            Console.WriteLine("Вывод всех складов:");
            for (int i = 0; i < Client.Warehouses.Count; i++)
            {
                Console.WriteLine(
                    (i + 1) +
                    ".  Название:  " + Client.Warehouses[i].Name);
            }
        }

        private void AddWarehouse()
        {
            Console.WriteLine("Добавление нового склада:");

            Console.WriteLine("Напишите название:");
            string name = Console.ReadLine().Replace(" ", "");
            if (name.Length == 0)
            {
                Console.WriteLine("Название введено неверно!");
                return;
            }

            Console.WriteLine("Напишите номер аптеки:");
            int numberPharmacy;
            if (!int.TryParse(Console.ReadLine().Replace(" ", ""), out numberPharmacy) ||
                numberPharmacy < 1 || 
                numberPharmacy > Client.Pharmacies.Count)
            {
                Console.WriteLine("Введен неверный номер аптеки!");
                return;
            }

            Client.AddWarehouse(name, numberPharmacy - 1);
        }

        private void DeleteWarehouse()
        {
            Console.WriteLine("Удаление склада:");
            Console.WriteLine("Напишите номер удаляемого склада:");
            int number = -1;
            if (!int.TryParse(Console.ReadLine(), out number) ||
                number < 1 || 
                number > Client.Warehouses.Count)
            {
                Console.WriteLine("Введен неверный номер!");
                return;
            }
            Client.DeleteWarehouse(number - 1);
        }
        #endregion

        #region Batch
        /// <summary>
        /// Меню партий
        /// </summary>
        private void BatchMenu()
        {
            int selectedNumber = -1;

            while (selectedNumber != 0)
            {
                Console.WriteLine("Меню партии:\n" + Messages.Menu + Messages.EndMenu);
                if (int.TryParse(Console.ReadLine(), out selectedNumber))
                {
                    if (selectedNumber < 0 || selectedNumber > 3)
                    {
                        Console.WriteLine("Введен неверный номер!");
                        continue;
                    }
                    //Спуск в меню по иерархий
                    switch (selectedNumber)
                    {
                        case 1: //Вывести все записи
                            WriteAllBatches();
                            break;
                        case 2: //Добавить новую запись
                            AddBatch();
                            break;
                        case 3: //Удалить запись
                            DeleteBatch();
                            break;
                        case 0: //Назад
                            return;
                    }
                }
                else
                {
                    Console.WriteLine("Введен неверный номер!");
                }
            }
        }

        private void WriteAllBatches()
        {
            Console.WriteLine("Вывод всех партий:");
            for (int i = 0; i < Client.Batches.Count; i++)
            {
                Console.WriteLine(
                    (i + 1) +
                    ".  Название:  " + Client.Batches[i].Name +
                    "   Количество: " + Client.Batches[i].Count);
            }
        }

        private void AddBatch()
        {
            Console.WriteLine("Добавление новой партий:");

            Console.WriteLine("Напишите название:");
            string name = Console.ReadLine().Replace(" ", "");
            if (name.Length == 0)
            {
                Console.WriteLine("Название введено неверно!");
                return;
            }

            Console.WriteLine("Напишите количество:");
            int count;
            if (!int.TryParse(Console.ReadLine().Replace(" ", ""), out count))
            {
                Console.WriteLine("Введено неверное количество!");
                return;
            }

            Console.WriteLine("Напишите номер товара:");
            int numberProduct;
            if (!int.TryParse(Console.ReadLine().Replace(" ", ""), out numberProduct) ||
                numberProduct < 1 || 
                numberProduct > Client.Products.Count)
            {
                Console.WriteLine("Введен неверный номер товара!");
                return;
            }

            Console.WriteLine("Напишите номер склада:");
            int numberWarehouse;
            if (!int.TryParse(Console.ReadLine().Replace(" ", ""), out numberWarehouse) ||
                numberWarehouse < 1 || 
                numberWarehouse > Client.Warehouses.Count)
            {
                Console.WriteLine("Введен неверный номер склада!");
                return;
            }

            Client.AddBatch(name, count, numberProduct - 1, numberWarehouse - 1);
        }

        private void DeleteBatch()
        {
            Console.WriteLine("Удаление партии:");
            Console.WriteLine("Напишите номер удаляемой партии:");
            int number = -1;
            if (!int.TryParse(Console.ReadLine(), out number) ||
                number < 1 ||
                number > Client.Warehouses.Count)
            {
                Console.WriteLine("Введен неверный номер!");
                return;
            }
            Client.DeleteBatch(number - 1);
        }
        #endregion
    }
}
