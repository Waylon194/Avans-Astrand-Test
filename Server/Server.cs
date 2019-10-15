#region Imports
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
#endregion

namespace Server
{
    class Server
    {
        #region Variables
        private IPAddress ipAdress;
        private int port;
        private bool serverRunning = true;
        #endregion

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
            
            while (serverRunning)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Server.Start: Client connected");
                Thread thread = new Thread(HandleClientThread);
                thread.Start(client);
            }

            Console.ReadLine();
        }

        //Reads messages and sends them to the MessageHandler
        private void HandleClientThread(object obj)
        {
            TcpClient client = obj as TcpClient;
            bool running = true;
            NetworkStream stream = client.GetStream();
            ClientConnection connection = new ClientConnection();
            MessageHandler messageHandler = new MessageHandler(connection);

            while (running && serverRunning)
            {
                string received;

                try
                {
                    received = connection.Read(stream);
                }
                catch (IOException)
                {
                    Console.WriteLine("Server.HandleClientThread: IOException");
                    break;
                }

                if(received != "")
                {
                    messageHandler.HandleMessage(received, client);
                }
            }

            client.Close();
            stream.Close();
            Console.WriteLine("Server.HandleClientThread: Client connection ended");
        }
    }
}