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
            Console.WriteLine("Quel est votre nom ?");
            string a = Console.ReadLine();
            while (a[0] == 'B' && a[1] == 'O' && a[2] == 'T' && a[3] == '_')
            {
                Console.WriteLine("ERREUR : Le préfixe 'BOT_' est réservé aux ordinateurs.");
                Console.WriteLine("Quel est votre nom ?");
                a = Console.ReadLine();
            }
            name = a;
            Console.WriteLine("Joueur " + name + " a rejoint la partie.");
        }
        public override CartePoints play()
        {
            Console.WriteLine("Voici les cartes restantent dans votre main :");
            int a;
            for (int i = 0; i < main.Count();i++)
            {
                Console.WriteLine("Element numero :" + i + "valeur de la carte : " + main.ElementAt(i).getVal());
            }
            Console.WriteLine("Indiquez l'index de la carte à jouer");
            while (!int.TryParse(Console.ReadLine(), out a) || a > 0 || a < main.Count())
            {
                Console.WriteLine("ERREUR : Saisie non conforme.");
                Console.WriteLine("Indiquez l'index de la carte à jouer");
            }
            return main.ElementAt(a);
        }
      
       
    }
}
