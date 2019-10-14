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
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ClientData\log.txt";
        private JArray data;

        public JArray Data { get => data; set => data = value; }

        public FileIOClass()
        {
            InitData();
        }

        //Read or create a JArray
        private bool InitData()
        {
            try
            {
                data = JArray.Parse(TryRead());
                return true;
            }
            catch (JsonReaderException)
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(new JArray()));
                return InitData();
            }
        }

        //To prevent fileIO failures, it will be tried untill they can access the file.
        private string TryRead()
        {
            while (true)
            {
                try
                {
                    return File.ReadAllText(path);
                }
                catch (IOException)
                {
                    Console.WriteLine("FileIOClass.TryRead: IOException. Trying again in 1 second");
                    Thread.Sleep(1000);
                }
            }
        }

        //To prevent fileIO failures, it will be tried untill they can access the file.
        public void TryWrite(JObject obj)
        {
            data = JArray.Parse(TryRead());
            data.Add(obj);

            while (true)
            {
                try
                {
                    File.WriteAllText(path, data.ToString());
                    break;
                } catch (IOException)
                {
                    Console.WriteLine("FileIOClass.TryWrite: IOException. Trying again in 1 second");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
