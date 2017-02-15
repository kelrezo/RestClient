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
            Debug.WriteLine(receiveContent);
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(url);
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var get = client.GetStreamAsync("/");

            //var response = get.Result;
            //byte[] x = new byte[1117];
            //response.Read(x, 0,1117);
            //response.Headers.Add("Content-Type","text/html");
            // Debug.WriteLine(response.Content.Headers);
            //foreach (var data in response.Content.ReadAsHttpResponseMessageAsync().ToString())
            //{
            //    Debug.WriteLine(data);
            //} 
            //var dataObjects = response.Content.ReadAsAsync<IEnumerable<string>>().Result;
            //foreach (var d in dataObjects)
            //{
            //    Console.WriteLine("{0}", d.Name);
            //}
            //Debug.WriteLine(response.Content.);
            //var data = response.Content.ReadAsByteArrayAsync();

            // Debug.WriteLine(response.Content.);
        }

    }
}
