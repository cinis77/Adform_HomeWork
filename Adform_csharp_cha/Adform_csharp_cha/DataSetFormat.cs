using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adform_csharp_cha
{
    class DataSetFormat
    {
        public DateTime DateOfData { get; private set; }
        public int DataAmount { get; private set; }

        public void FormDataFromString(string[] dataElements)
        {
            try
            {
                DateOfData = DateTime.Parse(dataElements[0]);
                DataAmount = int.Parse(dataElements[1]);
            }
            catch (Exception)
            {

                throw new FormatException(message: "Error while formating");
            }
            
        }
    }
}
