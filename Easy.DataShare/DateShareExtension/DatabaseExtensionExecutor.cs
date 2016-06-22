using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.DataShare.DateShareExtension
{
    public class DatabaseExtensionExecutor
    {
        public ExtensionResult Run(DatabaseTemplate template)
        {
            DateTime begin = DateTime.Now;
            DateTime end = DateTime.Now.AddMonths(template.MonthStep);

            string databaseName = this.GetDatabaseName(template.DatabaseNamePrefix, begin, end);
            this.CreateDatabase(template.DatabaseDrive, template.CreateDatabaseSql, databaseName, template.ConnectionDatabaseString);

            string databaseConnectionString = string.Format(template.DatabaseConnectionStringTemplate, databaseName);

            this.CreateTable(template.DatabaseDrive, databaseConnectionString, template.CreateTableSql);

            return new ExtensionResult()
            {
                Begin = begin,
                End = end,
                ConnectionString = databaseConnectionString
            };

        }

        private void CreateTable(string databaseDrive, string connectionString, string createTableSql)
        {
            var connection = GetConnection(databaseDrive, connectionString);

            try
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = createTableSql;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        private void CreateDatabase(string databaseDrive,string createDatabaseSql,string databaseName,string connectionString)
        {
            var connection = GetConnection(databaseDrive, connectionString);
            try
            {
                connection.Open();

                var createDatabaseCommand = connection.CreateCommand();
                createDatabaseCommand.CommandText = string.Format(createDatabaseSql, databaseName);
                createDatabaseCommand.CommandType = CommandType.Text;
                createDatabaseCommand.ExecuteNonQuery();

            }
            finally
            {
                connection.Close();
            }
        }

        private IDbConnection GetConnection(string databaseDrive,string connectionString)
        {
            var type = Type.GetType(databaseDrive);

            return Activator.CreateInstance(type,new object[1] { connectionString}) as IDbConnection;
        }

        private String GetDatabaseName(string prefixName, DateTime begin, DateTime end)
        {
            string beginString = begin.Date.ToString("yyyyMMdd");
            string endString = end.Date.ToString("yyyyMMdd");

            return $"{prefixName}_{beginString}_{endString}";
        }

        
    }
}
