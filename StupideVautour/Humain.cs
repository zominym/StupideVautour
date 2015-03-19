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
        
        public bool estDansMain(int val)
        {
            foreach(CartePoints carte in main)
            {
                if(carte.getVal()==val)
                {
                    return true;
                }
            }
            return false;

        }

        public override CartePoints play()
        {
            afficheMain();
            int a = -1;
            Console.WriteLine("Indiquez la valeur de la carte à jouer");
            while (!estDansMain(a))
            {
                while (!int.TryParse(Console.ReadLine(), out a) || a > 0 || a <= 15)
                {
                    Console.WriteLine("ERREUR : Saisie non conforme.");
                    Console.WriteLine("Indiquez l'index de la carte à jouer");
                }
                if(!estDansMain(a))
                {
                    Console.WriteLine("ERREUR : Cette carte n'est pas dans votre main.");
                }
            }
            CartePoints carteAJouee = main.ElementAt(a);
            main.RemoveAt(a);
            carteJouees.Add(carteAJouee);
            return carteAJouee;
        }
      
       
    }
}
