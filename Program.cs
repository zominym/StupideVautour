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
            for (int i = 1; i < nbJoueurs; i++)
            {
                IAs.Add(new IA(i));
            }

                Console.WriteLine("Création et mélange d'un jeu de cartes...");
            Talon talon = new Talon();

            Console.WriteLine("----- Début du jeu ! -----");

            CarteVS carte;


            /* BOUCLE DE JEU (15 TOURS) */
            for (int tour = 1; tour <= 15; tour++ )
            {

                /* TIRAGE ET AFFICHAGE DE LA CARTE DU TALON */
                carte = talon.tireCarte();
                if (carte.isSouris()) { Console.WriteLine("Carte souris retournée ! Sa valeur est :"); }
                else { Console.WriteLine("Carte vautour retournée ! Sa valeur est :"); }
                Console.Write(carte.getVal() + ".");

                
                /* CHAQUE JOUEUR JOUE UNE CARTE */
                CartePoints[] cartes = new CartePoints[nbJoueurs];
                cartes[0] = player.play();
                foreach (IA ia in IAs)
                {
                    cartes[ia.getID()] = ia.play();
                }


                /* AFFICHAGE DES CARTES JOUEES */
                Console.WriteLine("Le joueurs "+player.getName()+"joue sa carte :"+cartes[0]+".");
                foreach (IA ia in IAs)
                {
                    Console.WriteLine("Le joueur "+ia.getName()+" joue sa carte :"+cartes[ia.getID()]+".");
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
                    if (max == 0) {/* REJOUER */}
                    else { /* donner la carte au joueur qui a mis le max */ }
                }
                else
                {
                    /* pareil avec min */
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
