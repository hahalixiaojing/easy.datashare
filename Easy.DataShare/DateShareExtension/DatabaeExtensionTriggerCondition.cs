using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.DataShare.DateShareExtension
{
    public class DatabaeExtensionTriggerCondition
    {
        public string TableName { get; set; }
        public int MaxRows { get; set; } = 50000000;
        public int BeforeDay { get; set; } = 3;
    }
}
