using HwidProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hwid_Protection_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            EnsureHwidValid();

            Console.WriteLine("Hello World!");
            Console.Read();
        }

        private static void EnsureHwidValid()
        {
            HwidClient client = new HwidClient("https://localhost:44306/");
            bool isValid = client.VerifyAsync().Result;
            if (!isValid)
            {
                throw new Exception("You aren't allowed to run this program.");
            }
        }
    }
}
