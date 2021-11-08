using System;
using System.Threading;
using Controller;
using Model;

namespace race_simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Data.Initialize();
            Data.NextRace();
            Console.WriteLine(Data.CurrentRace.track.Name);
            Visualisation.Initialize();
            Visualisation.DrawTrack(Data.CurrentRace.track);
            
            // game loop
             for (; ; )
             {
                 Thread.Sleep(100);
             }
        }
    }
}
