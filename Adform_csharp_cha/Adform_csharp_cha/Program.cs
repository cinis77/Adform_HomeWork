using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;


namespace Adform_csharp_cha
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                FindAnomalyBetweenBids();
                FindImpressionsCountWeekly();
                Console.WriteLine("Waiting for data....");

                while (true)
                {

                }

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(JsonException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unknown exception");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Source);
            }


        }

               static async void FindAnomalyBetweenBids()
        {
            ClientInformation clientInformation = new ClientInformation("https://id.adform.com/sts/.well-known/openid-configuration", "https://api.adform.com/scope/eapi", "sellside.apiteam@tests.adform.com", "xPDUpHFZHuobERbKVjVxPujndfyg4C6KLDItwLwK");
            await clientInformation.FormHttpClientData();
            DataInformation bidInfo = new DataInformation(clientInformation.GetHttpClientData(), new DateTime(year: DateTime.Today.Year, month: 1, day: 1), DateTime.Today, 7, new string[] { "bidRequests" });
            await bidInfo.FormDataToList();
            List<DataSetFormat> fullDataInformation = bidInfo.GetDataSet();
            DateTime yesterdayDay = fullDataInformation.First().DateOfData;
            int yesterdayBid = fullDataInformation.First().DataAmount;
            foreach (var item in fullDataInformation)
            {
                if ((item.DataAmount / 3) >= yesterdayBid || item.DataAmount <= (yesterdayBid/3) )
                {
                    //Anomaly
                    Console.WriteLine("Anomaly found: Yesterday was {0} {1} Today is {2} {3}",yesterdayDay, yesterdayBid, item.DateOfData, item.DataAmount);
                    yesterdayBid = item.DataAmount;
                    yesterdayDay = item.DateOfData;

                }
                else
                {
                    yesterdayBid = item.DataAmount;
                    yesterdayDay = item.DateOfData;
                }
            }
        }


        static async void FindImpressionsCountWeekly()
        {
            ClientInformation clientInformation = new ClientInformation("https://id.adform.com/sts/.well-known/openid-configuration", "https://api.adform.com/scope/eapi", "sellside.apiteam@tests.adform.com", "xPDUpHFZHuobERbKVjVxPujndfyg4C6KLDItwLwK");
            await clientInformation.FormHttpClientData();
            DataInformation impressionInfo = new DataInformation(clientInformation.GetHttpClientData(), new DateTime(year: DateTime.Today.Year, month: 1, day: 1), DateTime.Today, 7, new string[] { "impressions" });
            await impressionInfo.FormDataToList();
            int i = 0;
            int weekCount = 1;
            int sumOfImpressions = 0;
            foreach (var item in impressionInfo.GetDataSet())
            {
                if (i == 6)
                {
                    Console.WriteLine("Week {0}, Impressions {1}", weekCount, sumOfImpressions);
                    sumOfImpressions = 0;
                    weekCount++;
                    i = 0;
                }
                else
                {
                    sumOfImpressions += item.DataAmount;
                    i++;
                }
            }
        }

    }
}


       

    

