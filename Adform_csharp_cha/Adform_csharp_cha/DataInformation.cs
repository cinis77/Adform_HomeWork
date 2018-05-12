using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adform_csharp_cha
{
    class DataInformation
    {
        HttpClient Client { get; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; }
        public int DaysInterval { get; set; }
        string[] TypeOfMetrics { get; }
        List<DataSetFormat> DataSet { get; }


        public async Task FormDataToList()
        {
            DateTime dateForCycle = StartDate.AddDays(DaysInterval);
            while (dateForCycle <= EndDate)
            {
                FilterDate filterDate = new FilterDate(StartDate, dateForCycle);
                var jsonStringCreator = new JsonStringCreator(filter: new FilterType(filterDate), dimension: new string[] { "date" }, metrics: TypeOfMetrics);
                string output = jsonStringCreator.CreateJsonString(jsonStringCreator);
                HttpContent contentPost = new StringContent(output, Encoding.UTF8, "application/json");
                var respons = await Client.PostAsync("https://api.adform.com/v1/reportingstats/publisher/reportdata", contentPost);
                var responseString = await respons.Content.ReadAsStringAsync();
                DataFromResponse dataFromResponse = JsonConvert.DeserializeObject<DataFromResponse>(responseString);
                Console.WriteLine("Deserializing JSON and parsing");
                foreach (var item in dataFromResponse.reportData.rows)
                {
                    DataSetFormat tempElement = new DataSetFormat();
                    tempElement.FormDataFromString(item);
                    DataSet.Add(tempElement);
                }
                dateForCycle = dateForCycle.AddDays(1);
                StartDate = dateForCycle;
                dateForCycle = dateForCycle.AddDays(DaysInterval);
                Console.WriteLine("Data is being configured");
            }
        }

        public List<DataSetFormat> GetDataSet()
        {
            if (DataSet == null)
                throw new ArgumentException("List of data is not inicialized");

            return DataSet;
        }


        public DataInformation(HttpClient Client, DateTime StartDate, DateTime EndDate, int DaysInterval, string[] TypeOfMetrics)
        {
            this.Client = Client;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.DaysInterval = DaysInterval;
            this.TypeOfMetrics = TypeOfMetrics;
            DataSet = new List<DataSetFormat>();
        } 


    }
}
