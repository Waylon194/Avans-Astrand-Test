#region Imports
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
#endregion

namespace Server
{
    class ClientConnection
    {
        #region Variables
        private static  Encoding encoding = Encoding.UTF8;
        private int bufferSize = 4;
        #endregion

        //Read a message from the client
        public string Read(NetworkStream stream)
        {
            try
            {
                //Read the first bufferSize bytes for the message length and convert it to an integer
                byte[] length = new byte[bufferSize];
                stream.Read(length, 0, bufferSize);
                int totalRead = 0;
                //Zet de length (Byte array) om naar een integer getal
                int messageLength = BitConverter.ToInt32(length, 0);
                //Maak een nieuwe buffer aan met de waarde van de message length
                byte[] buffer = new byte[messageLength];

                //Zolang het ontvangen bericht kleiner is dan de buffer lengte
                while (totalRead < messageLength)
                {
                    int read = stream.Read(buffer, totalRead, buffer.Length - totalRead);
                    totalRead += read;
                }

                return encoding.GetString(buffer, 0, totalRead);
            }
            catch (IOException)
            {
                //Throw the exeption to the method called point
                throw;
            }
        }

        //Send a message to one client
        public void Write(TcpClient client, JObject data)
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

            client.GetStream().WriteAsync(toSend, 0, toSend.Length);
            client.GetStream().Flush();
        }
    }
}
