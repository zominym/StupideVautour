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
                Console.Clear();
                Console.Write("                     ___\n");
                Console.Write("                     Nom > Nombre d'IA > Difficulté > Jeu \n\n");
                System.Threading.Thread.Sleep(500);
                Program.print("Quel est votre nom ? (Saisie autorisée : 3~20 chars)\n[ Le préfixe 'BOT' est réservé aux ordinateurs ]\n\n");
                a = Console.ReadLine();
            }
            while (a.Length < 3 || (a[0] == 'B' && a[1] == 'O' && a[2] == 'T'));
            a = a.Trim();
            if (a.Length > 20) { a = a.Remove(20); }
            name = a;
            Program.playerName = name;
            Program.print("\nJoueur " + name + " rejoint la partie.");
            System.Threading.Thread.Sleep(500);
            Program.print(".");
            System.Threading.Thread.Sleep(500);
            Program.print(".\n");
            System.Threading.Thread.Sleep(500);
        }

        public CartePoints play()
        {
            int a = -1;
            Program.printPartie("Quelle carte voulez-vous jouer ? (Saisie autorisée : 1~15 si la carte existe)\n", true);
            Console.WriteLine();
            do
            {
                Program.printPartie("Quelle carte voulez-vous jouer ? (Saisie autorisée : 1~15 si la carte existe)\n", false);
                afficheMain();
                Console.WriteLine();
            }
            while (!int.TryParse(Console.ReadLine(), out a) && a > 0 && a <= 15 || !estDansMain(a)) ;
            return playCarte(a);
        }
      
       
    }
}
