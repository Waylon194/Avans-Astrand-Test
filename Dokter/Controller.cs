using Newtonsoft.Json.Linq;
using SharedUtillities;
using System;

namespace Dokter
{
    public class Controller
    {
        private HomeScreen homeScreen;
        private AsyncConnection connection;
        private JObject data;

        public Controller(HomeScreen homeScreen)
        {
            this.homeScreen = homeScreen;
        }

        //Connect async to the server
        public void Connect()
        {
            connection = new AsyncConnection();
            connection.Connect();
            connection.NewMessage += OnMessage;
        }

        //Load a Json object from the received message
        private void OnMessage(object sender, string message)
        {
            data = JObject.Parse(message);
        }

        //Retrieve all the client data
        public void DataRequest()
        {
            var data = new
            {
                type = "dataRequest"
            };
            connection.Write(JObject.FromObject(data));
        }
    }
}