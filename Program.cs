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
            nbJoueurs++;
            List<IA> IAs = new List<IA>();
            for (int i = 1; i < nbJoueurs; i++)
            {
                IAs.Add(new IA(i));
            }
            Console.WriteLine("Création et mélange d'un jeu de cartes...");
            Talon talon = new Talon();


            Console.WriteLine();
            Console.WriteLine("----- Début du jeu ! -----");
            Console.WriteLine();

            CarteVS carte;
            

            /* BOUCLE DE JEU (15 TOURS) */
            for (int tour = 1; tour <= 15; tour++)
            {
                Console.WriteLine();
                Console.WriteLine("----- Tour " + tour + " ! -----");

                /* TIRAGE ET AFFICHAGE DE LA CARTE DU TALON */
                carte = talon.tireCarte();
                if (carte.isSouris()) { Console.Write("Carte SOURIS retournée ! Sa valeur est : "); }
                else { Console.Write("Carte VAUTOUR retournée ! Sa valeur est : "); }
                Console.WriteLine(carte.getVal() + ".");

                
                /* CHAQUE JOUEUR JOUE UNE CARTE */
                CartePoints[] cartes = new CartePoints[nbJoueurs];
                cartes[0] = player.play();
                foreach (IA ia in IAs)
                {
                    cartes[ia.getID()] = ia.play();
                }


                /* AFFICHAGE DES CARTES JOUEES */
                Console.WriteLine("Le joueurs " + player.getName() + " joue sa carte :" + cartes[0].getVal() + ".");
                foreach (IA ia in IAs)
                {
                    Console.WriteLine("Le joueur " + ia.getName() + " joue sa carte :" + cartes[ia.getID()].getVal() + ".");
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
                    else
                    {
                        if (ID == 0) { player.giveCard(carte); Console.WriteLine("La carte va à " + player.getName() + "."); }
                        else { IAs[ID].giveCard(carte); Console.WriteLine("La carte va à " + IAs[ID].getName() + "."); } /* bug potentiel si la liste n'est pas rangée dans l'ordre des indices */
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
                    if (min == 20) { /* REJOUER */}
                    else
                    {
                        if (ID == 0) { player.giveCard(carte); }
                        else { IAs[ID].giveCard(carte); } /* bug potentiel si la liste n'est pas rangée dans l'ordre des indices */
                    }
                }
            }



            /*
            parts.Sort(delegate(Part x, Part y)
            {
                if (x.PartName == null && y.PartName == null) return 0;
                else if (x.PartName == null) return -1;
                else if (y.PartName == null) return 1;
                else return x.PartName.CompareTo(y.PartName);
            })
            */
            Console.WriteLine("DEBUG : Le joueur a " + player.getPoints() + "points.");

            Console.WriteLine();
            Console.WriteLine("----- Fin du jeu ! -----");
            Console.WriteLine(); 
            Console.WriteLine("Et voici le classement :");


            IAs.Sort(   delegate(IA x, IA y)
                        {
                            if (x.getPoints() == y.getPoints()) return 0;
                            else if (x.getPoints() > y.getPoints()) return -1;
                            else return 1;
                        });
            for (int i = 0; i <nbJoueurs; i++)
            {
                if (player.getPoints() >= IAs[0].getPoints())
                {
                    Console.WriteLine("N° " + i + ": " + player.getName() + " avec " + player.getPoints() + " points.");
                    player.setPoints(-20);
                }
                else
                {
                    Console.WriteLine("N° " + i + ": " + IAs[0].getName() + " avec " + IAs[0].getPoints() + " points.");
                    IAs.RemoveAt(0);
                }
            }

            //int playerScore = player.getPoints();
            //for (int i = 0; i < nbJoueurs; i++)
            //{
            //    int maxIA = -20;
            //    IA stock = IAs[1];
            //    foreach (IA ia in IAs)
            //    {
            //        if (ia.getPoints() > maxIA)
            //        {
            //            stock = ia;
            //        }
            //        if (maxIA > playerScore)
            //        {
            //            Console.WriteLine("N° " + i + ": " + IAs[i].getName() + " avec " + IAs[i].getPoints() + " points.");
            //            IAs.Remove(stock);
            //        }
            //        else
            //        {
            //            Console.WriteLine("N° " + i + ": " + player.getName() + " avec " + player.getPoints() + " points.");
            //            playerScore = -20;
            //        }
            //    }
            //}
            //if (playerScore != -20)
            //{
            //    Console.WriteLine("N° " + nbJoueurs + ": " + player.getName() + " avec " + player.getPoints() + " points.");
            //}

            Console.ReadLine();

        }
    }
}
