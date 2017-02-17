using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestClient
{
    class Program
    {
        static void Main(string[] args)
        {
 
            string url1 = "https://acadianasoftwaregroup.org/api/cms?o=10&s=0";
            string url2 = "https://acadianasoftwaregroup.org/api/user/profiles/{0}";
            string url3 = "https://acadianasoftwaregroup.org/api/cal/event?s={0}&e={1}";
            string url4 = "https://acadianasoftwaregroup.org/api/bb/{0}";
            string url5 = "https://acadianasoftwaregroup.org/api/bb/{0}/{1}";

            Console.WriteLine("Articles\n");
            var articles = request(url1);
            foreach (var article in articles)
            {
                Console.WriteLine("Title: " + (string)article["title"]);
                Console.WriteLine("Author: " + request2(string.Format(url2, article["authorId"]))["name"]);
                Console.WriteLine("----------------------------------------------------------------------");
            }

            Console.WriteLine("\nEvents\n");
            DateTime end = DateTime.UtcNow;
            DateTime start = end.AddMonths(-6);
            var events = request(string.Format(url3, start.ToString("o"), end.ToString("o")));
            foreach (var meet in events)
            {
                Console.WriteLine(meet);
            }

            Console.WriteLine("\nPost in first Thread\n");
            var boards = request(string.Format(url4, ""));
            //each board
            foreach (var board in boards)
            {
                Console.WriteLine(board);
                var threads = request(string.Format(url4, board["boardId"]));
                Console.WriteLine(threads);
                //each thread in board
                foreach (var thread in threads)
                {
                    var posts = request(string.Format(url5, thread["boardId"], thread["threadId"]));
                    //each post in thread
                    foreach (var post in posts)
                    {
                        Console.WriteLine(post);
                        Console.WriteLine(request2(string.Format(url2, post["authorID"])));
                    }
                    break;
                }
                break;
            }
            Console.ReadLine();         
        }
        
        static JArray request(string url)
        {
            return (JArray)JsonConvert.DeserializeObject(getRequest(url));
        }
        static JObject request2(string url)
        {
            return (JObject)JsonConvert.DeserializeObject(getRequest(url));
        }
        static string getRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "get";
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
            return receiveContent;
        }

    }
}
