
namespace Easy.DataShare.DateShareExtension
{
    public class DatabaseTemplate
    {
        public string ConnectionDatabaseString { get; set; }
        public string DatabaseConnectionStringTemplate { get; set; }
        public string DatabaseNamePrefix { get; set; }
        public string CreateDatabaseSql { get; set; }
        public string CreateTableSql { get; set; }
        public int MonthStep { get; set; } = 12;
        public string DatabaseDrive { get; set; }

    }
}
