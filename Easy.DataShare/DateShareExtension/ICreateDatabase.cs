using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.DataShare.DateShareExtension
{
    public interface ICreateDatabase
    {
        void Create(string connectionString, string databaseName);
    }
}
