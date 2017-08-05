using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AltCoinMarketConsumerLibrary;

namespace AltCoinMarketAPI.Models
{
    public class Ticker
    {
        public List<object> error { get; set; }
        public string currencyPair { get; set; }

        public List<string> a { get; set; }
        public List<string> b { get; set; }
        public List<string> c { get; set; }
        public List<string> v { get; set; }
        public List<string> p { get; set; }
        public List<int> t { get; set; }
        public List<string> l { get; set; }
        public List<string> h { get; set; }
        public string o { get; set; }
    }   

    //<pair_name> = pair name
    //a = ask array(<price>, <whole lot volume>, <lot volume>),
    //b = bid array(<price>, <whole lot volume>, <lot volume>),
    //c = last trade closed array(<price>, <lot volume>),
    //v = volume array(<today>, <last 24 hours>),
    //p = volume weighted average price array(<today>, <last 24 hours>),
    //t = number of trades array(<today>, <last 24 hours>),
    //l = low array(<today>, <last 24 hours>),
    //h = high array(<today>, <last 24 hours>),
    //o = today's opening price
}