using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adform_csharp_cha
{
   
    class ReportData
    {
        public string[] columnHeaders { get; set; }
        public string[][] rows { get; set; }
    }

    class DataFromResponse
    {
        public ReportData reportData { get; set; }
        public string correlationCode { get; set; }
    }
}
