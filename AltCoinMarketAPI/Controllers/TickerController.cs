using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AltCoinMarketAPI.Models;
using AltCoinMarketConsumerLibrary;
using Newtonsoft.Json;

namespace AltCoinMarketAPI.Controllers
{
    public class TickerController : ApiController
    {
        private Ticker ticker;
        //Ticker[] tickers = new Ticker[]
        //{
        //new Ticker { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
        //new Ticker { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
        //new Ticker { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        //};

        //public IEnumerable<Ticker> GetAllTickers()
        //{
        //    return tickers;
        //}

        public IHttpActionResult GetTicker()
        {
            //var ticker = tickers.FirstOrDefault();
            //if (tickers == null)
            //{
            //    return NotFound();
            //}
            //return Ok(ticker);

            KrakenConsumer consumer = new KrakenConsumer();
            Jayrock.Json.JsonObject response = consumer.GetTicker(new List<string> { Constants.CurrencyPairs.BTCUSD });
            //ticker = (Ticker)Jayrock.Json.Conversion.JsonConvert.Import(typeof(Ticker), response.ToString());
            ticker = JsonConvert.DeserializeObject<Ticker>(response.ToString());

            if (ticker == null)
            {
                return NotFound();
            }
            return Ok(ticker);            
        }
    }
}
