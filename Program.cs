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
            Humain player = new Humain();
            int a;
            Console.WriteLine("Combien de joueurs IA en plus de vous ? ( 1~4 )");
            while (!int.TryParse(Console.ReadLine(), out a) || a > 4 || a < 1 )
            {
                Console.WriteLine("ERREUR : Saisie non conforme.");
                Console.WriteLine("Combien de joueurs IA en plus de vous ? ( 1~4 )");
            }
            Console.WriteLine("Ajout de " + a + " joueurs Ordinateur...");
            List<IA> IAs = new List<IA>();
            for (int i = 1; i < a; i++)
            {
                IAs.Add(new IA(i));
            }

                Console.WriteLine("Création et mélange d'un jeu de cartes...");
            Talon talon = new Talon();

            Console.WriteLine("Début du jeu !");

            
            Console.ReadLine();

        }
    }
}
