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
            Console.WriteLine("Quel est votre nom ? (4~20 chars)");
            string a = Console.ReadLine();
            a = a.Trim();
            if (a.Length > 20) { a = a.Remove(20); }
            while (a.Length < 3 || (a[0] == 'B' && a[1] == 'O' && a[2] == 'T'))
            {
                Console.WriteLine("ERREUR DE SAISIE. (Le préfixe 'BOT' est réservé aux ordinateurs.)");
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
            Console.WriteLine("Quelle carte voulez-vous jouer ? (1~15)");
            while (!int.TryParse(Console.ReadLine(), out a) && a > 0 && a <= 15 || !estDansMain(a))
            {
                Console.WriteLine("ERREUR : Saisie non conforme.");
                Console.WriteLine("Quelle carte voulez-vous jouer ? (1~15)");
            }
            CartePoints carteAJouer = new CartePoints(0);
            foreach (CartePoints carte in main)
            {
                if (carte.getVal() == a)
                {
                    carteAJouer = new CartePoints(carte.getVal());
                    carteJouees.Add(carteAJouer);
                    main.Remove(carte);
                    break;
                }
            }            
            return carteAJouer;
        }
      
       
    }
}
