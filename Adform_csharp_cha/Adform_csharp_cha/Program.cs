using System;
using IdentityModel;
using IdentityModel.Client;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Text;
using System.Threading.Tasks;

namespace Adform_csharp_cha
{
    class DataTypes
    {
        public double dateOfData;
        public int BidCount;

    }
    class Program
    {


        public static int TimesAskForResponse;
        static void Main(string[] args)
        {
          
            kazkas();
            Console.WriteLine("Laukiu");

            while (true)
            {

            }


        }


        static async void kazkas()
        {
            var randukazka = new DiscoveryClient("https://id.adform.com/sts/.well-known/openid-configuration");
            var doc = await randukazka.GetAsync();

            var tokenEndpoint = doc.TokenEndpoint;
            var keys = doc.KeySet.Keys;
            string ad = doc.TokenEndpoint;
            //string address = "https://id.adform.com/sts/connect/token";
            string scope = "https://api.adform.com/scope/eapi";
            string clientID = "sellside.apiteam@tests.adform.com";
            string clientSecret = "xPDUpHFZHuobERbKVjVxPujndfyg4C6KLDItwLwK";
            var stilius = new AuthenticationStyle();
            stilius = AuthenticationStyle.PostValues;

            var client = new TokenClient(address: ad, clientId: clientID, clientSecret: clientSecret, style: stilius);
            var response = await client.RequestClientCredentialsAsync(scope);
            var token = response.AccessToken;
            HttpClient klient = new HttpClient();
            klient.SetToken("Bearer", token);
            DateTime data = new DateTime(year:2018, month: 01, day: 01);
            TimesAskForResponse = 0;
            while(data < DateTime.Today)
            {
                
                DateTime temp = data.AddDays(6);
                GetStreamOfData(klient, data, temp);
                temp = temp.AddDays(1);
                data = temp;
                if(data.DayOfYear > DateTime.Today.DayOfYear)
                {
                    data = DateTime.Today;
                }
                
            }
            
           
                
            

        }
        
        static async void GetStreamOfData(HttpClient klient, DateTime start, DateTime end)
        {
            string zodis = "{\"filter\": {\"date\": {\"from\": \""+start.Year+"-"+start.Month+"-"+start.Day+"\",\"to\": \""+end.Year+"-"+end.Month+"-"+end.Day+ "\" } },\"dimensions\" : [ \"date\" ],   \"metrics\": [\"bidRequests\" ]  }";
            HttpContent contentPost = new StringContent(zodis, Encoding.UTF8, "application/json");
            var respons = await klient.PostAsync("https://api.adform.com/v1/reportingstats/publisher/reportdata", contentPost);
            var responseString = await respons.Content.ReadAsStringAsync();
            
            List<string> stringsplittingva = responseString.Split(',').ToList();
            stringsplittingva.Remove(stringsplittingva.First());
            stringsplittingva.Remove(stringsplittingva.First());
            stringsplittingva.Remove(stringsplittingva.Last());
            List<string> duomenys = new List<string>();
            foreach (var item in stringsplittingva)
            {
                string laikina = new String(item.Where(Char.IsDigit).ToArray());
                duomenys.Add(laikina);
            }

            duomenuApdorojimas(duomenys);
            
        }

        static List<DataTypes> kk = new List<DataTypes>();

        static void duomenuApdorojimas(List<string> duomenys)
        {
            
            double forKeeping = 0;
            int i = 1;
            foreach(var element in duomenys)
            {
                if((i%2)==0)
                {
                    i = 1;
                    int laikinaVieta = 0;
                    int.TryParse(element, out laikinaVieta);
                    DataTypes tt = new DataTypes();
                    tt.dateOfData = forKeeping;
                    tt.BidCount = laikinaVieta;
                    kk.Add(tt);
                }
                else
                {
                    i++;
                    double.TryParse(element, out forKeeping);
                    forKeeping /= 1000000;
                }
            }
            int variableForWorkAround = 0;
            if((DateTime.Today.DayOfYear % 7)!= 0)
            {
                variableForWorkAround = (DateTime.Today.DayOfYear / 7) + 1;
            }
            else
            {
                variableForWorkAround = (DateTime.Today.DayOfYear / 7);
            }


            TimesAskForResponse++;
            if (TimesAskForResponse == variableForWorkAround)
            {
                kk = kk.OrderBy(x => x.dateOfData).ToList();
                //DataFormatingForOutPut();
                FindAnomaly();
            }
        }

        static void FindAnomaly()
        {
            double yesterdayDay = kk.First().dateOfData;
            int yesterdayBid = kk.First().BidCount;
            foreach (var item in kk)
            {
                if ((item.BidCount / 3) > yesterdayBid || item.BidCount < (yesterdayBid/3) )
                {
                    //Anomaly
                    Console.WriteLine("Anomaly found: Yesterday was {0} {1} Today is {2} {3}",yesterdayDay, yesterdayBid, item.dateOfData, item.BidCount);
                    yesterdayBid = item.BidCount;
                    yesterdayDay = item.dateOfData;

                }
                else
                {
                    yesterdayBid = item.BidCount;
                    yesterdayDay = item.dateOfData;
                }
            }
        }

        #region
        /*
        static void DataFormatingForOutPut()
        {
            int i = 0;
            int weekCount = 1;
            int sumOfImpressions = 0;
            foreach (var item in kk)
            {
                if(i == 6)
                {
                    Console.WriteLine("Week {0}, Impressions {1}", weekCount, sumOfImpressions);
                    sumOfImpressions = 0;
                    weekCount++;
                    i = 0;
                }
                else
                {
                    sumOfImpressions += item.impressionsCounts;
                    i++;
                }
            }
        }
        */
        #endregion
    }
}


       

    

