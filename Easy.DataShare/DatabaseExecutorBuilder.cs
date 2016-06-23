using System.IO;
using System.Xml.XPath;
using Easy.DataShare.DateShare;

namespace Easy.DataShare
{
    public class DatabaseExecutorBuilder
    {
        public IDateTimeDatabaseExecutor BuildDataTimeExecutor(IDatabaseSource databaseSource)
        {
            var selector = new DateTimeSplitDatabaseSelector(databaseSource);
            var executor = new DefaultDateTimeDatabaseExecute(selector);
            return executor;

        }
    }
}
