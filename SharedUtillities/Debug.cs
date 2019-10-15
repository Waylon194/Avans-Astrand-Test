using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUtillities
{
    public class Debug
    {
        private static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ClientData\Debug.txt";

        public static void Log(string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
