using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SharedUtillities
{
    public class AsyncConnection
    {
        private const string ip = "localhost";
        private const int port = 6666;
        private const int bufferSize = 4;

        private NetworkStream stream;
        private byte[] buffer;

        /*  The dokter and client application can recieve messages
         *  by subscribing to the event
         */
        public event EventHandler<string> NewMessage;

        //Make a connection to the server
        public void Connect()
        {
            buffer = new byte[bufferSize];

            TcpClient client = new TcpClient();
            client.Connect(ip, port);
            stream = client.GetStream();
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
        }

        //Get an Async result
        private void OnRead(IAsyncResult result)
        {
            int receivedBytes = this.stream.EndRead(result);
            int packetLength = BitConverter.ToInt32(buffer, 0);
            byte[] totalBuffer = new byte[packetLength];
            int readPos = 0;
            while (readPos < packetLength)
            {
                receivedBytes = stream.Read(totalBuffer, readPos, packetLength - readPos);
                readPos += receivedBytes;
            }

            string message = Encoding.UTF8.GetString(totalBuffer);

            //Fire the event
            NewMessage(this, message);

            this.stream.BeginRead(this.buffer, 0, buffer.Length, OnRead, null);
        }

        //Send data to the server async
        public void Write(JObject data)
        {
            //The data to send
            byte[] serialisedData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            //The length of the message
            byte[] length = BitConverter.GetBytes(serialisedData.Length);
            byte[] toSend = new byte[bufferSize + serialisedData.Length];

            //Write the length
            for (int i = 0; i < bufferSize; i++)
            {
                toSend[i] = length[i];
            }

            //Write the data
            for (int i = 0; i < serialisedData.Length; i++)
            {
                toSend[i + bufferSize] = serialisedData[i];
            }

            stream.WriteAsync(toSend, 0, toSend.Length);
            stream.Flush();
        }
    }
}
