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
            string url4 = "https://acadianasoftwaregroup.org/api/bb/{0}";
            string url5 = "https://acadianasoftwaregroup.org/api/bb/{0}/{1}";

            Console.WriteLine("Articles\n");
            dynamic articles = request(url1);
            List<dynamic> writers = new List<dynamic>();
            foreach (var article in articles)
            {
                Console.WriteLine("Title: " + article.title);
                Console.WriteLine("Author: " + request(string.Format(url2, article.authorId)).name);
                Console.WriteLine("----------------------------------------------------------------------");
            }

            Console.WriteLine("\nEvents\n");
            DateTime end = DateTime.UtcNow;
            DateTime start = end.AddMonths(-6);
            dynamic events = request(string.Format(url3, start.ToString("o"), end.ToString("o")));
            foreach (var meet in events)
            {
                Console.WriteLine(meet);
            }

            Console.WriteLine("\nPost in first Thread\n");
   
            dynamic baord = request(string.Format(url4,""));
            dynamic boardId;
            dynamic authorId;
            //each board
            foreach (var board in baord)

            {

                boardId = board.boardId;
                authorId = board.authorId;
                Console.WriteLine(boardId + "           " + authorId);
                dynamic threads = request(string.Format(url4, boardId));
                //each thread in board
                foreach (var thread in threads)
                {
                    dynamic posts = request(string.Format(url5, boardId, thread.threadId));
                    Console.WriteLine(posts);
                    //each post in thread
                    foreach (var post in posts)
                    {
                        Console.WriteLine(post);
                        Console.WriteLine(request(string.Format(url2,post.authorId)).name);
                    }
                    break;
                 }
                break;
            }

            Console.ReadLine();
          
        }
        static dynamic request(string url)
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
            return JsonConvert.DeserializeObject(receiveContent);
        }

    }
}
