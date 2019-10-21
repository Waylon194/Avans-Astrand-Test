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
        private int ticks = 0;
        private bool steadyState = false;
        private int steadyStateTick;

        private double vo2max = 0;
        private int maxBPMTest = 0;
        private int restingBPM = 0;
        private int lastResistanceTick;

        private int age = 15;
        private int weight = 70;
        private string gender;
        private int maxBPMForAge = 210;
        private double factor;

        private const int resStep = 3;

        public bool runningTest { get; set; }
        public string FirstName { get => firstName; }
        public string LastName { get => lastName; }
        public AstradTestClient AstradTestClient { get; set; }
        public int Age { get => age; set => age = value; }
        #endregion

        public void Start()
        {
            if (age <= 15)
            {
                maxBPMForAge = 210;
                factor = 1.1;
            }
            else if (age > 15 && age <= 25)
            {
                maxBPMForAge = 210;
                factor = 1;
            }
            else if (age > 25 && age <= 35)
            {
                maxBPMForAge = 200;
                factor = 0.87;
            }
            else if (age > 35 && age <= 40)
            {
                maxBPMForAge = 190;
                factor = 0.83;
            }
            else if (age > 40 && age <= 45)
            {
                maxBPMForAge = 180;
                factor = 0.78;
            }
            else if (age > 45 && age <= 50)
            {
                maxBPMForAge = 170;
                factor = 0.75;
            }
            else if (age > 50 && age <= 55)
            {
                maxBPMForAge = 160;
                factor = 0.71;
            }
            else if (age > 55 && age <= 60)
            {
                maxBPMForAge = 150;
                factor = 0.68;
            }
            else //60+
            {
                maxBPMForAge = 150;
                factor = 0.65;
            }

            bike = new Bike(this);
            bike.ConnectAsync();
            connection = new AsyncConnection();
            connection.Connect();
            data = new JArray();
            runningTest = false;
        }

        public void SetClientData(string firstName, string lastName, int age, int weight, string gender)
        {
            if (firstName.Trim() != "")
            {
                this.firstName = firstName;
            }

            if (lastName.Trim() != "")
            {
                this.lastName = lastName;
            }

            this.age = age;
            this.weight = weight;
            this.gender = gender;
        }

        //Set messages if the rpm is "wrong"
        public void rpmGuard(int rpm)
        {
            Console.WriteLine($"RPM: {rpm}");

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
            }
        }

        //Set the resistance
        public void bpmGuard(int bpm)
        {
            //Debug
            Console.WriteLine($"____BPM {bpm}");

            if (runningTest)
            {
                if (bpm == maxBPMForAge)
                {
                    bike.SetResistance(0);

                }

                //if (bpm < 125)
                //{
                //    bike.AdaptResistance(5);
                //}
                //else if (bpm > 135)
                //{
                //    bike.AdaptResistance(-5);
                //}
            }
        }

        //Add the bike data to the list and put it on the screen
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
                    resistance = bike.resistance,
                    ticks = ticks
                };
                
                JObject jdata = JObject.FromObject(data);
                this.data.Add(jdata);
                steadyState = CheckSteadyState();

                if (bike.bpm > maxBPMTest)
                {
                    maxBPMTest = bike.bpm;
                }

                vo2max = CalculateVO2max();
            }
            //Print();
        }

        private bool CheckSteadyState()
        {
            if (bike.bpm >= 125 && bike.bpm <= 135)
            {
                if (bike.rpm >= 50 && bike.rpm <= 60)
                {
                    if (steadyState == false)
                    {
                        steadyStateTick = ticks;
                    }
                    return true;
                }
            }
            return false;
        }

        private double CalculateVO2max()
        {
            return maxBPMTest / restingBPM * 15.3 / (weight * (ticks / 60))  * 100;
        }

        //Return the text hints
        private string GetTextMessage()
        {
            //Teveel bpm en te hoge versnelling (weinig toeren)
            if (bike.bpm > 135 && bike.rpm < 50)
            {
                return "Omlaag schakelen";
            }

            //Te lage hartslag en te lage versnelling (veel toeren)
            if (bike.bpm < 125 && bike.rpm > 60)
            {
                return "Omhoog schakelen";
            }

            //Te hoge hartslag en te hoge versnelling (veel toeren)
            if (bike.bpm > 135 && bike.rpm > 60)
            {
                return "Langzamer trappen";
            }

            //Te lage hartslag en te lage versnalling (weinig toeren)
            if (bike.bpm < 125 && bike.rpm < 50)
            {
                return "Harder trappen";
            }

            //Te hoge hartslag
            if (bike.bpm > 135 && bike.rpm >= 50 && bike.rpm <= 60)
            {
                //Weerstand omlaag of terug schakelen
                if (bike.resistance > 0 && !(lastResistanceTick == ticks))
                {
                    lastResistanceTick = ticks;
                    bike.AdaptResistance(-resStep);
                    return "Weerstand gaat omlaag";
                }
                else
                {
                    return "Omlaag schakelen";
                }
            }

            //Te lage hartslag
            if (bike.bpm < 125 && bike.rpm >= 50 && bike.rpm <= 60)
            {
                //Weerstand omhoog of op schakelen
                if (!(bike.resistance + resStep > 200) && !(lastResistanceTick == ticks))
                {
                    lastResistanceTick = ticks;
                    bike.AdaptResistance(resStep);
                    return "Weerstand gaat omhoog";
                }
                else
                {
                    bike.SetResistance(bike.resistance / 3);
                    return "Omhoog schakelen";
                }
            }

            //Teveel toeren
            if (bike.bpm >= 125 && bike.bpm <= 135 && bike.rpm > 60)
            {
                //Weerstand omhoog of terug schakelen
                if (!(bike.resistance + resStep > 200) && !(lastResistanceTick == ticks))
                {
                    lastResistanceTick = ticks;
                    bike.AdaptResistance(resStep);
                    return "Weerstand gaat omhoog";
                }
                else
                {
                    return "Omlaag schakelen";
                }
            }

            //Te weinig toeren
            if (bike.bpm >= 125 && bike.bpm <= 135 && bike.rpm < 50)
            {
                //Weerstand omlaag of terug schakelen
                if (bike.resistance > 0 && !(lastResistanceTick == ticks))
                {
                    lastResistanceTick = ticks;
                    bike.AdaptResistance(-resStep);
                    return "Weerstand gaat omlaag";
                }
                else
                {
                    return "Omhoog schakelen";
                }
            }
            return "Hou dit tempo aan";
        }

        //Return the remaing training time
        private string GetRemainingTime()
        {
            if (ticks < 120)
            {
                return $"Warmup: {120 - ticks} sec";
            }
            else if (ticks < 360)
            {
                return $"Test: {360 - ticks} sec";
            }
            else if (ticks < 420)
            {
                return $"Cooldown: {420 - ticks} sec";
            }
            return "Test klaar";
        }

        private void Print()
        {
            Console.WriteLine($"First name {this.FirstName}, last name {this.LastName}, bikename {bike.bikeName}, total distance {bike.totalDistance}, speed {bike.speed}, bpm {bike.bpm}," +
                    $"amount of updates {bike.amountOfUpdates}, rpm {bike.rpm}, power {bike.power}, resistance {bike.resistance}");
        }

        //Send the data to the server
        public void SendTrainingData()
        {
            JObject jObject = new JObject();
            jObject.Add("type", "data");
            jObject.Add("firstName", firstName);
            jObject.Add("lastName", lastName);
            jObject.Add("age", age);
            jObject.Add("weight", weight);
            jObject.Add("gender", gender);
            jObject.Add("date", DateTime.Now.ToString());
            jObject.Add("steadyState", steadyState);
            jObject.Add("vo2max", (vo2max * factor));

            if (steadyStateTick >= 239) // 2 minuten warmup + 2 minuten training
            {
                jObject.Add("avgBPM", GetBpmAverage());
            }
            
            var jSonArray = JsonConvert.SerializeObject(this.data);
            var jArray = JArray.Parse(jSonArray);
            jObject.Add("bikeData", jArray);

            connection.Write(jObject);
            Debug.Log(this.data.ToString());
            Debug.Log(jObject.ToString());
        }

        //Calculate the avarage BPM from the last two test minutes
        public int GetBpmAverage()
        {
            int dataAmount = 0;
            int total = 0;

            foreach (JObject jObject in this.data)
            {
                if (jObject["ticks"].ToObject<int>() > 239)
                {
                    dataAmount++;
                    total += jObject["bpm"].ToObject<int>();
                }
            }
            return total / dataAmount;
        }
        #region Ticks
        //What to do when ticked
        public void Tick()
        {
            Console.WriteLine("Tick " + ticks);
            ticks++;

            if (ticks == 120) //2min warmup, 120 sec
            {
                warmup();
                restingBPM = bike.bpm;
            }

            if (ticks == 360) //4min test, 360 sec
            {
                astradTest();
            }

            if (ticks == 420) //1min cooldown
            {
                cooldown();
            }

            if (AstradTestClient != null)
            {
                AstradTestClient.DataUpdate(bike.rpm, bike.bpm, bike.resistance / 2, GetTextMessage(), GetRemainingTime());
            }
        }

        private void warmup()
        {
            runningTest = true;
            Console.WriteLine("WarmUp done!");
        }

        private void astradTest()
        {
            runningTest = false;
            SendTrainingData();
            Console.WriteLine("AstradTest done!");
        }

        private void cooldown()
        {
            Console.WriteLine("WarmUp done!");
        }
        #endregion
    }
}