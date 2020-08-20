using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class Common
    {
        public static int Counter = 0;
        public static int ConstrCount = 0;

        static Common()
        {
            ConstrCount++;
        }

        public static bool IsEmpty(string value)
        {
            if (value.Length ==0)
            {
                return true;
            }
            return false;
        }

        public static string GetComputerName()
        {
            return "HP14";
        }
    }

    public class CustomerClass
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        private string MachineName = "";

        public CustomerClass()
        {
            MachineName = Common.GetComputerName();
        }

        public void Insert()
        {
            Common.Counter++;
        }
    }

    public class CountryMaster
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        private string MachineName = "";

        public CountryMaster()
        {
            MachineName = Common.GetComputerName();
        }

        public void Insert()
        {
            Common.Counter++;
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            CustomerClass objCustomer = new CustomerClass();
            CountryMaster objCounty = new CountryMaster();

            objCustomer.CustomerCode = "ABC";
            objCustomer.CustomerName = "Mile";
            objCustomer.Insert();

            objCounty.CountryCode = "SRB";
            objCounty.CountryName = "Serbia";
            objCounty.Insert();

            Console.WriteLine("{0}", Common.Counter);
            Console.WriteLine("{0}", Common.ConstrCount);



            Console.ReadLine();
        }
    }
}

