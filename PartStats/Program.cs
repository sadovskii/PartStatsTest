using PartStats.BLL;
using System;

namespace PartStats
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var worker = new Worker();
            worker.Router(args[0], args[1]);
        }
    }
}
