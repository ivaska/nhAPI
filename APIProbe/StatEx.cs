using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIProbe
{
    public class StatEx
    {       
        public Result result { get; set; }
        public string method { get; set; }
    }
    public class Result
    {
       // public string addr { get; set; }
        public IList<Current> current { get; set; }
        public bool nh_wallet { get; set; }
        public IList<PastData> past { get; set; }
        public IList<Payment> payments { get; set; }
        public string addr { get; set; }
        //public string method { get; set; }
    }
    public class Profitability
    {

    }
    public class Current
    {
        public double profitability { get; set; }
        public Speed data { get; set; }
        //
        public string name { get; set; }
        public string suffix { get; set; }
        public int algo { get; set; }
        //public IList<Data> data { get; set; }
        
        //public double balance { get; set; }
    }
    public class CurrentItem
    {
        public IList<object> item { get; set; }
    }
    public class Speed
    {
        // a (accepted), rt (rejected target), rs (rejected stale),
        // rd (rejected duplicate) and ro (rejected other)
        public double a { get; set; }
        public double rs { get; set; }
        public double rd { get; set; }
        public double ro { get; set; }
    }
    public class Past
    {
  //      "past":[{
		//"algo":3,
		//"data":[

  //          [4863234, // timestamp; multiply with 300 to get UNIX timestamp
		//	{"a":"28.6"}, // speed object
		//	"0" // balance (unpaid)
		//	],[4863235,{"a":"27.4"},"0.00000345"],
		//	... // next entries with inc. timestamps
		//]},
        public int algo { get; set; }
        public IList<PastData> data { get; set; }
    }
    public class PastData
    {
        public Int32 timestamp { get; set; }
        public Speed s { get; set; }
        public double balance { get; set; }
    }
    public class Payment
    {
  //      "payments":[{
		//"amount":"0.00431400",
		//"fee":"0.00023000",
		//"TXID":"txidhere",
		//"time":1453538732, // UNIX timestamp
		//"type":0 // payment type (0 for standard NiceHash payment)
        public double amount { get; set; }
        public double fee { get; set; }
        public string TXID { get; set; }
        public Int32 time { get; set; }
        public int type { get; set; }
    }
}
