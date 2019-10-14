using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class MessageHandler
    {
        private Dictionary<string, Action<JObject, TcpClient>> callbacks;

        public MessageHandler()
        {
            callbacks = new Dictionary<string, Action<JObject, TcpClient>>();
            callbacks["command"] = OnCommand;
            callbacks["data"] = OnData;
            callbacks["message"] = OnMessage;
        }

        private void OnCommand(JObject obj, TcpClient client)
        {
            throw new NotImplementedException();
        }

        private void OnData(JObject obj, TcpClient client)
        {
            throw new NotImplementedException();
        }

        private void OnMessage(JObject obj, TcpClient client)
        {
            throw new NotImplementedException();
        }
        
        //Execute the given function from the callbacks
        private void Invoke(string function, JObject data, TcpClient client)
        {
            Console.WriteLine("MessageHandler.Invoke: invoking " + function);
            callbacks[function.Trim().ToLower()].Invoke(data, client);
        }

        //Parse and handle a given message
        public void HandleMessage(string message, TcpClient client)
        {
            try
            {
                JObject data = JObject.Parse(message);
                Invoke(data["type"].ToObject<string>(), data, client);
            }
            catch (JsonReaderException)
            {
                Console.WriteLine("MessageHandler.HandleMessage: JsonReaderException");
            }
        }
    }
}
