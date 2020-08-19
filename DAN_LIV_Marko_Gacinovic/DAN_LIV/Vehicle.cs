using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_LIV
{
    public abstract class Vehicle
    {
        double EngineDisplacement { get; set; }
        public int Weight { get; set; }
        public string Category { get; set; }
        public string MotorType { get; set; }
        public string Color { get; set; }
        public int MotorNumber { get; set; }

        public abstract void Start();
        public abstract void Stop();
    }
}
