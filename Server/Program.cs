#region Imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("localhost", 6666);
            server.Start();
        }
    }
}