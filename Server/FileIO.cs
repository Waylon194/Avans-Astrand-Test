using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class FileIOClass
    {
        private static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ClientData\log.txt";
        private static readonly object fileIOlock = new object();
        
        private JArray data;

        #region Singleton
        /*  To make the file thread safe, we want to aquire locks.
         *  But if you have multiple instances of the same class with the same lock,
         *  it will ignore the lock
         */

        private static FileIOClass instance;

        public static FileIOClass GetInstance()
        {
            lock (fileIOlock)
            {
                if (instance == null)
                {
                    instance = new FileIOClass();
                }
                return instance;
            }
        }

        private FileIOClass() { InitData(); }
        #endregion


        public JArray Data { get => data; }

        //Read data or create a JArray
        private void InitData()
        {
            lock (fileIOlock)
            {
                try
                {
                    data = JArray.Parse(Read());
                }
                catch (JsonReaderException)
                {
                    File.WriteAllText(path, JsonConvert.SerializeObject(new JArray()));
                    data = JArray.Parse(Read());
                    Console.WriteLine("FileIO.InitData: Created an empty array");
                }
            }
        }

        //Read data from the specified file
        private string Read()
        {
            string data;

            lock (fileIOlock)
            {
                data = File.ReadAllText(path);
            }
            Console.WriteLine("FileIO.Read: Read all data from " + path);
            return data;
        }

        //Write data to the specified file
        public void Write(JObject obj)
        {
            lock (fileIOlock)
            {
                data = JArray.Parse(Read());
                data.Add(obj);
                File.WriteAllText(path, data.ToString());
            }
            Console.WriteLine("FileIO.Write: wrote data to " + path);
        }
    }
}
