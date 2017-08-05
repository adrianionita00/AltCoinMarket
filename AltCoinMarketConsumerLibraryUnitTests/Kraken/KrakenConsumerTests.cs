using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AltCoinMarketConsumerLibrary;

namespace AltCoinMarketConsumerLibraryUnitTests
{
    [TestClass]
    public class KrakenConsumerTests
    {
        public static KrakenConsumer consumer = new KrakenConsumer();

        [TestMethod]
        public void GetTickerTest()
        {
            var ticker = consumer.GetTicker(new List<string> { Constants.CurrencyPairs.BTCUSD });

            Assert.IsNotNull(ticker);
        }

        [TestMethod]
        public void GetRecentTradesTest()
        {
            long epoch = (long)(DateTime.UtcNow.AddHours(-1) - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;

            var trades = consumer.GetRecentTrades(Constants.CurrencyPairs.BTCUSD, epoch);

            Assert.IsNotNull(trades);
        }

        [TestMethod]
        public void GetOrderBookTest()
        {
            var depth = consumer.GetOrderBook(Constants.CurrencyPairs.BTCUSD, 1);

            Assert.IsNotNull(depth);
        }
    }
}
