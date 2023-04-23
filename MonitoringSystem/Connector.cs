using System.Net.Sockets;
using System.Text;

namespace MonitoringSystem
{
    public static class Connector
    {
        public static void ConnectTCPIP(String server, int port, String message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                //Int32 port = 502;

                // Prefer a using declaration to ensure the instance is Disposed later.
                using TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = Utility.StringToByteArray(message);

                // Get a client stream for reading and writing.
                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the server response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                //responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                //Console.WriteLine("Received: {0}", responseData);

                responseData = Utility.ByteArrayToString(data);
                Console.WriteLine("Received: {0}", responseData);
                int valueA1 = data == null ? 
                    0 
                    : int.Parse(responseData.Substring(18, 4), System.Globalization.NumberStyles.HexNumber);
                Console.WriteLine("ValueA1: {0}", valueA1);

                // Explicit close is not necessary since TcpClient.Dispose() will be
                // called automatically.
                // stream.Close();
                // client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        public static async Task ConnctHTTP(string server, int port, string message)
        {
            try
            {
                Console.WriteLine("** SERVICE STARTED **");

                var httpClient = new HttpClient();

                var todosJson = await httpClient.GetStringAsync($"https://jsonplaceholder.typicode.com/todos/{1}");
            }
            finally
            {
                Console.WriteLine("** SERVICE STOPPED **");
            }
        }
    }
}