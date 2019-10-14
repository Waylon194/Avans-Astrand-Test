using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        private IPAddress ipAdress;
        private int port;
        private bool listening;

        public Server(string ipAdress, int port)
        {
            this.ipAdress = IPAddress.Parse(ipAdress);
            this.port = port;
        }

        //Start the server and listen for clients
        public void Start()
        {
            TcpListener listener = new TcpListener(ipAdress, port);
            listener.Start();

            Console.WriteLine("Server has started on IP: {0} and port: {1}", ipAdress, port);

            while (listening)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine($"Accepted client at {DateTime.Now}");

                Thread thread = new Thread(HandleClientThread);
                thread.Start(client);
            }
        }

        //Reads messages and sends them to the MessageHandler
        private void HandleClientThread(object obj)
        {
            TcpClient client = obj as TcpClient;
            bool running = true;
            NetworkStream stream = client.GetStream();
            ClientConnection connection = new ClientConnection();
            MessageHandler messageHandler = new MessageHandler();

            while (running)
            {
                string received;

                try
                {
                    received = connection.Read(stream);
                }
                catch (IOException)
                {
                    Console.WriteLine("SERVER: IOException");
                    break;
                }

                if(received != "")
                {
                    messageHandler.HandleMessage(received);
                }
            }

            client.Close();
            stream.Close();
            Console.WriteLine("Connection endend with a client");
        }
    }
}
