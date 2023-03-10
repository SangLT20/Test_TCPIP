
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Server
{
    class Program
    {
        static int PORT_NO = 5002;
        static string SERVER_IP = "14.225.205.166";

        static void Main(string[] args)
        {
            Console.Write("Hello, Please enter Ip: ");
            SERVER_IP = Console.ReadLine();
            Console.Write("Hello, Please enter port: ");
            PORT_NO = int.Parse(Console.ReadLine());
            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            TcpListener listener = new TcpListener(localAdd, PORT_NO);
            Console.WriteLine("Listening...");
            listener.Start();
            //---incoming client connected---
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Connted");
            while (true)
            {

                //---get the incoming data through a network stream---
                NetworkStream nwStream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];

                //---read incoming stream---
                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                //---convert the data received into a string---
                string dataReceived = ASCIIEncoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received : " + dataReceived);

                //---write back the text to the client---
                Console.WriteLine("Sending back : " + dataReceived);
                nwStream.Write(buffer, 0, bytesRead);

            }

            client.Close();
            listener.Stop();
            Console.ReadLine();
        }
    }
}