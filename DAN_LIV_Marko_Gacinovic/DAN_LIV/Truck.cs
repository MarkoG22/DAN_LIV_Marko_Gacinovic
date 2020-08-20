using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_LIV
{
    class Truck : Vehicle
    {
        public double LoadCapacity { get; set; }
        public double Height { get; set; }
        public int NumberOfSeats { get; set; }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        public void Load()
        {

        }

        public void Unload()
        {

        }
    }
}
