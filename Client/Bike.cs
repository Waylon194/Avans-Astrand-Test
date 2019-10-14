using Avans.TI.BLE;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    //Made by: Bart & Jasper on 11-sep-19 0x19 Bike connection
    public class Bike
    {
        #region Variables
        string bikeName;
        private BLE bleBike;
        private BLE bleHeart;
        int previousTotalDistance = 0;
        int totalDistance = 0;
        #endregion

        public Bike(string bikeName)
        {
            this.bikeName = bikeName;
        }

        public async Task ConnectAsync()
        {
            int errorCode = 0;
            bleBike = new BLE();
            bleHeart = new BLE();
            // We need some time to list available devices
            Thread.Sleep(1000);
            // List available devices
            List<String> bleBikeList = bleBike.ListDevices();
            Console.WriteLine("Devices found: ");

            foreach (var name in bleBikeList)
            {
                Console.WriteLine($"Device: {name}");
            }

            // Connecting
            errorCode = errorCode = await bleBike.OpenDevice(bikeName);
            var services = bleBike.GetServices;

            foreach (var service in services)
            {
                Console.WriteLine($"Service: {service}");
            }

            // Set service
            errorCode = await bleBike.SetService("6e40fec1-b5a3-f393-e0a9-e50e24dcca9e");
            // Subscribe
            bleBike.SubscriptionValueChanged += BleBike_SubscriptionValueChanged;
            errorCode = await bleBike.SubscribeToCharacteristic("6e40fec2-b5a3-f393-e0a9-e50e24dcca9e");

            // Heart rate
            errorCode = await bleHeart.OpenDevice("Decathlon Dual HR");
            await bleHeart.SetService("HeartRate");

            bleHeart.SubscriptionValueChanged += BleBike_SubscriptionValueChanged;
            await bleHeart.SubscribeToCharacteristic("HeartRateMeasurement");
            Console.Read();
        }

        public void BleBike_SubscriptionValueChanged(object sender, BLESubscriptionValueChangedEventArgs e)
        {
            if (!Checksum(e.Data))
            {
                return;
            }

            //Speed
            if (e.Data[4] == 0x10)
            {
                int totalDistance = e.Data[7];
                int SpeedLSB = e.Data[8];
                int SpeedMSB = e.Data[9];
                calculateTotalDistance(totalDistance);
                calculatedSpeed(SpeedLSB, SpeedMSB);
            }

            //Heartrate
            if (e.Data[0] == 0x16)
            {
                int bpm = e.Data[1];
            }

            //Bike data: updates per sec, rounds per minute, power in watt
            if (e.Data[4] == 0x19)
            {
                int amountOfUpdates = e.Data[5];
                int rpm = e.Data[6];
                int accumulatedPowerLSB = e.Data[7];
                int accumulatedPowerMSB = e.Data[8];
                calculatedPower(accumulatedPowerLSB, accumulatedPowerMSB);
            }    
        }

        //Checks if the byte array is valid
        private static bool Checksum(byte[] data)
        {
            int result = data[0];

            for (int i = 1; i < data.Length - 1; i++)
            {
                result ^= data[i];
            }

            return result == data[data.Length - 1];
        }

        async public void SetResistance(int amount)
        {
            if (amount >= 1 && amount <= 200)
            {
                byte[] message = new byte[13];

                //msg info
                message[0] = 0xA4;                     //Sync byte
                message[1] = 0x09;                     //Length of message is 8 bytes content + 1 channel byte
                message[2] = 0x4E;                     //Msg id 
                message[3] = 0x05;                     //Channel id
                //content
                message[4] = 0x30;                     //We want to change the resistance (0x30)

                for (int i = 5; i < 11; i++)
                {
                    message[i] = 0xFF;
                }

                message[11] = Convert.ToByte(amount);  //Value of the resistance, 1 == 0.5%

                //Checksum
                byte checksum = 0;
                for (int i = 1; i < 12; i++)
                {
                    checksum ^= message[i];
                }

                message[12] = checksum;

                await bleBike.WriteCharacteristic("6e40fec3-b5a3-f393-e0a9-e50e24dcca9e", message);
            }
        }

        public int calculateTotalDistance(int distance)
        {
            if (distance < previousTotalDistance)
            {
                previousTotalDistance = distance;
                totalDistance += distance;
            }
            else
            {
                totalDistance += (distance - previousTotalDistance);
                previousTotalDistance = distance;
            }
            return totalDistance;
        }

        public double calculatedSpeed(int LSB, int MSB)
        {
            int combined = (MSB << 8) | LSB;
            return (double)combined / 1000.0;
        }

        public double calculatedPower(int LSB, int MSB)
        {
            int combined = (MSB << 8) | LSB;
            return (double)combined / 1000.0;
        }
    }
}