using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_LIV
{
    class Program
    {
                

        static void Main(string[] args)
        {
            Truck t1 = new Truck();
            Truck t2 = new Truck();

            Tractor m1 = new Tractor();
            Tractor m2 = new Tractor();

            List<Car> carsForRace = new List<Car>();

            Car a = new Car("AA111AA");
            a.Repaint();
            carsForRace.Add(a);
            Car b = new Car("BB222BB");
            b.Repaint();
            carsForRace.Add(b);            
            Car golf = new Car("GG555GG");
            golf.Color = "Orange";
            carsForRace.Add(golf);

            Stopwatch s = new Stopwatch();

            s.Start();
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine(s.ElapsedMilliseconds / 1000);
            }
            s.Stop();
            Console.Clear();

            Thread car1 = new Thread(() => a.Race());
            Thread car2 = new Thread(() => b.Race());
            Thread car3 = new Thread(() => golf.Race());
            Thread semaphore = new Thread(() => Car.Semaphore());

            semaphore.Start();
            car1.Start();
            car2.Start();
            car3.Start();            
            

            Console.ReadLine();
        }

        
    }
}
