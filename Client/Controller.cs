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
        private static Bike client1;

        public static void CreateBike()
        {
            client1 = new Bike("Bike 1");
        }

        public static void rpmGuard(int rpm)
        {
            //Sending messages to make the client cycle faster or slower
            if (rpm < 50)
            {
                //faster
                client1.
            }
            else if (rpm > 60)
            {
                //slower
            }
        }

        public static void bpmGuard(int bpm)
        {
            //Setting the resistance according to the bpm of the client
            switch (bpm)
            {
                case 100:
                    client1.setResistance(1);
                    break;
                case 110:
                    client1.setResistance(2);
                    break;
                case 120:
                    client1.setResistance(3);
                    break;
                case 125:
                    client1.setResistance(4);
                    break;
                //No case for 130 bpm because that is the ideal bpm for this test
                case 135:
                    client1.setResistance(4);
                    break;
                case 140:
                    client1.setResistance(3);
                    break;
            }
        }
    }
}