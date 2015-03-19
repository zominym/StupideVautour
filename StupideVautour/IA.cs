using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupideVautour
{
    class IA : Joueur
    {


        public IA(int i)
        {
            ID = i;
            switch (ID)
            {
                case 0: name = "BOT_Alan"; break;
                case 1: name = "BOT_Bert"; break;
                case 2: name = "BOT_Chen"; break;
                case 3: name = "BOT_Dean"; break;
                default: name = "BOT_ERROR"; break;
            }
            Console.WriteLine("Ordinateur " + name + " a rejoint la partie.");
        }
    }
}
