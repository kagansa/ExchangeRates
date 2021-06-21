using Newtonsoft.Json;
using System;
using System.Net;

namespace Business
{
    public class ExchangeRateManager
    {
        private string APIUrl = "https://openexchangerates.org/api/latest.json?app_id=a3b87a13462d44a1902da59d65477e77";
        private bool firstRun = true;
        private dynamic ratesList;

        private LogManager logManager = new LogManager();

        private void GetLatestAPI()
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("Content-Type:application/json");
                    client.Headers.Add("Accept:application/json");

                    var result = client.DownloadString(APIUrl);

                    dynamic stuff = JsonConvert.DeserializeObject(result);
                    string rateList = stuff.rates.ToString();

                    dynamic ratelistJson = JsonConvert.DeserializeObject(rateList);

                    ratesList = ratelistJson;
                }
            }
            catch (Exception e)
            {
                logManager.logger("Kur Değerleri Aktarılamadı... " + e.Message + " " + DateTime.Now);
            }
        }

        public decimal Get(string from, string to, decimal value)
        {
            if (((DateTime.Now.Hour == 10 || DateTime.Now.Hour == 15) && DateTime.Now.Minute == 00) || firstRun)
            {
                GetLatestAPI();
                firstRun = false;
            }

            return ratesList[to] * value;
        }
    }
}