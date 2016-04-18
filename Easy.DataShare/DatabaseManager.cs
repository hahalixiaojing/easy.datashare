using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.DataShare.DateShare;

namespace Easy.DataShare
{
    public  class DatabaseManager
    {
        private readonly IDateTimeDatabaseExecute dataTimeDatabaseExecute;

        public DatabaseManager(IDateTimeDatabaseExecute dataTimeDatabaseExecute)
        {
            this.dataTimeDatabaseExecute = dataTimeDatabaseExecute;
        }
        public IDateTimeDatabaseExecute Executor
        {
            get
            {
                return dataTimeDatabaseExecute;
            }
        }
    }
}
