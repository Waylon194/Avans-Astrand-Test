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
        private string firstName = "First name not entered!";
        private string lastName = "Last name not entered!";

        public bool runningTest { get; set; }
        public string FirstName { get => firstName; }
        public string LastName { get => lastName; }

        public void Start()
        {
            bike = new Bike(this);
            connection = new AsyncConnection();
            connection.Connect();
            data = new JArray();
            runningTest = false;
        }

        public void SetName(string firstName, string lastName)
        {
            if(firstName.Trim() != "")
            {
                this.firstName = firstName;
            }
            if(lastName.Trim() != "")
            {
                this.lastName = lastName;
            }
        }

        public void rpmGuard(int rpm)
        {
            if (runningTest)
            {
                //Sending messages to make the client cycle faster or slower
                if (rpm < 50)
                {
                    //Faster
                }
                else if (rpm > 60)
                {
                    //slower
                }
                Console.WriteLine($"RPM: {rpm}");
            }
        }

        //TODO: if(x >= bpm && x + 10 >= bpm) do stuff
        public void bpmGuard(int bpm)
        {
            Console.WriteLine($"____BPM {bpm}");

            if (runningTest)
            {
                ////Setting the resistance according to the bpm of the client
                //switch (bpm)
                //{
                //    case 100:
                //        bike.SetResistance(1);
                //        break;
                //    case 110:
                //        bike.SetResistance(2);
                //        break;
                //    case 120:
                //        bike.SetResistance(3);
                //        break;
                //    case 125:
                //        bike.SetResistance(4);
                //        break;
                //    //No case for 130 bpm because that is the ideal bpm for this test
                //    case 135:
                //        bike.SetResistance(4);
                //        break;
                //    case 140:
                //        bike.SetResistance(3);
                //        break;
                //}

                if (bpm < 125)
                {
                    bike.AdaptResistance(10);
                } else if (bpm > 135)
                {
                    bike.AdaptResistance(-10);
                }
            }
        }

        public void DataUpdate()
        {
            if (runningTest)
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
                
                JObject jdata = JObject.FromObject(data);
                this.data.Add(jdata);
                //Print();
            }
        }

        private void Print()
        {
            Console.WriteLine($"First name {this.FirstName}, last name {this.LastName}, bikename {bike.bikeName}, total distance {bike.totalDistance}, speed {bike.speed}, bpm {bike.bpm}," +
                    $"amount of updates {bike.amountOfUpdates}, rpm {bike.rpm}, power {bike.power}, resistance {bike.resistance}");
        }

        public void SendTrainingData()
        {
            JObject jObject = new JObject();
            jObject.Add("type", "data");
            jObject.Add("firstName", firstName);
            jObject.Add("lastName", lastName);
            jObject.Add("date", DateTime.Now.ToString());
            
            var jSonArray = JsonConvert.SerializeObject(this.data);
            var jArray = JArray.Parse(jSonArray);
            jObject.Add("bikeData", jArray);

            connection.Write(jObject);
            Debug.Log(this.data.ToString());
            Debug.Log(jObject.ToString());
        }
    }
}