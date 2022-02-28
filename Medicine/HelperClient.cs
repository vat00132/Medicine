using Medicine.DBHelpers;
using Medicine.Models;
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

        /// <summary>
        /// Товары
        /// </summary>
        public List<ProductModel> Products;
        /// <summary>
        /// Аптеки
        /// </summary>
        public List<PharmacyModel> Pharmacies;
        /// <summary>
        /// Склады
        /// </summary>
        public List<WarehouseModel> Warehouses;
        /// <summary>
        /// Партий
        /// </summary>
        public List<BatchModel> Batches;

        public HelperClient()
        {
            DBProvider provider = new DBProvider();
            ProductDBHelper = new ProductDBHelper(provider);
            PharmacyDBHelper = new PharmacyDBHelper(provider);
            WarehouseDBHelper = new WarehouseDBHelper(provider);
            BatchDBHelper = new BatchDBHelper(provider);

            LoadData();
        }

        /// <summary>
        /// Загрузка данных из БД
        /// </summary>
        public void LoadData()
        {
            Products = new List<ProductModel>();
            Pharmacies = new List<PharmacyModel>();
            Warehouses = new List<WarehouseModel>();
            Batches = new List<BatchModel>();

            foreach (var product in ProductDBHelper.GetAllValues())
            {
                Products.Add(product as ProductModel);
            }

            foreach (var pharmacy in PharmacyDBHelper.GetAllValues())
            {
                Pharmacies.Add(pharmacy as PharmacyModel);
            }

            foreach (var warehouse in WarehouseDBHelper.GetAllValues())
            {
                Warehouses.Add(warehouse as WarehouseModel);
            }

            foreach (var batch in BatchDBHelper.GetAllValues())
            {
                Batches.Add(batch as BatchModel);
            }
        }

        /// <summary>
        /// Вывод всего списка товаров и его количества для выбранной аптеки
        /// </summary>
        /// <param name="index">Индекс аптеки</param>
        public Dictionary<ProductModel, int> GetAllProductsForSelectedPharmacy(int index)
        {
            //Получить все склады для данной аптеки
            var warehouses = Warehouses.Where(warehouse =>
            warehouse.IDPharmacies == Pharmacies[index].ID).ToList();

            //Получить все партий для данных складов
            List<BatchModel> batches = new List<BatchModel>();
            warehouses.ForEach(warehouse =>
            {
                batches.AddRange(
                    Batches.Where(batch =>
                    batch.IDWarehouse == warehouse.ID)
                    .ToList());
            });

            //Получить все товары для данных партий
            Dictionary<ProductModel, int> products = new Dictionary<ProductModel, int>();
            batches.ForEach(batch =>
            {
                //Берем товар из данной партий
                var product = Products.FirstOrDefault(u => u.ID == batch.IDProduct);
                //Если уже есть в словаре
                if (products.ContainsKey(product))
                {
                    products[product] += batch.Count;
                }
                else
                {
                    products.Add(product, batch.Count);
                }
            });

            return products;
        }

        #region Add
        /// <summary>
        /// Добавление нового товара
        /// </summary>
        /// <param name="name">Название</param>
        public void AddProduct(string name)
        {
            ProductModel model = new ProductModel(Guid.NewGuid(), name);
            ProductDBHelper.CreateItem(model);
            Products.Add(model);
        }

        /// <summary>
        /// Добавление новой аптеки
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="address">Адрес</param>
        /// <param name="phone">Телефон</param>
        public void AddPharmacy(string name, string address, string phone)
        {
            PharmacyModel model = new PharmacyModel(Guid.NewGuid(), name, address, phone);
            PharmacyDBHelper.CreateItem(model);
            Pharmacies.Add(model);
        }

        /// <summary>
        /// Добавление нового склада
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="indexPharmacy">Индекс аптеки</param>
        public void AddWarehouse(string name, int indexPharmacy)
        {
            WarehouseModel model = new WarehouseModel(Guid.NewGuid(), Pharmacies[indexPharmacy].ID, name);
            WarehouseDBHelper.CreateItem(model);
            Warehouses.Add(model);
        }

        /// <summary>
        /// Добавление новой партий
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="count">Количество</param>
        /// <param name="indexProduct">Индекс товара</param>
        /// <param name="indexWarehouse">Индекс склада</param>
        public void AddBatch(string name, int count, int indexProduct, int indexWarehouse)
        {
            BatchModel model = new BatchModel(Guid.NewGuid(), Products[indexProduct].ID, Warehouses[indexWarehouse].ID, count, name);
            BatchDBHelper.CreateItem(model);
            Batches.Add(model);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Удаление товара по индексу
        /// </summary>
        /// <param name="index"></param>
        public void DeleteProduct(int index)
        {
            //Удалить все партий с данным твоаром
            for (int i = 0; i < Batches.Count; i++)
            {
                if (Batches[i].IDProduct == Products[index].ID)
                {
                    DeleteBatch(i);
                    i--;
                }
            }

            //Удалить товар
            ProductDBHelper.DeleteItem(Products[index].ID);
            Products.Remove(Products[index]);
        }

        /// <summary>
        /// Удаление аптеки по индексу
        /// </summary>
        /// <param name="index"></param>
        public void DeletePharmacy(int index)
        {
            //Удалить все склады от данной аптеки
            for (int i = 0; i < Warehouses.Count; i++)
            {
                if (Warehouses[i].IDPharmacies == Pharmacies[i].ID)
                {
                    DeleteWarehouse(i);
                    i--;
                }
            }

            //Удалить аптеку
            PharmacyDBHelper.DeleteItem(Pharmacies[index].ID);
            Pharmacies.Remove(Pharmacies[index]);
        }

        /// <summary>
        /// Удаление склада по индексу
        /// </summary>
        /// <param name="index"></param>
        public void DeleteWarehouse(int index)
        {
            //Удалить все партий на данном складе
            for (int i = 0; i < Batches.Count; i++)
            {
                if (Batches[i].IDWarehouse == Warehouses[i].ID)
                {
                    DeleteBatch(i);
                    i--;
                }
            }

            //Удалить склад
            WarehouseDBHelper.DeleteItem(Warehouses[index].ID);
            Warehouses.Remove(Warehouses[index]);
        }

        /// <summary>
        /// Удаление партии по индексу
        /// </summary>
        /// <param name="index"></param>
        public void DeleteBatch(int index)
        {
            BatchDBHelper.DeleteItem(Batches[index].ID);
            Batches.Remove(Batches[index]);
        }
        #endregion
    }
}
