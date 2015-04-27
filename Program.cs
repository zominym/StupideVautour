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
            print("\n                     Nom > Nombre d'IA > Difficulté > Jeu \n\n");
            System.Threading.Thread.Sleep(500);

            List<Main> playedCards = new List<Main>();
            List<CarteVS> turnedCards = new List<CarteVS>();
            Humain player = new Humain();
            
            int nbJoueurs;
            
            do
            {
                Console.Clear();
                Console.Write("                           ___________\n");
                Console.Write("                     Nom > Nombre d'IA > Difficulté > Jeu \n\n");
                print("Combien de joueurs IA en plus de vous ? (Saisie autorisée : 1~4)\n\n");  /* FAIRE LE "AJOUTER UNE IA (0=débile, 1=random)" */
            }
            while (!int.TryParse(Console.ReadLine(), out nbJoueurs) || nbJoueurs > 4 || nbJoueurs < 1);


            print("\nAjout de " + nbJoueurs + " joueurs Ordinateur.");
            System.Threading.Thread.Sleep(500);
            print(".");
            System.Threading.Thread.Sleep(500);
            print(".\n\n");
            System.Threading.Thread.Sleep(500);


            nbJoueurs++;
            List<IA> IAs = new List<IA>();
            for (int i = 1; i < nbJoueurs; i++)
            {
                IAs.Add(new IA(i));
            }
            foreach (IA ia in IAs)
            {
                Console.Clear();
                Console.Write("                                         __________\n");
                Console.Write("                     Nom > Nombre d'IA > Difficulté > Jeu \n\n");
                if (ia.getID() == 0) { print("Détermination de la difficulté des joueurs :\n\n"); }
                else { Console.Write("Détermination de la difficulté des joueurs :\n\n"); }
                int diff;
                do
                {
                    print("Quelle difficulté pour le joueur " + ia.getName() + " ?\n");
                    print("Aléatoire          |         Facile\n");
                    print("    0                           1  \n\n");
                }
                while (!int.TryParse(Console.ReadLine(), out diff) || diff > 2 || diff < 0);
                ia.setDifficulty(diff);
            }

            Console.Clear();
            Console.Write("                                                      ___\n");
            Console.Write("                     Nom > Nombre d'IA > Difficulté > Jeu \n\n");
            print("Création et mélange d'un jeu de cartes.");
            System.Threading.Thread.Sleep(500);
            print(".");
            System.Threading.Thread.Sleep(500);
            print(".\n\n");
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
                foreach (IA ia in IAs)
                {
                    cartes[ia.getID()] = ia.play(carte, playedCards, turnedCards);
                }
                cartes[0] = player.play();
                


                /* ON ENREGISTRE CHAQUE CARTE JOUEE PAR CHAQUE JOUEUR */
                for (int i = 0; i < nbJoueurs; i++)
                {
                    playedCards.ElementAt(i).add(cartes[i]);
                }


                /* AFFICHAGE DES CARTES JOUEES */
                print("\nLe joueurs " + player.getName() + " joue sa carte : " + cartes[0].getVal() + ".\n");
                foreach (IA ia in IAs)
                {
                    print("\nLe joueur " + ia.getName() + " joue sa carte : " + cartes[ia.getID()].getVal() + ".\n");
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
                    if (max == 0) { rejouer = true; }
                    else
                    {
                        if (ID == 0) { player.giveCard(carte); print("\nLa carte va à " + player.getName() + ".\n"); }
                        else { ID--; IAs.ElementAt(ID).giveCard(carte); print("\nLa carte va à " + IAs.ElementAt(ID).getName() + ".\n"); } /* bug potentiel si la liste n'est pas rangée dans l'ordre des indices */
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
                    if (min == 20) { rejouer = true;  }
                    else
                    {
                        if (ID == 0) { player.giveCard(carte); print("\nLa carte va à " + player.getName() + ".\n"); }
                        else { ID--; IAs.ElementAt(ID).giveCard(carte); print("\nLa carte va à " + IAs.ElementAt(ID).getName() + ".\n"); } /* bug potentiel si la liste n'est pas rangée dans l'ordre des indices */
                        turnedCards.Add(carte); // Si non-égalité, on ajoute la carte à l'historique des cartes prises(
                    }
                }
                Console.ReadLine();
            }

            print("\n");
            print("----- Fin du jeu ! -----\n");
            print("\n");
            print("Et voici le classement :\n");


            int playerScore = player.getPoints();
            for (int i = 0; i < nbJoueurs; i++)
            {
                IA stock = new IA();
                int maxIA = -20;
                foreach (IA ia in IAs)
                {
                    if (ia.getPoints() >= maxIA)
                    {
                        stock = ia;
                        maxIA = ia.getPoints();
                    }
                }
                if (maxIA > playerScore)
                {
                    print("N° " + (i+1) + ": " + stock.getName() + " avec " + stock.getPoints() + " points.");
                    IAs.Remove(stock);
                }
                else
                {
                    print("N° " + (i+1) + ": " + player.getName() + " avec " + player.getPoints() + " points.");
                    playerScore = -20;
                }
            }
            if (playerScore != -20)
            {
                print("N° " + nbJoueurs + ": " + player.getName() + " avec " + player.getPoints() + " points.");
            }

            Console.ReadLine();

        }

        

        public static void print(String s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                Console.Write(s[i]);
                System.Threading.Thread.Sleep(10);
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
            printspecial("   _____ _               _     _        \n  / ____| |             (_)   | |       \n | (___ | |_ _   _ _ __  _  __| | ___   \n  \\___ \\| __| | | | '_ \\| |/ _` |/ _ \\  \n  ____) | |_| |_| | |_) | | (_| |  __/  \n |_____/ \\__|\\__,_| .__/|_|\\__,_|\\___|  \n                  | |                   \n                  |_|                   \n __      __         _\n \\ \\    / /        | |                  \n  \\ \\  / /_ _ _   _| |_ ___  _   _ _ __ \n   \\ \\/ / _` | | | | __/ _ \\| | | | '__|\n    \\  / (_| | |_| | || (_) | |_| | |   \n     \\/ \\__,_|\\__,_|\\__\\___/ \\__,_|_|   ");
        }

        public static void printPartie(String s, bool defil)
        {
            if (defil)
            {
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("                    Partie de " + playerName + ", Tour : " + tour + "\n\n");
                if (isSouris) { print("Carte SOURIS retournée ! Sa valeur est : "); }
                else { print("Carte VAUTOUR retournée ! Sa valeur est : "); }
                print(val + ".\n\n");
                for (int i = 0; i < s.Length; i++)
                {
                    Console.Write(s[i]);
                    System.Threading.Thread.Sleep(10);
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("                    Partie de " + playerName + ", Tour : " + tour + "\n\n");
                if (isSouris) { Console.Write("Carte SOURIS retournée ! Sa valeur est : "); }
                else { Console.Write("Carte VAUTOUR retournée ! Sa valeur est : "); }
                Console.Write(val + ".\n\n");
                Console.Write(s);
            }

        }
    }
}
