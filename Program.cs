using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupideVautour
{
    class Program
    {
        static public String playerName = "";
        static int tour = 0;
        static bool isSouris = true;
        static int val = 0;

        static void Main(string[] args)
        {
            afficheMenu();
            Console.ReadLine();

            Console.Clear();
            print("\n                     Nom > Nombre de Joueurs > Paramétrage des IA > Jeu \n\n");

            List<Main> playedCards = new List<Main>();
            List<CarteVS> turnedCards = new List<CarteVS>();
            List<Joueur> players = new List<Joueur>();
            players.Add(new Humain());
            
            int nbJoueurs;
            
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("                           _________________\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("                     Nom > Nombre de Joueurs > Paramétrage des IA > Jeu \n\n");
                print("Combien de joueurs IA en plus de vous ? (Saisie autorisée : 1~4)\n\n");
            }
            while (!int.TryParse(Console.ReadLine(), out nbJoueurs) || nbJoueurs > 4 || nbJoueurs < 1);


            print("\nAjout de " + nbJoueurs + " joueurs Ordinateur...\n\n");
            System.Threading.Thread.Sleep(500);

            nbJoueurs++;
            for (int i = 1; i < nbJoueurs; i++)
            {
                players.Add(new IA(i));
            }
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("                                                                    ___\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("                     Nom > Nombre de Joueurs > Paramétrage des IA > Jeu \n\n");
            print("Création et mélange d'un jeu de cartes...\n\n");
            System.Threading.Thread.Sleep(500);


            Talon talon = new Talon();

            for (int i = 0; i < nbJoueurs; i++)
            {
                playedCards.Add(new Main());
            }

            print("\n");
            print("Début du jeu !");
            System.Threading.Thread.Sleep(500);
            print("!");
            System.Threading.Thread.Sleep(500);
            print("!");
            System.Threading.Thread.Sleep(500);

            Console.Clear();
            print("\n                    Partie de " + playerName + "\n\n");
            print("\n");

            CarteVS carte = new CarteVS(0);

            bool rejouer = false;
            /* BOUCLE DE JEU (15 TOURS) */
            for (tour = 1; tour <= 15; tour++)
            {

                /* TIRAGE DE LA CARTE DU TALON */
                if (!rejouer) { carte = talon.tireCarte(); }
                if (rejouer) { rejouer = false; }
                isSouris = carte.isSouris();
                val = carte.getVal();


                
                /* CHAQUE JOUEUR JOUE UNE CARTE */
                CartePoints[] cartes = new CartePoints[nbJoueurs];
                foreach (Joueur pl in players)
                {
                    cartes[pl.getID()] = pl.play(carte, playedCards, turnedCards);
                }
                


                /* ON ENREGISTRE CHAQUE CARTE JOUEE PAR CHAQUE JOUEUR */
                for (int i = 0; i < nbJoueurs; i++)
                {
                    playedCards.ElementAt(i).add(cartes[i]);
                }


                /* AFFICHAGE DES CARTES JOUEES */
                foreach (Joueur pl in players)
                {
                    print("\nLe joueur " + pl.getName() + " joue sa carte : " + cartes[pl.getID()].getVal() + ".\n");
                }


                /* SUPPRESSION DES CARTES DOUBLONS */
                for (int num = 1; num <= 15; num++)
                {
                    bool existeDeja = false;
                    bool existeDeuxFois = false;
                    for (int i = 0; i < nbJoueurs; i++)
                    {
                        if (cartes[i] != null)
                        {
                            if (cartes[i].getVal() == num)
                            {
                                if (!existeDeuxFois)
                                {
                                    if (existeDeja) { existeDeuxFois = true; }
                                    else { existeDeja = true; }
                                }
                            }
                        }
                    }
                    for (int i = 0; i < nbJoueurs; i++)
                    {
                        if (cartes[i] != null)
                        {
                            if (cartes[i].getVal() == num)
                            {
                                if (existeDeuxFois)
                                {
                                    cartes[i] = null;
                                }
                            }
                        }
                    }

                }
                

                /* CALCUL VAINQUEUR */
                if (carte.isSouris())
                {
                    int max = 0;
                    int ID = -1;
                    for (int i = 0; i < nbJoueurs; i++)
                    {
                        if (cartes[i] != null)
                        {
                            if (cartes[i].getVal() > max)
                            {
                                max = cartes[i].getVal();
                                ID = i;
                            }
                        }
                    }
                    if (max == 0) { rejouer = true; print("\nÉgalité ! On rejoue pour la même carte !"); }
                    else
                    {
                        players.ElementAt(ID).giveCard(carte); print("\nLa carte va à " + players.ElementAt(ID).getName() + ".");
                        turnedCards.Add(carte); // Si non-égalité, on ajoute la carte à l'historique des cartes prises
                    }
                }
                else
                {
                    int min = 20;
                    int ID = -1;
                    for (int i = 0; i < nbJoueurs; i++)
                    {
                        if (cartes[i] != null)
                        {
                            if (cartes[i].getVal() < min)
                            {
                                min = cartes[i].getVal();
                                ID = i;
                            }
                        }
                    }
                    if (min == 20) { rejouer = true; print("\nÉgalité ! On rejoue pour la même carte !"); }
                    else
                    {
                        players.ElementAt(ID).giveCard(carte); print("\nLa carte va à " + players.ElementAt(ID).getName() + ".");
                        turnedCards.Add(carte); // Si non-égalité, on ajoute la carte à l'historique des cartes prises
                    }
                }
                Console.ReadLine();
            }

            print("\n");
            print("----- Fin du jeu ! -----\n");
            print("\n");
            print("Et voici le classement :\n");


            int playerScore = players.ElementAt(0).getPoints();

            players.Sort(
                delegate(Joueur p1, Joueur p2)
                {
                    if (p1.getPoints() < p2.getPoints()) { return 1; }
                    if (p1.getPoints() == p2.getPoints()) { return 0; }
                    if (p1.getPoints() > p2.getPoints()) { return -1; }
                    return 0;
                }
            );

            int pos = 1;
            foreach (Joueur pl in players)
            {
                print("N° " + pos + " : " + pl.getName() + " avec " + pl.getPoints() + " points.\n");
                ++pos;
            }

            Console.ReadLine();

        }

        

        public static void print(String s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                Console.Write(s[i]);
                System.Threading.Thread.Sleep(1);
            }

        }

        public static void printspecial(String s)
        {
            for (int i = 0; i < s.Length; i+=1)
            {
                Console.Write(s[i]);
                System.Threading.Thread.Sleep(1);
            }

        }

        static void afficheMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("   _____ _               _     _        \n  / ____| |             (_)   | |       \n | (___ | |_ _   _ _ __  _  __| | ___   \n  \\___ \\| __| | | | '_ \\| |/ _` |/ _ \\  \n  ____) | |_| |_| | |_) | | (_| |  __/  \n |_____/ \\__|\\__,_| .__/|_|\\__,_|\\___|  \n                  | |                   \n                  |_|                   \n __      __         _\n \\ \\    / /        | |                  \n  \\ \\  / /_ _ _   _| |_ ___  _   _ _ __ \n   \\ \\/ / _` | | | | __/ _ \\| | | | '__|\n    \\  / (_| | |_| | || (_) | |_| | |   \n     \\/ \\__,_|\\__,_|\\__\\___/ \\__,_|_|   ");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void printPartie(bool defil)
        {
            if (defil)
            {
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("                    Partie de " + playerName + ", Tour : " + tour + "\n\n");
                if (isSouris) { print("Carte SOURIS retournée ! Sa valeur est : "); }
                else { print("Carte VAUTOUR retournée ! Sa valeur est : "); }
                print(val + ".\n\n");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("                    Partie de " + playerName + ", Tour : " + tour + "\n\n");
                if (isSouris) { Console.Write("Carte SOURIS retournée ! Sa valeur est : "); }
                else { Console.Write("Carte VAUTOUR retournée ! Sa valeur est : "); }
                Console.Write(val + ".\n\n");
            }

        }
    }
}
