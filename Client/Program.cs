using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static int PORT_NO = 501;
        static string SERVER_IP = "14.225.205.166";
        static void Main(string[] args)
        {
            Console.Write("Hello, Please enter Ip: ");
            SERVER_IP = Console.ReadLine();

            Console.Write("Please enter port number: ");
            PORT_NO = int.Parse(Console.ReadLine());

            //Uses a remote endpoint to establish a socket connection.
            TcpClient tcpClient = new TcpClient();
            IPAddress ipAddress = Dns.GetHostEntry(SERVER_IP).AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, PORT_NO);

            tcpClient.Connect(ipEndPoint);

            NetworkStream nwStream = tcpClient.GetStream();

            while (true)
            {
                //---data to send to the server---
                string textToSend = DateTime.Now.ToString();
                textToSend = $"{Console.ReadLine()} : {textToSend} ";
                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

                //---send the text---
                Console.WriteLine("Sending : " + textToSend);
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                //---read back the text---
                byte[] bytesToRead = new byte[tcpClient.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytesToRead, 0, tcpClient.ReceiveBufferSize);
                Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));

            }

            Console.ReadLine();
            tcpClient.Close();
        }
    }
}