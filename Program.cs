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
            int nbJoueurs;
            Console.WriteLine("Combien de joueurs IA en plus de vous ? ( 1~4 )");
            while (!int.TryParse(Console.ReadLine(), out nbJoueurs) || nbJoueurs > 4 || nbJoueurs < 1 )
            {
                Console.WriteLine("ERREUR : Saisie non conforme.");
                Console.WriteLine("Combien de joueurs IA en plus de vous ? ( 1~4 )");
            }
            Console.WriteLine("Ajout de " + nbJoueurs + " joueurs Ordinateur...");
            List<IA> IAs = new List<IA>();
            for (int i = 0; i < nbJoueurs; i++)
            {
                IAs.Add(new IA(i));
            }

                Console.WriteLine("Création et mélange d'un jeu de cartes...");
            Talon talon = new Talon();

            Console.WriteLine("----- Début du jeu ! -----");

            CarteVS carte;
            for (int tour = 1; tour <= 15; tour++ )
            {
                carte = talon.tireCarte();
                player.play();
                foreach (IA ia in IAs)
                {
                    IAs.play();
                }
            }

            Console.WriteLine("----- Fin du jeu ! -----");
            Console.WriteLine("Et voici le classement :");
            IAs.Sort();
            int indexIA = 0;
            int indexJ = 0;
            int playerScore = player.getPoints();
            for (int i = 0; i < nbJoueurs; i++)
            {
                int maxIA = -20;
                IA stock;
                foreach (IA ia in IAs)
                {
                    if (ia.getPoints() > maxIA)
                    {
                        stock = ia;
                    }
                    if (maxIA > playerScore)
                    {
                        Console.WriteLine("N° " + i + ": " + IAs[i].getName() + " avec " + IAs[i].getPoints() + " points.");
                        IAs.Remove(stock);
                    }
                    else
                    {
                        Console.WriteLine("N° " + i + ": " + player.getName() + " avec " + player.getPoints() + " points.");
                        playerScore = -20;
                    }
                }
            }
            if (playerScore != -20)
            {
                Console.WriteLine("N° " + nbJoueurs + ": " + player.getName() + " avec " + player.getPoints() + " points.");
            }

            Console.ReadLine();

        }
    }
}
