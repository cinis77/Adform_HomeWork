using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adform_csharp_cha
{

    
    class JsonStringCreator
    {
        public IFilterTypeData filter { get; }
        public string[] dimensions { get; }
        public string[] metrics { get; }

        /// <summary>
        /// Create serialized Json string from object
        /// </summary>
        /// <param name="objektas"></param>
        /// <returns></returns>
        public string CreateJsonString(object objektas)
        {
            string output = JsonConvert.SerializeObject(objektas, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" });
            Console.WriteLine("creating Json string");
            if (output == null || output == "{}")
                throw new JsonException(message: "Failed to create JSON string");

            return output;
        }


     
        public JsonStringCreator(IFilterTypeData filter, string[] dimension, string[] metrics)
        {
            this.filter = filter;
            this.dimensions = dimension;
            this.metrics = metrics;
            
        }
    }
}
