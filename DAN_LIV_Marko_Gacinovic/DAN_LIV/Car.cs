using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace DAN_LIV
{
    class Car : Vehicle
    {
        public string RegNum { get; set; }
        public int NumberOfDoors { get; set; }
        public int TankVolume { get; set; }
        public string TransmissionType { get; set; }
        public string Manufacturer { get; set; }
        public int TrafficCardNumber { get; set; }
        public int FuelConsumption { get; set; }

        static Random rnd = new Random();
               
        static AutoResetEvent auto = new AutoResetEvent(false);        
        static CountdownEvent countdown = new CountdownEvent(3);
        static object locker = new object();
        
        public Car(string regNum)
        {
            RegNum = regNum;
            FuelLitres = rnd.Next(52,70);
            FuelConsumption = rnd.Next(4,7);
        }

        public void Repaint()
        {
            TrafficCardNumber = rnd.Next(10000, 100000);
            Color = "Red";            
        }

        public override void Start()
        {
            Console.WriteLine("Car {0} ready for start.", RegNum);
            countdown.Signal();
        }

        public override void Stop()
        {
            Console.WriteLine("Car {0} stopped at semaphore.", RegNum);
        }

        public void Race()
        {
            Start();
            countdown.Wait();

            Stopwatch s1 = new Stopwatch();
            Stopwatch s2 = new Stopwatch();
            Stopwatch s3 = new Stopwatch();

            s1.Start();
            while (s1.ElapsedMilliseconds < 10000)
            {
                if (FuelLitres > 0)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Car {0} is moving...", RegNum);
                    FuelLitres = FuelLitres - FuelConsumption;
                }
                else
                {
                    Console.WriteLine("Car {0} ended the race. Fuel empty.", RegNum);
                    return;
                }
            }
            s1.Stop();

            if (auto.WaitOne())
            {
                Stop();
            }

            s2.Start();
            while (s2.ElapsedMilliseconds < 3000)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Car {0} is moving...", RegNum);
                FuelLitres = FuelLitres - FuelConsumption;
            }
            s2.Stop();

            if (FuelLitres < 15)
            {
                lock (locker)
                {
                    Console.WriteLine("Car {0} stopped at gas station.", RegNum);
                    Console.WriteLine("Car {0} refueled.", RegNum);
                    FuelLitres = 50;
                }
            }

            s3.Start();
            while (s3.ElapsedMilliseconds < 7000)
            {
                if (FuelLitres > 1)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Car {0} is moving...", RegNum);
                    FuelLitres = FuelLitres - (int)FuelConsumption;
                }
                else
                {
                    Console.WriteLine("Car {0} ended the race. Fuel empty.", RegNum);
                    return;
                }
            }
            s3.Stop();            

            Console.WriteLine("\n\t {0} car {1} ended the race successfully.\n", Color, RegNum);
        }

        public static void Semaphore()
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            while (s.ElapsedMilliseconds < 20000)
            {
                Thread.Sleep(2000);
                auto.Set();

                Thread.Sleep(2000);
                auto.Reset();
            }
            s.Stop();
        }
    }
}
