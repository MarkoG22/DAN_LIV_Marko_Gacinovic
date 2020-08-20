using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace DAN_LIV
{
    class Program
    {    
        static void Main(string[] args)
        {
            // truck objects
            Truck t1 = new Truck();
            Truck t2 = new Truck();

            // list of trucks
            List<Truck> truckList = new List<Truck> { t1, t2 };

            // tractor objects
            Tractor m1 = new Tractor();
            Tractor m2 = new Tractor();

            // list of tractors
            List<Tractor> tractorList = new List<Tractor> { m1, m2 };

            // list of cars
            List<Car> carsForRace = new List<Car>();

            // creating cars for the race, painting them and adding to the list
            Car a = new Car("AA111AA");
            a.Repaint();
            carsForRace.Add(a);
            Car b = new Car("BB222BB");
            b.Repaint();
            carsForRace.Add(b);            
            Car golf = new Car("GG555GG");
            golf.Color = "Orange";
            carsForRace.Add(golf);

            // stopwatch for countdown to the race
            Stopwatch s = new Stopwatch();

            s.Start();
            // countdown 5 seconds
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine("\t" + s.ElapsedMilliseconds / 1000);
            }
            s.Stop();
            Console.Clear();

            // creating the threads for the race
            Thread car1 = new Thread(() => a.Race());
            Thread car2 = new Thread(() => b.Race());
            Thread car3 = new Thread(() => golf.Race());

            // semaphore thread
            Thread semaphore = new Thread(() => Car.Semaphore());

            // starting the threads
            semaphore.Start();
            car1.Start();
            car2.Start();
            car3.Start();      

            Console.ReadLine();
        }

        
    }
}
