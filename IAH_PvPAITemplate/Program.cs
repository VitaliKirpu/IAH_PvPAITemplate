using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IAH_PvPAITemplate.Class;
using IAH_PvPAITemplate.Class.AI;

namespace IAH_PvPAITemplate
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            ApiConnector apiConnector = new ApiConnector("http://127.0.0.1:6800");

            using (HttpClient httpClient = apiConnector.GetHttpClient())
            {
                Debugger.WriteLog("Starting PvP (HelloWorldAI) Template...");

                MatchInstance instance = new MatchInstance(httpClient);
                instance.AssignAI<HelloWorldAI>();
                instance.AssignRequests();
                
                await instance.LoopMatchInstance();
            }

            Debugger.WriteLog("Press any Key to Close...");
            Console.ReadKey();
        }
    }
}