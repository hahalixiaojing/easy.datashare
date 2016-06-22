using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.DataShare.DateShareExtension
{
    public class DateShareExtensionManager
    {
        private readonly DatabaseTemplate template;
        public DateShareExtensionManager(DatabaseTemplate template)
        {
            this.template = template;
        }

        public void Run()
        {
            var executor = new DatabaseExtensionExecutor();
            executor.Run(template);
        }
    }
}
