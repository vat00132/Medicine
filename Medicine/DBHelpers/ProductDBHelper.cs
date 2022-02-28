using Medicine.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.DBHelpers
{
    public class ProductDBHelper : DBHelper
    {
        /// <summary>
        /// Название таблицы товаров в БД
        /// </summary>
        private const string PRODUCT_TABLE = "Products";

        public ProductDBHelper(DBProvider provider)
            : base(provider)
        {
            CheckTable();
        }

        /// <summary>
        /// Проверка на наличие таблицы
        /// </summary>
        protected override void CheckTable()
        {
            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"SELECT COUNT(*) FROM sys.sysobjects WHERE name=@Table");
            command.Parameters.AddWithValue("@Table", PRODUCT_TABLE);

            //Данной таблицы нет
            if ((int)command.ExecuteScalar() == 0)
            {
                command = DBProvider.SqlCommand(
                    @"CREATE TABLE " + PRODUCT_TABLE + " (ID uniqueidentifier NOT NULL, Name NVARCHAR(255) NOT NULL)");
                command.ExecuteNonQuery();
            }

            DBProvider.CloseConnection();
        }

        /// <summary>
        /// Создать товар
        /// </summary>
        /// <param name="name">Название</param>
        public override void CreateItem(Model model)
        {
            ProductModel product = model as ProductModel;
            if (model == null)
                return;

            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"INSERT INTO " + PRODUCT_TABLE + " (ID, Name) VALUES (@ID, @Name)");
            command.Parameters.AddWithValue("@ID", product.ID);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.ExecuteNonQuery();

            DBProvider.CloseConnection();
        }

        /// <summary>
        /// Удаление товара по id
        /// </summary>
        /// <param name="id">id товара</param>
        public override void DeleteItem(Guid id)
        {
            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"DELETE  FROM " + PRODUCT_TABLE + " WHERE ID=@ID");
            command.Parameters.AddWithValue("@ID", id);
            command.ExecuteNonQuery();

            DBProvider.CloseConnection();
        }

        /// <summary>
        /// Удаление товара по названию
        /// </summary>
        /// <param name="name">Название товара</param>
        public override void DeleteItem(string name)
        {
            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"DELETE  FROM " + PRODUCT_TABLE + " WHERE Name=@Name");
            command.Parameters.AddWithValue("@Name", name);
            command.ExecuteNonQuery();

            DBProvider.CloseConnection();
        }

        /// <summary>
        /// Получить все записи из таблицы
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Model> GetAllValues()
        {
            List<ProductModel> models = new List<ProductModel>();

            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"SELECT * FROM " + PRODUCT_TABLE);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                //Если есть данные
                if (reader.HasRows)
                {
                    //Построчное считывание
                    while (reader.Read())
                    {
                        models.Add(
                            new ProductModel(
                                reader.GetGuid(0),  //id
                                reader.GetString(1)));  //Название
                    }
                }
            }

            DBProvider.CloseConnection();

            return models;
        }

        /// <summary>
        /// Получить записи с заданным названием
        /// </summary>
        /// <param name="name">Название</param>
        /// <returns></returns>
        public override IEnumerable<Model> GetValuesByName(string name)
        {
            List<ProductModel> models = new List<ProductModel>();

            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"SELECT * FROM " + PRODUCT_TABLE + " WHERE Name=@Name");
            command.Parameters.AddWithValue("@Name", name);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                //Если есть данные
                if (reader.HasRows)
                {
                    //Построчное считывание
                    while (reader.Read())
                    {
                        models.Add(
                            new ProductModel(
                                reader.GetGuid(0),  //id
                                reader.GetString(1)));  //Название
                    }
                }
            }

            DBProvider.CloseConnection();

            return models;
        }
    }
}
