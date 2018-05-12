using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adform_csharp_cha
{
    class FilterType
    {
        
        public FilterDate date { get; }

        public FilterType(FilterDate date)
        {
            this.date = date;
        }

    }
}
