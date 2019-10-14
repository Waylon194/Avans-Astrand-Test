#region Imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Client
{
    class Controller
    {
        private readonly static string[] bikesArray = {
                "Tacx Flux 00438", // 0
                "Tacx Flux 00457", // 1
                "Tacx Flux 00472", // 2
                "Tacx Flux 01140", // 3
                "Tacx Flux 24517" // 4
            };

        private static Bike bike;

        public static void CreateBike()
        {
            bike = new Bike(bikesArray[0]);
        }

        public static void rpmGuard(int rpm)
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
        public static void bpmGuard(int bpm)
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
    }
}