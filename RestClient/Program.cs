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
 
            string url = "https://acadianasoftwaregroup.org/#/";
            var request = (HttpWebRequest)WebRequest.Create(url);
          
            request.Method = "get";
            var response = request.GetResponse();
            var content = response.GetResponseStream();
            var responseStream = response.GetResponseStream();
            string receiveContent= "";
            if (responseStream != null)
            {
                var reader = new StreamReader(responseStream);
                receiveContent = reader.ReadToEnd();
                reader.Close();              
            }

            //Console.WriteLine(receiveContent);
            url = "https://acadianasoftwaregroup.org/api/cms?o=10&s=0";
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "get";
            response = request.GetResponse();
            
            content = response.GetResponseStream();
            responseStream = response.GetResponseStream();
            receiveContent = "";
            if (responseStream != null)
            {
                var reader = new StreamReader(responseStream);
                receiveContent = reader.ReadToEnd();
                reader.Close();
            }
            dynamic articles = JsonConvert.DeserializeObject(receiveContent);
            foreach (var article in articles)
            {
                Console.WriteLine(article.title);
            }

            Console.ReadLine();
          
        }

    }
}
