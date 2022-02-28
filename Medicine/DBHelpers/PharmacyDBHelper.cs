using Medicine.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.DBHelpers
{
    public class PharmacyDBHelper : DBHelper
    {
        /// <summary>
        /// Название таблицы аптек в БД
        /// </summary>
        private const string PHARMACY_TABLE = "Pharmacies";

        public PharmacyDBHelper(DBProvider provider)
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
            command.Parameters.AddWithValue("@Table", PHARMACY_TABLE);

            //Данной таблицы нет
            if ((int)command.ExecuteScalar() == 0)
            {
                command = DBProvider.SqlCommand(
                    @"CREATE TABLE " + PHARMACY_TABLE + " (ID uniqueidentifier NOT NULL, Name NVARCHAR(255) NOT NULL, Address NVARCHAR(255) NOT NULL, Phone NVARCHAR(20) NOT NULL)");
                command.ExecuteNonQuery();
            }

            DBProvider.CloseConnection();
        }

        /// <summary>
        /// Создать аптеку
        /// </summary>
        /// <param name="name">Название</param>
        public override void CreateItem(Model model)
        {
            PharmacyModel pharmacy = model as PharmacyModel;
            if (pharmacy == null)
                return;

            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"INSERT INTO " + PHARMACY_TABLE + " (ID, Name, Address, Phone) VALUES (@ID, @Name, @Address, @Phone)");
            command.Parameters.AddWithValue("@ID", pharmacy.ID);
            command.Parameters.AddWithValue("@Name", pharmacy.Name);
            command.Parameters.AddWithValue("@Address", pharmacy.Address);
            command.Parameters.AddWithValue("@Phone", pharmacy.Phone);
            command.ExecuteNonQuery();

            DBProvider.CloseConnection();
        }

        /// <summary>
        /// Удаление аптеки по id
        /// </summary>
        /// <param name="id">id аптеки</param>
        public override void DeleteItem(Guid id)
        {
            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"DELETE  FROM " + PHARMACY_TABLE + " WHERE ID=@ID");
            command.Parameters.AddWithValue("@ID", id);
            command.ExecuteNonQuery();

            DBProvider.CloseConnection();
        }

        /// <summary>
        /// Удаление аптеки по названию
        /// </summary>
        /// <param name="name">Название аптеки</param>
        public override void DeleteItem(string name)
        {
            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"DELETE  FROM " + PHARMACY_TABLE + " WHERE Name=@Name");
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
            List<PharmacyModel> models = new List<PharmacyModel>();

            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"SELECT * FROM " + PHARMACY_TABLE);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                //Если есть данные
                if (reader.HasRows)
                {
                    //Построчное считывание
                    while (reader.Read())
                    {
                        models.Add(
                            new PharmacyModel(
                                reader.GetGuid(0),  //id
                                reader.GetString(1),    //Название
                                reader.GetString(2),    //Адрес
                                reader.GetString(3)));  //Телефон
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
            List<PharmacyModel> models = new List<PharmacyModel>();

            DBProvider.OpenConnection();

            var command = DBProvider.SqlCommand(
                @"SELECT * FROM " + PHARMACY_TABLE + " WHERE Name=@Name");
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
                            new PharmacyModel(
                                reader.GetGuid(0),  //id
                                reader.GetString(1),    //Название
                                reader.GetString(2),    //Адрес
                                reader.GetString(3)));  //Телефон
                    }
                }
            }

            DBProvider.CloseConnection();

            return models;
        }
    }
}
