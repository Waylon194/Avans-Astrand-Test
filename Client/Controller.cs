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
        #region Variables
        private Bike bike;
        private AsyncConnection connection;
        private JArray data;
        private string firstName = "First name not entered!";
        private string lastName = "Last name not entered!";
        private int age = 0;
        public int maxBPMForAge = 210;
        public bool runningTest { get; set; }
        public string FirstName { get => firstName; }
        public string LastName { get => lastName; }
        public int Age { get => age; set => age = value; }
        #endregion

        public void Start()
        {
            if (age <= 15)
            {
                maxBPMForAge = 210;
            }
            else if (age > 15 && age <= 25)
            {
                maxBPMForAge = 210;
            }
            else if (age > 25 && age <= 35)
            {
                maxBPMForAge = 200;
            }
            else if (age > 35 && age <= 40)
            {
                maxBPMForAge = 190;
            }
            else if (age > 40 && age <= 45)
            {
                maxBPMForAge = 180;
            }
            else if (age > 45 && age <= 50)
            {
                maxBPMForAge = 170;
            }
            else if (age > 50 && age <= 55)
            {
                maxBPMForAge = 160;
            }
            else //55+
            {
                maxBPMForAge = 150;
            }

            bike = new Bike(this);
            connection = new AsyncConnection();
            connection.Connect();
            data = new JArray();
            runningTest = false;
        }

        public void SetName(string firstName, string lastName)
        {
            if (firstName.Trim() != "")
            {
                this.firstName = firstName;
            }

            if (lastName.Trim() != "")
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
                //Debug
                Console.WriteLine($"RPM: {rpm}");
            }
        }

        //TODO: if(x >= bpm && x + 10 >= bpm) do stuff
        public void bpmGuard(int bpm)
        {
            //Debug
            Console.WriteLine($"____BPM {bpm}");

            if (runningTest)
            {
                if (bpm == maxBPMForAge)
                {
                    bike.AdaptResistance(-200);
                }

                if (bpm < 125)
                {
                    bike.AdaptResistance(10);
                }
                else if (bpm > 135)
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