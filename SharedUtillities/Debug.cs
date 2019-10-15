#region Imports
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace SharedUtillities
{
    public class Debug
    {
        #region Variables
        private static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ClientData\Debug.txt";
        #endregion

        public static void Log(string content)
        {
            File.WriteAllText(path, content);
        }
    }
}