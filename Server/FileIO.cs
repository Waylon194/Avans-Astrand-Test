using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class FileIO
    {
        private Dictionary<string, StreamWriter> writers;
        private List<string> fileData;

        private readonly static string[] CLIENTDATA_FOLDER_SEGEMENTS = new Uri(Directory.GetCurrentDirectory()).Segments;
        private readonly static string CLIENTDATA_FOLDER_NOUSAGE = string.Concat(Util.SubArray(CLIENTDATA_FOLDER_SEGEMENTS, 0, CLIENTDATA_FOLDER_SEGEMENTS.Length - 2)) + "ClientData/";
        private readonly static string CLIENT_DATA_FOLDER = CLIENTDATA_FOLDER_NOUSAGE.Remove(0, 1);

        public List<string> FileData { get => fileData; set => fileData = value; }

        public FileIO()
        {
            InitWriters();
            ReadAllFiles();
        }

        //Make streamwriters for all the files
        private void InitWriters()
        {
            string[] files = Directory.GetFiles(CLIENT_DATA_FOLDER);

            foreach (string file in files)
            {
                writers.Add(file, new StreamWriter(file));
            }
        }

        //Read the total file and put the data in the list
        private void ReadAllFiles()
        {
            fileData = new List<string>();

            string[] files = Directory.GetFiles(CLIENT_DATA_FOLDER);

            foreach (string file in files)
            {
                File.ReadAllText(file);
            }
        }
        
        //Save the data to a file
        public void SaveData(JObject obj)
        {
            if (!File.Exists(CLIENT_DATA_FOLDER + obj["bikeSerialNumber"].ToObject<string>() + DateTime.Now.ToString("dd-MM-yyyy") + ".txt"))
            {
                string toBeAddedPath = CLIENT_DATA_FOLDER + obj["bikeSerialNumber"].ToObject<string>().ToLower().Trim() + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
                using (File.Create(toBeAddedPath)) { }
                writers.Add(toBeAddedPath, new StreamWriter(toBeAddedPath));
                writers[toBeAddedPath].WriteLine(obj);
            }
            else
            {
                writers[CLIENT_DATA_FOLDER + obj["bikeSerialNumber"].ToObject<string>().ToLower().Trim() + DateTime.Now.ToString("dd-MM-yyyy") + ".txt"].WriteLine(obj);
            }
        }
    }

    static class Util
    {
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
