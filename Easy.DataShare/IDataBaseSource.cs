using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.DataShare.DateShare;

namespace Easy.DataShare
{
    public interface IDatabaseSource
    {
        IList<DefaultDateTimeSplitDatabase> GetDataSource();
    }
}
