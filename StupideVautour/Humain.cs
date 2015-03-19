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
        
       

        public override CartePoints play()
        {
            afficheMain();
            int a = -1;
            Console.WriteLine("Indiquez la valeur de la carte à jouer");
            while (!int.TryParse(Console.ReadLine(), out a) || a > 0 || a <= 15 || !estDansMain(a))
            {
                Console.WriteLine("ERREUR : Saisie non conforme.");
                Console.WriteLine("Indiquez la valeur de la carte à jouer");
            }           
            CartePoints carteAJouee = this.poseCarte(a);
            carteJouees.Add(carteAJouee);
            return carteAJouee;
        }
      
       
    }
}
