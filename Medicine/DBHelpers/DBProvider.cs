using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.DBHelpers
{
    public class DBProvider
    {
        /// <summary>
        /// Источник данных
        /// </summary>
        private const string DataSource = "DESKTOP-6CO4U8L\\SQLEXPRESS";
        /// <summary>
        /// БД
        /// </summary>
        private const string InitialCatalog = "Medicine";
        /// <summary>
        /// Строка подключения
        /// </summary>
        private readonly string connectionString = new SqlConnectionStringBuilder
        {
            DataSource = DataSource,
            InitialCatalog = InitialCatalog,
            IntegratedSecurity = true
        }.ConnectionString;
        /// <summary>
        /// Подключение к БД
        /// </summary>
        private SqlConnection SqlConnection;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DBProvider()
        {
            SqlConnection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Создание sql запроса
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <returns></returns>
        public SqlCommand SqlCommand(string query)
        {
            var command = new SqlCommand
            {
                Connection = SqlConnection,
                CommandText = query
            };
            return command;
        }

        /// <summary>
        /// Открыть соединение
        /// </summary>
        public void OpenConnection()
        {
            SqlConnection.Open();
        }

        /// <summary>
        /// Закрыть соединение
        /// </summary>
        public void CloseConnection()
        {
            SqlConnection.Close();
        }
    }
}
