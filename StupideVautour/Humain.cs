using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupideVautour
{
    class Humain : Joueur
    {

        public Humain()
        : base( -1 )
        {
            Console.WriteLine("\tQuel est votre nom ?");
            string a = Console.ReadLine();
            while (a[0] == 'B' && a[1] == 'O' && a[2] == 'T' && a[3] == '_')
            {
                Console.WriteLine("ERREUR : Le préfixe 'BOT_' est réservé aux ordinateurs.");
                a = Console.ReadLine();
            }
        }
    }
}
