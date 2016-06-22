using System;
using System.Configuration;
using System.IO;
using Easy.DataShare.DateShareExtension;
using Easy.Public;
using Easy.Rpc.directory;
using Newtonsoft.Json;

namespace DatabaseExpansion
{
    /*
    new DatabaseTemplate()
             {
                 CreateDatabaseSql = "CREATE DATABASE IF NOT EXISTS {0} DEFAULT CHARSET utf8 COLLATE utf8_general_ci;",
                 DatabaseDrive = "MySql.Data.MySqlClient.MySqlConnection,MySql.Data",
                 MonthStep = 12,
                 ConnectionDatabaseString = "Server=127.0.0.1;Port=3306;Uid=etao;Pwd=etao123;Charset=utf8",
                 CreateTableSql = @"CREATE TABLE `db_version` (
                                     `id` INT(11) NOT NULL AUTO_INCREMENT,
                                     `from_version` INT(11) NOT NULL,
                                     `current_version` INT(11) NOT NULL,
                                     `last_update_datetime` DATETIME NOT NULL,
                                                 PRIMARY KEY(`id`)
                                 )",
                 DatabaseNamePrefix = "auto",
                 DatabaseConnectionStringTemplate = "Server=127.0.0.1;Port=3306;Uid=etao;Pwd=etao123;Database={0};Charset=utf8"
             }
         */
    class Program
    {
        static void Main(string[] args)
        {

            if(args == null || args.Length == 0)
            {
                System.Console.WriteLine("no json config");
            }

            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, args[0]);

            string configString = File.ReadAllText(file);

            var config = JsonConvert.DeserializeObject<DatabaseTemplate>(configString);
            var m = new DateShareExtensionManager(config);
            m.Run();

            System.Console.WriteLine("success !");

        }
    }
}
