#region Imports
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedUtillities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Client
{
    public class Controller
    {
        private Bike bike;
        private AsyncConnection connection;
        private JArray data;

        public Controller()
        {
            bike = new Bike(this);
            connection = new AsyncConnection();
            connection.Connect();
            data = new JArray();
        }

        public void rpmGuard(int rpm)
        {
            //Sending messages to make the client cycle faster or slower
            if (rpm < 50)
            {
                //faster
            }
            else if (rpm > 60)
            {
                //slower
            }
        }

        //TODO: if(x >= bpm && x + 10 >= bpm) do stuff
        public void bpmGuard(int bpm)
        {
            //Setting the resistance according to the bpm of the client
            switch (bpm)
            {
                case 100:
                    bike.SetResistance(1);
                    break;
                case 110:
                    bike.SetResistance(2);
                    break;
                case 120:
                    bike.SetResistance(3);
                    break;
                case 125:
                    bike.SetResistance(4);
                    break;
                //No case for 130 bpm because that is the ideal bpm for this test
                case 135:
                    bike.SetResistance(4);
                    break;
                case 140:
                    bike.SetResistance(3);
                    break;
            }
        }

        public void DataUpdate()
        {
            var data = new
            {
                bikeName = bike.bikeName,
                totalDistance = bike.totalDistance,
                speed = bike.speed,
                bpm = bike.bpm,
                amountOfUpdates = bike.amountOfUpdates,
                rpm = bike.rpm,
                power = bike.power,
                resistance = bike.resistance
            };
            Console.WriteLine($"Bikename {bike.bikeName}, total distance {bike.totalDistance}, speed {bike.speed}, bpm {bike.bpm}," +
                $"amount of updates {bike.amountOfUpdates}, rpm {bike.rpm}, power {bike.power}, resistance {bike.resistance}");
            JObject jdata = JObject.FromObject(data);
            this.data.Add(jdata);
        }

        public void SendTrainingData()
        {
            JObject jObject = new JObject();
            jObject.Add("type", "data");
            jObject.Add("date", DateTime.Now.ToString());
            jObject.Add("bikeData", this.data);
            connection.Write(jObject);
        }
    }
}