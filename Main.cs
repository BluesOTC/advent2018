﻿using System;

namespace Advent
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            DateTime last = start;
            Day15Dijkstra.Run();
            Console.WriteLine("Run Time: " + (DateTime.Now - last));
            last = DateTime.Now;
            Day1.Run();
            Console.WriteLine("Run Time: " + (DateTime.Now - last));
            last = DateTime.Now;
            Day2.Run();
            Console.WriteLine("Run Time: " + (DateTime.Now - last));
            last = DateTime.Now;
            Day3.Run();
            Console.WriteLine("Run Time: " + (DateTime.Now - last));
            last = DateTime.Now;
            Day4.Run();
            Console.WriteLine("Run Time: " + (DateTime.Now - last));
            last = DateTime.Now;
            Day5.Run();
            Console.WriteLine("Run Time: " + (DateTime.Now - last));
            last = DateTime.Now;
            Day6.Run();
            Console.WriteLine("Run Time: " + (DateTime.Now - last));
            last = DateTime.Now;
            Day7.Run();
            Console.WriteLine("Run Time: " + (DateTime.Now - last));
            last = DateTime.Now;
            Day8.Run();
            Console.WriteLine("Run Time: " + (DateTime.Now - last));
            last = DateTime.Now;
            Day9LinkedList.Run();
            Console.WriteLine("Run Time: " + (DateTime.Now - last));
            last = DateTime.Now;
            /*Day10.Run(); //this output is ugly
            Console.WriteLine("Run Time: " + (DateTime.Now - last));
            last = DateTime.Now;*/
            Day11.Run();
            Console.WriteLine("Run Time: " + (DateTime.Now - last));
            last = DateTime.Now;
            Day12.Run();
            Console.WriteLine("Run Time: " + (DateTime.Now - last));
            last = DateTime.Now;
            Day13.Run();
            Console.WriteLine("Run Time: " + (DateTime.Now - last));
            last = DateTime.Now;
            Day14.Run();
            Console.WriteLine("Run Time: " + (DateTime.Now - last));
            last = DateTime.Now;
            Day16.Run();
            Console.WriteLine("Run Time: " + (DateTime.Now - last));
            last = DateTime.Now;
            Console.WriteLine("\nTotal Run Time: " + (DateTime.Now - start));
        }
    }
}
