using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.DataShare.DateShare
{
    public interface IDateTimeSplitDatabase
    {
        int Index
        {
            get;
        }

        DateTime Start
        {
            get;
        }

        DateTime End
        {
            get;
        }
        string Database { get; }

        bool IsSelected(DateTime start, DateTime end);
        bool IsSelected(DateTime datetime);
    }
}
