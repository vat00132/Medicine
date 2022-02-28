using Medicine.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.DBHelpers
{
    public class BatchDBHelper : DBHelper
    {
        /// <summary>
        /// Название таблицы партий в БД
        /// </summary>
        private const string BATCH_TABLE = "Batches";

        public BatchDBHelper(DBProvider provider)
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
            command.Parameters.AddWithValue("@Table", BATCH_TABLE);

            //Данной таблицы нет
            if ((int)command.ExecuteScalar() == 0)
            {
                command = DBProvider.SqlCommand(
                    @"CREATE TABLE " + BATCH_TABLE + " (ID uniqueidentifier NOT NULL, IDProduct uniqueidentifier NOT NULL, IDWarehouse uniqueidentifier NOT NULL, Count INT NOT NULL, Name NVARCHAR(255) NOT NULL)");
                command.ExecuteNonQuery();
            }

            DBProvider.CloseConnection();
        }

        /// <summary>
        /// Создание новой партий
        /// </summary>
        public override void CreateItem(Model model)
        {
            BatchModel batch = model as BatchModel;
            if (batch == null) return;

            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"INSERT INTO " + BATCH_TABLE + " (ID, IDProduct, IDWarehouse, Count, Name) VALUES (@ID, @IDProduct, @IDWarehouse, @Count, @Name)");
            command.Parameters.AddWithValue("@ID", batch.ID);
            command.Parameters.AddWithValue("@IDProduct", batch.IDProduct);
            command.Parameters.AddWithValue("@IDWarehouse", batch.IDWarehouse);
            command.Parameters.AddWithValue("@Count", batch.Count);
            command.Parameters.AddWithValue("@Name", batch.Name);
            command.ExecuteNonQuery();

            DBProvider.CloseConnection();
        }

        /// <summary>
        /// Удаление партий по id
        /// </summary>
        /// <param name="id">id партий</param>
        public override void DeleteItem(Guid id)
        {
            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"DELETE  FROM " + BATCH_TABLE + " WHERE ID=@ID");
            command.Parameters.AddWithValue("@ID", id);
            command.ExecuteNonQuery();

            DBProvider.CloseConnection();
        }

        /// <summary>
        /// Удаление партий по названию
        /// </summary>
        /// <param name="name">Название партий</param>
        public override void DeleteItem(string name)
        {
            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"DELETE  FROM " + BATCH_TABLE + " WHERE Name=@Name");
            command.Parameters.AddWithValue("@Name", name);
            command.ExecuteNonQuery();

            DBProvider.CloseConnection();
        }

        /// <summary>
        /// Получить все значения из таблицы
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Model> GetAllValues()
        {
            List<BatchModel> models = new List<BatchModel>();

            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"SELECT * FROM " + BATCH_TABLE);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                //Если есть данные
                if (reader.HasRows)
                {
                    //Построчное считывание
                    while (reader.Read())
                    {
                        models.Add(
                            new BatchModel(
                                reader.GetGuid(0),  //id
                                reader.GetGuid(1),  //id товара
                                reader.GetGuid(2),  //id склада
                                reader.GetInt32(3), //Количество товара в партий
                                reader.GetString(4)));  //Название
                    }
                }
            }

            DBProvider.CloseConnection();

            return models;
        }

        /// <summary>
        /// Получить все записи с заданным названием
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override IEnumerable<Model> GetValuesByName(string name)
        {
            List<BatchModel> models = new List<BatchModel>();

            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"SELECT * FROM " + BATCH_TABLE + " WHERE Name=@Name");
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
                            new BatchModel(
                                reader.GetGuid(0),  //id
                                reader.GetGuid(1),  //id товара
                                reader.GetGuid(2),  //id склада
                                reader.GetInt32(3), //Количество товара в партий
                                reader.GetString(4)));  //Название
                    }
                }
            }

            DBProvider.CloseConnection();

            return models;
        }
    }
}
