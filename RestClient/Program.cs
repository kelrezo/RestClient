using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace RestClient
{
    class Program
    {
        static void Main(string[] args)
        {
 
            string url1 = "https://acadianasoftwaregroup.org/api/cms?o=10&s=0";
            string url2 = "https://acadianasoftwaregroup.org/api/user/profiles/{0}";
            string url3 = "https://acadianasoftwaregroup.org/api/cal/event?s={0}&e={1}";
            string method = "get";
            //request = (HttpWebRequest)WebRequest.Create(url);
            //request.Method = "get";
            //response = request.GetResponse();


            //content = response.GetResponseStream();
            //responseStream = response.GetResponseStream();
            //if (responseStream != null)
            //{
            //    var reader = new StreamReader(responseStream);
            //    receiveContent = reader.ReadToEnd();
            //    reader.Close();
            //}
            //dynamic articles = JsonConvert.DeserializeObject(receiveContent);
            dynamic articles = request(url1, method);
            List<dynamic> writers = new List<dynamic>();
            foreach (var article in articles)
            {
                Console.WriteLine("Title: " + article.title);
                Console.WriteLine("Author: " + request(string.Format(url2, article.authorId), method).name);
                Console.WriteLine("----------------------------------------------------------------------");
            }
            Console.WriteLine("\nEvents\n");
            DateTime end = DateTime.UtcNow;
            DateTime start = end.AddMonths(-6);
            dynamic events = request(string.Format(url3, start.ToString("o"), end.ToString("o")), method);
            foreach (var meet in events)
            {
                Console.WriteLine(meet);
            }
            Console.ReadLine();
          
        }
        static dynamic request(string url,string method)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            var response = request.GetResponse();
            var content = response.GetResponseStream();
            var responseStream = response.GetResponseStream();
            string receiveContent = "";
            if (responseStream != null)
            {
                var reader = new StreamReader(responseStream);
                receiveContent = reader.ReadToEnd();
                reader.Close();
            }
            //dynamic data = JsonConvert.DeserializeObject(receiveContent);
            return JsonConvert.DeserializeObject(receiveContent);
        }

    }
}
