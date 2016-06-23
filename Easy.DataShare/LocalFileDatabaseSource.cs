using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using Easy.DataShare.DateShare;

namespace Easy.DataShare
{
    public class LocalFileDatabaseSource : IDatabaseSource
    {
        private string filePath;
        private IList<DefaultDateTimeSplitDatabase> database;
        public LocalFileDatabaseSource(string filePath)
        {
            this.filePath = filePath;
            DatabaseLoader loader = new DatabaseLoader();
            database = loader.Load(new XPathDocument(this.filePath).CreateNavigator());
        }

        public IList<DefaultDateTimeSplitDatabase> GetDataSource()
        {
            return database;
        }
    }
}
