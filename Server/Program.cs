using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        enum SendType
        {
            ENDSTREAMING,
            STREAMING,
            AUDIO_DATA,
            REQUEST_ERROR,
            CURRENT_POSSITION,
            PAUSE,
            RESUME,
            GET_AUDIO_DATA,
            GET_CURRENT_POSSITION,
        }

        class Message
        {
            public SendType TypeSend;
            public string From;
            public string To;
            public byte[] Data;
            public string DataExtension;
        }

        public static List<TcpClient> tcpClients;
        public static Dictionary<string, byte[]> keyValuePairs;

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        static void Main(string[] args)
        {
            int port = 9009;
            keyValuePairs = new Dictionary<string, byte[]>();
            tcpClients = new List<TcpClient>();
            TcpListener tcpListener = new TcpListener(IPAddress.Parse(GetLocalIPAddress()), port);
            tcpListener.Start();
            Console.WriteLine("Server listen on " + GetLocalIPAddress() + ":" + port.ToString());
            while(true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                Console.WriteLine(client.Client.RemoteEndPoint.ToString() + " connected");
                tcpClients.Add(client);             
                Thread t = new Thread(ProcessClient);
                t.Start(client);
            }
        }
        
        public static void SendAll(byte[] buff,int lenght)
        {
            foreach(TcpClient client in tcpClients)
            {
                Console.WriteLine("Send message to " + client.Client.RemoteEndPoint.ToString());
                try
                {
                    client.Client.Send(buff, 0, lenght, SocketFlags.None);
                }
                catch
                {
                    Console.WriteLine("Send message to " + client.Client.RemoteEndPoint.ToString()+" err");
                    //tcpClients.Remove(client);
                }
               
            }
        }

        public static bool SendToClient(string endpointString, byte[] buff, int lenght)
        {
            var client = tcpClients.SingleOrDefault(n => n.Client.RemoteEndPoint.ToString() == endpointString);

            try
            {
                
                client.Client.Send(buff, 0, lenght, SocketFlags.None);
                return true;
            }
            catch
            {
                if(client != null)
                {
                    //tcpClients.Remove(client);
                }
                return false;
            }
            



        }

        public static void  ProcessClient(object sk)
        {
            TcpClient client = (TcpClient)sk;
            try
            {
                NetworkStream ns = client.GetStream();

                while (true)
                {
                    byte[] buff = new byte[11000000];
                    int readed = ns.Read(buff, 0, buff.Length);
                    
                    string s = Encoding.ASCII.GetString(buff,0,readed);
                    var datasend = JsonConvert.DeserializeObject<Message>(s);

                    switch (datasend.TypeSend)
                    {
                        case SendType.ENDSTREAMING:
                            {
                                Console.WriteLine("get " + datasend.TypeSend.ToString() + " from " + datasend.From + " to " + datasend.To);
                                SendAll(buff, readed);
                                break;
                            }
                        case SendType.GET_CURRENT_POSSITION:
                        case SendType.GET_AUDIO_DATA:
                        case SendType.CURRENT_POSSITION:
                        case SendType.AUDIO_DATA:
                            {
                                Console.WriteLine("get " + datasend.TypeSend.ToString() + " from " + datasend.From + " to " + datasend.To);
                                var ok =  SendToClient(datasend.To, buff, readed);
                                if (!ok)
                                {
                                    datasend.TypeSend = SendType.REQUEST_ERROR;
                                    s = JsonConvert.SerializeObject(datasend);
                                    buff = Encoding.ASCII.GetBytes(s);
                                    SendToClient(datasend.From, buff, s.Length);
                                }
                                break;
                            }
                     
                        case SendType.STREAMING:
                        case SendType.PAUSE:
                        case SendType.RESUME:
                            {
                               Console.WriteLine("get " + datasend.TypeSend.ToString() + " from " + datasend.From);
                               SendAll(buff, readed);
                                break;
                            }
                    }
                }
            }
            catch
            {
                return;
            }
            
           
            
        }
    }
}
