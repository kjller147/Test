using ClosedXML.Excel;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Test
{
    public class Program
    {
        private static HttpClientHandler handler = new HttpClientHandler();
        private static HttpClient client = new HttpClient(handler, false);
        private static RestClient restClient = new RestClient(new Uri("http://aspnetmonsters.com"));
        [STAThread]
        public static async Task Main(string[] args)
        {
            int? testNum = 5;
            int k = 0;
            int previous = 0;
            while (k != 10)
            {
                if (previous != k)
                {
                    Console.WriteLine(previous);
                    previous = k;
                }
                k++;
            }

            Console.WriteLine(testNum.HasValue ? testNum : null);
            var ip = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties();

            var ips = ip.GetActiveTcpConnections().Where(tcp => tcp.LocalEndPoint.Port == 56844
                 || tcp.RemoteEndPoint.Port == 60983 || tcp.LocalEndPoint.Address.ToString() == "192.168.1.231").ToList();
            
            foreach (var tcp in ips) // alternative: ip.GetActiveTcpListeners()
            {                
                Console.WriteLine(
                String.Format(
                "{0}:{3} : {1}:{4} : {2}",
                tcp.LocalEndPoint.Address,
                tcp.RemoteEndPoint.Address, tcp.State.ToString(), tcp.LocalEndPoint.Port, tcp.RemoteEndPoint.Port));                
            }

            Console.WriteLine(testNum ?? 0);
            if (testNum == 0)
                Console.WriteLine(testNum);
            string where = string.Empty;
            for (int i = 0; i < 43; i++)
            {
                where += "SCADD M" + i + ",";
            }
            where = where.TrimEnd(',');
            Console.WriteLine(where);
            //bool isTest = Convert.ToBoolean("0");
            //ServicePointManager.FindServicePoint(new Uri("http://aspnetmonsters.com"))
            //    .ConnectionLeaseTimeout = (int)TimeSpan.FromSeconds(5).TotalMilliseconds;
            //TestWebRequest();
            //for (int i = 0; i < 10; i++)
            //{
            //    Task.Factory.StartNew(() => TestRestSharp());
            //}
            //TestRestSharp();
            TestFactory();
            //client = new HttpClient(handler, false);
            client.BaseAddress = new Uri("http://aspnetmonsters.com");
            //client.DefaultRequestHeaders.ConnectionClose = true;

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine("Create task " + i);
            //    var t = Task.Factory.StartNew(() => TestTask());
            //    Console.WriteLine("Finish task " + 1);
            //    Thread.Sleep(500);
            //}

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff"));
            //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/do_POST");

            //    request.Headers.Add("TestName" + i, "value" + i);

            //    var resp = client.SendAsync(request).Result;

            //    string headers = resp.Headers.ToString();
            //    Console.WriteLine(headers);
            //    resp.Dispose();
            //    //Thread.Sleep(3000);
            //}

            //Console.WriteLine("Using....");
            //for (int i = 0; i < 10; i++)
            //{
            //    using (HttpClient client1 = new HttpClient())
            //    {
            //        Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff"));
            //        client1.BaseAddress = new Uri("http://aspnetmonsters.com");
            //        HttpRequestMessage request1 = new HttpRequestMessage(HttpMethod.Post, "/do_POST");

            //        request1.Headers.Add("TestName", "value");

            //        var resp = client1.SendAsync(request1).GetAwaiter().GetResult();

            //        string headers = resp.Headers.ToString();
            //        Console.WriteLine(headers);
            //    }
            //}
            Console.ReadLine();
        }

        private static void TestTask()
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff"));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/do_POST");

            request.Headers.Add("TestName", "value");

            var resp = client.SendAsync(request).GetAwaiter().GetResult();

            string headers = resp.Headers.ToString();
            Console.WriteLine(headers);
            
        }
        private static async void TestFactory()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient("testHttpPost",
                c => { c.BaseAddress = new Uri("http://aspnetmonsters.com");
                });
            IHttpClientFactory factory = services.BuildServiceProvider().GetRequiredService<IHttpClientFactory>();

            var client2 = factory.CreateClient("testHttpPost");
            
            for (int i = 0; i < 100; i++)
            {
                Task.Run(() =>
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "");

                    request.Headers.Add("TestName" + i, "value" + i);

                    var resp = client2.SendAsync(request).GetAwaiter().GetResult();

                    string headers = resp.Headers.ToString();
                    Console.WriteLine(headers);
                });                   
                
            } 
        }

        private static void TestRestSharp()
        {
            var request = new RestRequest("", Method.POST, DataFormat.Json);

            while (true)
            {
                for (int i = 0; i < 10; i++)
                {
                    request.AddHeader("testHeader" + i, "headerValue" + i);
                    
                    IRestResponse response = restClient.Post(request);
                    var content = response.Content;
                    Console.WriteLine(string.Join(",", response.Headers) + "\t" + response.StatusCode);                    
                }                
            }
        }
        
        private static void TestWebRequest()
        {
            var request = WebRequest.Create("http://localhost:8001//do_POST");
            request.Method = "POST";

            for (int i = 0; i < 10; i++)
            {
                request.Headers.Add("testHeader" + i, "headerValue" + i);

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Console.WriteLine(response.StatusDescription);

                response.Close();
            }
        }
        private static void TestMethod(Test test)
        {
            Console.WriteLine(test.Id);
        }

        public static void SocketServerRun()
        {
            
            // Establish the local endpoint  
            // for the socket. Dns.GetHostName 
            // returns the name of the host  
            // running the application. 
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[3];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

            // Creation TCP/IP Socket using  
            // Socket Class Costructor 
            Socket listener = new Socket(ipAddr.AddressFamily,
                            SocketType.Stream, ProtocolType.Tcp);

            try
            {

                // Using Bind() method we associate a 
                // network address to the Server Socket 
                // All client that will connect to this  
                // Server Socket must know this network 
                // Address 
                listener.Bind(localEndPoint);

                // Using Listen() method we create  
                // the Client list that will want 
                // to connect to Server 
                listener.Listen(10);

                while (true)
                {

                    Console.WriteLine("Waiting connection ... ");

                    // Suspend while waiting for 
                    // incoming connection Using  
                    // Accept() method the server  
                    // will accept connection of client 
                    Socket clientSocket = listener.Accept();

                    // Data buffer 
                    byte[] bytes = new Byte[1024];
                    string data = null;

                    while (true)
                    {

                        int numByte = clientSocket.Receive(bytes);

                        data += Encoding.ASCII.GetString(bytes,
                                                    0, numByte);

                        if (data.IndexOf("<EOF>") > -1)
                            break;
                    }

                    Console.WriteLine("Text received -> {0} ", data);
                    byte[] message = Encoding.ASCII.GetBytes("Test Server");

                    // Send a message to Client  
                    // using Send() method 
                    clientSocket.Send(message);

                    // Close client Socket using the 
                    // Close() method. After closing, 
                    // we can use the closed Socket  
                    // for a new Client Connection 
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }
        [JsonObject(MemberSerialization.OptIn)]
        public class Test
        {
            [JsonProperty]
            public int Id { get; set; }
            public int? SSNumber { get; set; }

        }

        public class InsideTest
        {
            public int Id { get; set; }

            public int Quantity { get; set; } = 6;
        }

        public class TestInh : Test
        {
            public string Name { get; set; }
        }
    }
}
