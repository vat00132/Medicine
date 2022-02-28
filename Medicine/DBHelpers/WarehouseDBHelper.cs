using Medicine.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.DBHelpers
{
    public class WarehouseDBHelper : DBHelper
    {
        /// <summary>
        /// Название таблицы складов в БД
        /// </summary>
        private const string WAREHOUSE_TABLE = "Warehouses";

        public WarehouseDBHelper(DBProvider provider)
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
            command.Parameters.AddWithValue("@Table", WAREHOUSE_TABLE);

            //Данной таблицы нет
            if ((int)command.ExecuteScalar() == 0)
            {
                command = DBProvider.SqlCommand(
                    @"CREATE TABLE " + WAREHOUSE_TABLE + " (ID uniqueidentifier NOT NULL, IDPharmacies uniqueidentifier NOT NULL, Name NVARCHAR(255) NOT NULL)");
                command.ExecuteNonQuery();
            }

            DBProvider.CloseConnection();
        }

        /// <summary>
        /// Создать новый склад
        /// </summary>
        public override void CreateItem(Model model)
        {
            WarehouseModel warehouse = model as WarehouseModel;
            if (warehouse == null) return;

            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"INSERT INTO " + WAREHOUSE_TABLE + " (ID, IDPharmacies, Name) VALUES (@ID, @IDPharmacies, @Name)");
            command.Parameters.AddWithValue("@ID", warehouse.ID);
            command.Parameters.AddWithValue("@IDPharmacies", warehouse.IDPharmacies);
            command.Parameters.AddWithValue("@Name", warehouse.Name);
            command.ExecuteNonQuery();

            DBProvider.CloseConnection();
        }

        /// <summary>
        /// Удаление склада по id
        /// </summary>
        /// <param name="id">id склада</param>
        public override void DeleteItem(Guid id)
        {
            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"DELETE  FROM " + WAREHOUSE_TABLE + " WHERE ID=@ID");
            command.Parameters.AddWithValue("@ID", id);
            command.ExecuteNonQuery();

            DBProvider.CloseConnection();
        }

        /// <summary>
        /// Удаление склада по названию
        /// </summary>
        /// <param name="name">Название склада</param>
        public override void DeleteItem(string name)
        {
            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"DELETE  FROM " + WAREHOUSE_TABLE + " WHERE Name=@Name");
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
            List<WarehouseModel> models = new List<WarehouseModel>();

            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"SELECT * FROM " + WAREHOUSE_TABLE);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                //Если есть данные
                if (reader.HasRows)
                {
                    //Построчное считывание
                    while (reader.Read())
                    {
                        models.Add(
                            new WarehouseModel(
                                reader.GetGuid(0),  //id
                                reader.GetGuid(1),  //id аптеки
                                reader.GetString(2)));  //Название
                    }
                }
            }

            DBProvider.CloseConnection();

            return models;
        }

        /// <summary>
        /// Получить все записи с заданным названием
        /// </summary>
        /// <param name="name">Название</param>
        /// <returns></returns>
        public override IEnumerable<Model> GetValuesByName(string name)
        {
            List<WarehouseModel> models = new List<WarehouseModel>();

            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"SELECT * FROM " + WAREHOUSE_TABLE + " WHERE Name=@Name");
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
                            new WarehouseModel(
                                reader.GetGuid(0),  //id
                                reader.GetGuid(1),  //id аптеки
                                reader.GetString(3)));  //Название
                    }
                }
            }

            DBProvider.CloseConnection();

            return models;
        }
    }
}
