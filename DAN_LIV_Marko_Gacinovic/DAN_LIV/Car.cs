using System;
using System.Threading;
using System.Diagnostics;

namespace DAN_LIV
{
    class Car : Vehicle
    {
        // properties
        public string RegNum { get; set; }
        public int NumberOfDoors { get; set; }
        public int TankVolume { get; set; }
        public string TransmissionType { get; set; }
        public string Manufacturer { get; set; }
        public int TrafficCardNumber { get; set; }
        public int FuelConsumption { get; set; }

        // random object for getting random values
        static Random rnd = new Random();
        
        // autoresetevent for the semaphore
        static AutoResetEvent auto = new AutoResetEvent(false);
        
        // countdownevent for starting the race at the same time
        static CountdownEvent countdown = new CountdownEvent(3);

        // locking object for the gas station
        static object locker = new object();
        
        // constructor
        public Car(string regNum)
        {
            RegNum = regNum;
            // random values for the fuel and fuel consumption
            FuelLitres = rnd.Next(50,70);
            FuelConsumption = rnd.Next(4,7);
        }

        /// <summary>
        /// method for repaintg the cars
        /// </summary>
        public void Repaint()
        {
            TrafficCardNumber = rnd.Next(10000, 100000);
            Color = "Red";            
        }

        /// <summary>
        /// method for signaling that the cars are ready to start
        /// </summary>
        public override void Start()
        {
            Console.WriteLine("Car {0} ready for start.", RegNum);
            countdown.Signal();
        }

        /// <summary>
        /// method for stopping at the semaphore
        /// </summary>
        public override void Stop()
        {
            Console.WriteLine("\nCar {0} stopped at semaphore.", RegNum);
        }

        /// <summary>
        /// method for the racing
        /// </summary>
        public void Race()
        {
            Start();
            
            // waiting all cars to be ready
            countdown.Wait();

            // stopwatches for timekeeping
            Stopwatch s1 = new Stopwatch();
            Stopwatch s2 = new Stopwatch();
            Stopwatch s3 = new Stopwatch();

            s1.Start();
            // loop for checking and calculating the fuel
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
                    Console.WriteLine("\nCar {0} ended the race. Fuel empty.\n", RegNum);
                    return;
                }
            }
            s1.Stop();

            // checking the light at the semaphore
            if (auto.WaitOne())
            {
                Stop();
            }

            s2.Start();
            // loop for checking and calculating the fuel
            while (s2.ElapsedMilliseconds < 3000)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Car {0} is moving...", RegNum);
                FuelLitres = FuelLitres - FuelConsumption;
            }
            s2.Stop();

            // loop for checking and calculating the fuel
            if (FuelLitres < 15)
            {
                // lock for refueling at the gas station one by one
                lock (locker)
                {
                    Console.WriteLine("\nCar {0} stopped at gas station.", RegNum);
                    Console.WriteLine("Car {0} refueled.\n", RegNum);
                    FuelLitres = 50;
                }
            }

            s3.Start();
            // loop for checking and calculating the fuel
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
                    Console.WriteLine("\nCar {0} ended the race. Fuel empty.\n", RegNum);
                    return;
                }
            }
            s3.Stop();            

            // finishing the race
            Console.WriteLine("\n\t {0} car {1} ended the race successfully.\n", Color, RegNum);
        }

        /// <summary>
        /// method for switching lights at the semaphore
        /// </summary>
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
