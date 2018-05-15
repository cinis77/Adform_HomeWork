using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adform_csharp_cha
{
    public class FilterDate
    {
        
        public DateTime from { get; private set; }
        public DateTime to { get; private set; }

        public void SetFromValue(DateTime from)
        {
            this.from = from;
        }

        public void SetToValue (DateTime to)
        {
            this.to = to;
        }

        public FilterDate( DateTime from, DateTime to)
        {
            
            this.from = from;
            this.to = to;
        }
    }
}
