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

            int a;
            Console.WriteLine("Combien de joueurs IA en plus de vous ? ( 1~4 )");
            while (!int.TryParse(Console.ReadLine(), out a) || a > 4 || a < 1 )
            {
                Console.WriteLine("Combien de joueurs IA en plus de vous ? ( 1~4 )");
            }
            Console.WriteLine("Ajout de " + a + " joueurs Ordinateur.");
            Joueur[] players = new IA[1 + a];
            players[0] = new Humain();

            Console.WriteLine("Création et mélange d'un jeu de cartes.");
            Talon talon = new Talon();
            talon.shuffle();
            
            
            Console.ReadLine();

        }
    }
}
