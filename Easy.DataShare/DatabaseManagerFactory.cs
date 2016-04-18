using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.DataShare
{
    public class DatabaseManagerFactory
    {
        private readonly IDictionary<string, DatabaseManager> managers = new Dictionary<string, DatabaseManager>();

        internal void Register(string name,DatabaseManager manager)
        {
            this.managers.Add(name, manager);
        }

        public DatabaseManager Get(string name)
        {
            return this.managers[name];
        }
    }
}
