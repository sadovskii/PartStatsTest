﻿using PartStats.BLL;
using PartStats.BLL.Factories;
using System;

namespace PartStats
{
    class Program
    {
        static void Main(string[] args)
        {
            string param1 = "filesystem";
            string param2 = @"E:\TestResult";

            //string a = "http";
            //string b = @"E:\TestResult\httpsavepath.html";

            if (args.Length >= 2)
            {
                param1 = args[0];
                param2 = args[1];
            }
            else
            {
                Console.WriteLine("Укажите параметры:");
                Console.Write("<input_mode>");
                param1 = Console.ReadLine();
                Console.Write("<input_address>");
                param2 = Console.ReadLine();
            }

            var managerFactory = new ManagerFactory();
            var fileManager = managerFactory.Create(param1);
            fileManager.Work(param2);
        }   
    }
}
