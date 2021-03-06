﻿using System;
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("                 ___\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("                 Nom > Nombre de Joueurs > Paramétrage des IA > Jeu \n\n");
                System.Threading.Thread.Sleep(500);
                Program.print("Quel est votre nom ? (Saisie autorisée : 3~20 chars)\n[ Le préfixe 'BOT' est réservé aux ordinateurs ]\n\n");
                a = Console.ReadLine();
            }
            while (a.Length < 3 || (a[0] == 'B' && a[1] == 'O' && a[2] == 'T'));
            a = a.Trim();
            if (a.Length > 20) { a = a.Remove(20); }
            name = a;
            Program.playerName = name;
            Program.print("\nJoueur " + name + " rejoint la partie...\n");
            System.Threading.Thread.Sleep(500);
        }
        /// <summary>
        /// Méthode de jeu du joueur,
        /// Affiche les cartes VS récupérés, les cartes de la main du joueur
        /// Demande au joueur de jouer une carte et vérifie qu'elle est présente dans sa main
        /// </summary>
        /// <returns></returns>

        public override CartePoints play(CarteVS ca, List<Main> playedCards, List<CarteVS> turnedCards)
        {
            int a = -1;
            Program.printPartie(true);
            Program.print("Voici les cartes que vous avez déjà récupérées :\n");
            if (pot.Count() == 0)
            {
                Program.print("Vous n'avez aucune carte pour le moment");
            }
            else
            {
                foreach (CarteVS c in pot)
                {
                    Program.print(c.getVal() + "  ");
                }
            }
            Program.print("\n\nQuelle carte voulez-vous jouer ? (Saisie autorisée : 1~15 si la carte existe)\n");
            afficheMain();
            Console.WriteLine();
            while (!int.TryParse(Console.ReadLine(), out a) && a > 0 && a <= 15 || !main.contient(a))
            {
                Program.printPartie(false);
                Console.Write("Voici les cartes que vous avez déjà récupérées :\n");
                if (pot.Count() == 0)
                {
                    Console.Write("Vous n'avez aucune carte pour le moment");
                }
                else
                {
                    foreach (CarteVS c in pot)
                    {
                        Console.Write(c.getVal() + "  ");
                    }
                }
                Console.Write("\n\nQuelle carte voulez-vous jouer ? (Saisie autorisée : 1~15 si la carte existe)\n");
                afficheMain();
                Console.WriteLine();
            }
            return playCarte(a);
        }
      
       
    }
}
