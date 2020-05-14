using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Xml;

namespace ClientTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "http://localhost:55323/WebServiceTest.asmx/HelloWorld";
            string postStr = this.HttpPost(url, "");
            MessageBox.Show(postStr);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string url = "http://localhost:55323/WebServiceTest.asmx/HelloX";
            string postStr = this.HttpPost(url, "x=" + textBox1.Text);
            MessageBox.Show(postStr);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string url = "http://localhost:55323/WebServiceTest.asmx/HelloXY";
            string postStr = this.HttpPost(url, "x=" + textBox2.Text + "&y=" + textBox3.Text);
            MessageBox.Show(postStr);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<string> lst = new List<string>() { "Lily", "Lucy", "LiLei", "Andy" };
            string json = JsonConvert.SerializeObject(lst);
            string url = "http://localhost:55323/WebServiceTest.asmx/HelloJson";
            string postStr = this.HttpPost(url, "json=" + json);
            MessageBox.Show(postStr);
        }

        string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            CookieContainer cookie = new CookieContainer();

            request.CookieContainer = cookie;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            response.Cookies = cookie.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();

            //获取全部返回值XML字符串
            //StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            //string retString = myStreamReader.ReadToEnd();
            XmlTextReader Reader = new XmlTextReader(myResponseStream);
            Reader.MoveToContent();
            string retString = Reader.ReadInnerXml();//取出Content中的Json数据
            Reader.Close();
            myResponseStream.Close();
            return retString;
        }
    }
}
