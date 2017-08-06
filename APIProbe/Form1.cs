using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace APIProbe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
           // timer1.Enabled = true;
            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://api.nicehash.com/api");
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://api.nicehash.com/api?method=stats.provider&addr=12zhBMHy2oRbSTHC1R7KuoC7nEZyd12Uny");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            using (Stream stream = resp.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        textBox1.Text += line;
                    }
                }
            }
            //resp.
            resp.Close();
            //           
            Account account = JsonConvert.DeserializeObject<Account>(textBox1.Text);
            dataGridView1.DataSource = account.result.Stats;
            dataGridView2.DataSource = account.result.Payments;
       }
        public class Stat
        {
            //{"balance":"0.00000488","rejected_speed":"0.00000000","algo":5,"accepted_speed":"0.00000000"}
            public double balance { get; set; }
            public double rejected_speed { get; set; }
            public int algo { get; set; }
            public double accepted_speed { get; set; }
        }
        public class Payment
        {
            //{"amount":"0.01048837","fee":"0.00043702","TXID":"1de56de98a775575e575c8bc114b7ea46048e119bbca0f011f47017f131d65b4","time":"2017-07-25 09:59:09"}
            public double amount { get; set; }
            public double fee { get; set; }
            public string TXID { get; set; }
            public DateTime time { get; set; }
        }
        public class Result
        {
            public IList<Stat> Stats { get; set; }
            public IList<Payment> Payments { get; set; } 
            public string addr { get; set; } 
        }
        public class Account
        {
            public Result result { get; set; }
            //public string addr { get; set; }
            public string method { get; set; }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1_Click(sender,e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://api.nicehash.com/api?method=stats.provider.ex&addr=12zhBMHy2oRbSTHC1R7KuoC7nEZyd12Uny");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            using (Stream stream = resp.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        textBox1.Text += line;
                    }
                }
            }
            //resp.
            resp.Close();
            //           
            //StatEx StatEx = JsonConvert.DeserializeObject<StatEx>(textBox1.Text);
            JObject o = JObject.Parse(textBox1.Text);
            MessageBox.Show((string)o["result"]["addr"]);
            //MessageBox.Show((string)o["result"]["payments"][0]["amount"]);

            dataGridView1.DataSource = o["result"]["current"];//StatEx.result.current;

            dataGridView2.DataSource = o["result"]["past"];//StatEx.result.payments;
            chart1.Series[0].Points.Clear();
            MessageBox.Show(o["result"]["past"][0]["data"][0].ToString());
            chart1.Series[0].Points.AddXY((float)o["result"]["past"][0]["data"][0][0], (float)o["result"]["past"][0]["data"][0][2]);
            //o["result"]["past"][0]["data"]
            JArray arr = (JArray)o["result"]["past"][0]["data"];
            foreach (JArray j in arr)
            {
                chart1.Series[0].Points.AddXY((float)j[0], (float)j[2]);
            }
            chart1.Series.Add("Second");
            JArray barr = (JArray)o["result"]["past"][1]["data"];
            foreach (JArray j in barr)
            {
                chart1.Series[0].Points.AddXY((float)j[0], (float)j[2]);
            }
            //chart1.Series[0].Points.AddXY()
            // (Speed)StatEx.result.current[0].data[0]
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string des = @"{'profitability':'0.00000054','data':[{},'0.00001008'],'name':'Keccak','suffix':'MH','algo':5}";
            //string des = @"{'data':[{'a':'3', 'ro':'3'},'0.00001008'],'name':'Keccak','suffix':'MH','algo':5}";
            Speed rr = JsonConvert.DeserializeObject<Speed>(des);
            
            Speed[] ss = new Speed[2];
            ss[0] = new Speed { a = 3 };
            ss[1] = new Speed { ro = 4};
            object[] obj = new object[2];
            Current cor = new Current { profitability = 0.4445, name = "Kubun",suffix="GH", algo =33, data = ss[0]};
            obj[0] = new Speed { a= 3, ro =34};
            obj[1] = "456";
            string oo = JsonConvert.SerializeObject(cor);
            JObject kk = JObject.Parse(oo);
            
           // var definition = new { Name = "" };
        }
        public class rec
        {
            public float p { get; set; }
            public Dat dat { get; set; }
            public string name { get; set; }
            public string suffix { get; set; }
            public int algo { get; set; } 
        }
        public class Dat
        {
            public IDictionary<string, float> ro { get; set; }
            public float value { get; set; }
        }
    }
}
