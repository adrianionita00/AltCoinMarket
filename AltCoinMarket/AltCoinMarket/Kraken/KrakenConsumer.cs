using Jayrock.Json;
using Jayrock.Json.Conversion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AltCoinMarketConsumerLibrary
{
    public class KrakenConsumer : IDisposable
    {
        string _url;
        int _version;
        //string _key;
        //string _secret;

        public KrakenConsumer()
        {
            _url = ConfigurationManager.AppSettings["KrakenBaseAddress"];
            _version = int.Parse(ConfigurationManager.AppSettings["KrakenApiVersion"]);
            //_key = ConfigurationManager.AppSettings["KrakenKey"];
            //_secret = ConfigurationManager.AppSettings["KrakenSecret"];
        }

        public JsonObject GetTicker(List<string> pairs)
        {
            if (pairs == null)
            {
                return null;
            }
            if (pairs.Count() == 0)
            {
                return null;
            }

            StringBuilder pairString = new StringBuilder("pair=");
            foreach (var item in pairs)
            {
                pairString.Append(item + ",");
            }
            pairString.Length--; //disregard trailing comma

            return QueryPublic("Ticker", pairString.ToString()) as JsonObject;
        }

        public JsonObject GetRecentTrades(string pair, long since)
        {
            string reqs = string.Format("pair={0}", pair);


            reqs += string.Format("&since={0}", since.ToString());


            return QueryPublic("Trades", reqs) as JsonObject;
        }

        public JsonObject GetOrderBook(string pair, int? count = null)
        {
            string reqs = string.Format("pair={0}", pair);

            if (count.HasValue)
            {

                reqs += string.Format("&count={0}", count.Value.ToString());
            }

            return QueryPublic("Depth", reqs) as JsonObject;
        }
        private JsonObject QueryPublic(string a_sMethod, string props = null)
        {
            string address = string.Format("{0}/{1}/public/{2}", _url, _version, a_sMethod);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(address);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";


            if (props != null)
            {

                using (var writer = new StreamWriter(webRequest.GetRequestStream()))
                {
                    writer.Write(props);
                }
            }

            //Make the request
            try
            {
                ////Wait for RateGate
                //_rateGate.WaitToProceed();

                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (Stream str = webResponse.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(str))
                        {
                            return (JsonObject)JsonConvert.Import(sr);
                        }
                    }
                }
            }
            catch (WebException wex)
            {
                using (HttpWebResponse response = (HttpWebResponse)wex.Response)
                {
                    using (Stream str = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(str))
                        {
                            if (response.StatusCode != HttpStatusCode.InternalServerError)
                            {
                                throw;
                            }
                            return (JsonObject)JsonConvert.Import(sr);
                        }
                    }
                }
            }
        }
        public void Dispose()
        {            
            GC.SuppressFinalize(this);
        }
    }
}
