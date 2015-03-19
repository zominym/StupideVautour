using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupideVautour
{
    class Program
    {
        static void Main(string[] args)
        {

            double a, b;
            Console.WriteLine("istenen sayıyı sonuna .00 koyarak yaz");
            while (!double.TryParse(Console.ReadLine(), out a))
            {

            }
            b = a * a;
            Console.WriteLine("Réponse : " + b);
            Console.ReadLine();

        }
    }
}
