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
        : base( 0 )
        {
            String a;
            do
            {
                Console.WriteLine("Quel est votre nom ? (Saisie autorisée : 3~20 chars, le préfixe 'BOT' est réservé aux ordinateurs)");
                a = Console.ReadLine();
            }
            while (a.Length < 3 || (a[0] == 'B' && a[1] == 'O' && a[2] == 'T'));
            a = a.Trim();
            if (a.Length > 20) { a = a.Remove(20); }
            name = a;
            Console.WriteLine("Joueur " + name + " a rejoint la partie.");
        }

        public CartePoints play()
        {
            afficheMain();
            int a = -1;
            do
            {
                Console.WriteLine("Quelle carte voulez-vous jouer ? (Saisie autorisée : 1~15 si la carte existe)");
            }
            while (!int.TryParse(Console.ReadLine(), out a) && a > 0 && a <= 15 || !estDansMain(a)) ;
            return playCarte(a);
        }
      
       
    }
}
