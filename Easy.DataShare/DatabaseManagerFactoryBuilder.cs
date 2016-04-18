using System.IO;
using System.Xml.XPath;
using Easy.DataShare.DateShare;

namespace Easy.DataShare
{
    public class DatabaseManagerFactoryBuilder
    {
        public DatabaseManagerFactory Build(Stream stream)
        {
            DatabaseLoader loader = new DatabaseLoader();
            var groups = loader.Load(new XPathDocument(stream).CreateNavigator());

            var factory = new DatabaseManagerFactory();
            foreach (var group in groups)
            {
                var selector = new DateTimeSplitDatabaseSelector();
                foreach (var database in group.Item2)
                {
                    selector.Register(database);
                }
                var executor = new DefaultDateTimeDatabaseExecute(selector);
                var manager = new DatabaseManager(executor);

                factory.Register(group.Item1, manager);
            }
            return factory;
        }
    }
}
