﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupideVautour
{
    class IA : Joueur
    {

        int diff;
        Main myPlayedCards = new Main();
        Talon t = null;

        public IA()
            :base(-1)
        {
            // TODO: Complete member initialization
        }

        public IA(int i)
        :base(i)
        {
            switch (ID)
            {
                case 1: name = "BOT_Alan"; break;
                case 2: name = "BOT_Bert"; break;
                case 3: name = "BOT_Chen"; break;
                case 4: name = "BOT_Dean"; break;
                default: name = "BOT_ERROR"; break;
            }
            this.diff = 0;
            Program.print("Joueur " + name + " a rejoint la partie...\n");
        }

        public void setDifficulty(int i)
        {
            this.diff = i;
        }

        public CartePoints play(CarteVS carteTournee, List<Main> playedCards, List<CarteVS> turnedCards)
        {
            switch (diff)
            {
                case 0:
                    return playRandom();
                case 1:
                    return playInOrder(carteTournee, turnedCards);
                case 2:
                    return playMemory(carteTournee, playedCards, turnedCards);
                    ;
                default:
                    return null;
            }

        }

        // Permet de récuperer la liste des cartes joués par les autres joueur 
        // A partir de l'ensemble  des cartes joués
        public List<Main> otherPlayedCards(List<Main> playedCards)
        {
            List<Main> allPlayedCards = new List<Main>();
            int i = 0; 
            foreach(Main m in playedCards)
            {
                allPlayedCards.Add(playedCards.ElementAt(i));
                i++;
            }
            allPlayedCards.RemoveAt(ID);
            return allPlayedCards;
        }


        // Retourne la liste des cartes de notre main meilleure qu'une certaine carte
        public List<CartePoints> listCardBetterThan(CartePoints bestCardPlayers)
        {
            List<CartePoints> cardBetterThan = new List<CartePoints>();
            foreach(CartePoints card in main.cartes)
            {
                if(card.getVal()>bestCardPlayers.getVal())
                {
                    cardBetterThan.Add(card);
                }
            }
            return cardBetterThan;
        }


        // Retourne la liste des carte VS restantes meilleures que la carte VS retournée
        public int cardVSBetterThan(CarteVS carteTourne)
        {
            int nbCardMorePrioritary = 0;
            foreach(CarteVS card in t.cartes)
            {
                if(Math.Abs(card.getVal())>carteTourne.getVal())
                {
                    nbCardMorePrioritary++;
                }
            }
            return nbCardMorePrioritary;
        }
        
        // Joue une carte de manière aléatoire
        private CartePoints playRandom()
        {
            CartePoints temp = new CartePoints(0);
            Random i = new Random();
            int pos = (int)i.Next(main.count());
            temp = main.cartes.ElementAt(pos);
            myPlayedCards.cartes.Add(main.cartes.ElementAt(pos));
            main.cartes.RemoveAt(pos);
            return temp;
        }

        private int Order(CarteVS carte, List<CarteVS> hist)
        {
            t = new Talon(0);
            for (int i = 1; i <= 5; i++)
            {
                t.cartes.Add(new CarteVS(i));
                t.cartes.Add(new CarteVS(-i));
            }
            for (int i = 6; i <= 10; i++)
            {
                t.cartes.Add(new CarteVS(i));
            }
            
            foreach (CarteVS c in hist)
            {
                t.remove(c);
            }

            int pos1 = main.count() - 1;
            int pos2 = main.count() - 1;
            foreach(CarteVS c in t.getCartes())
            {
                if (Math.Abs(c.getVal()) > Math.Abs(carte.getVal()))
                {
                    pos1--;
                    //print("DEBUG : PlayInOrder : pos1-- car " + c.getVal() + " > " + carte.getVal());
                }
                if (Math.Abs(c.getVal()) >= Math.Abs(carte.getVal()))
                {
                    pos2--;
                    //print("DEBUG : PlayInOrder : pos2-- car " + c.getVal() + " >= " + carte.getVal());
                }
            }
            int pos = pos1;
            pos2++;
            //Program.print("DEBUG : Alea entre " + pos1 + " et " + pos2);
            Random rand = new Random();
            pos = rand.Next(0, 1);
            if (pos == 1)
            {
                pos = pos1;
            }
            else
            {
                pos = pos2;
            }
            if (main.cartes.Count() > 5 && main.cartes.Count() <= 10) { pos += rand.Next(-1, 1); }
            if (main.cartes.Count() > 10) { pos += rand.Next(-2, 2); }
            //Console.Write("pos :" + pos); Console.ReadLine();
            if(pos < 0)
            {
                pos = 0;
            }
            if(pos >= main.cartes.Count())
            {
                pos = main.cartes.Count() - 1;
            }
            myPlayedCards.cartes.Add(main.cartes.ElementAt(pos));
            return (main.cartes.ElementAt(pos).getVal());
        }


        /* IA PlayInOrder : Chaque carte Vautour/Souris a une importance,
         * il joue la carte à points qui a la même importance
         * (souris 10 et carte 15 ont la même importance (maximale)
         * Cependant l'ordre change car il vaut mieux éviter
         * une carte vautour -5 que ramasser une carte souris 4
         */
        private CartePoints playInOrder(CarteVS c, List<CarteVS> hist)
        {
            return playCarte(Order(c,hist));
        }

      
        // Retourne la meilleur carte d'un joueur 
        private  List<CartePoints> bestCardPlayers(List<Main> listPlayedCards)
        {
            List<CartePoints> bestCardPlayers = new List<CartePoints>();
            foreach(Main mainPlayer in listPlayedCards)
            {   
                // On parcourt les cartes par valeurs décroissante
                for(int i = 15;i >0;i--)
                {
                    // Dés qu'une carte n'est pas dans la liste des cartes joués, c'est la plus grande carte restante
                    if(!mainPlayer.contient(new CartePoints(i)))
                    {
                        bestCardPlayers.Add(new CartePoints(i));
                        break;
                    }
                }
            }
            return bestCardPlayers;
        }


        // Retourne la pire carte d'un joueur
        private List<CartePoints> worstCardPlayers(List<Main> playedCards)
        {
            List<CartePoints> worstCardPlayers = new List<CartePoints>();
            foreach (Main mainPlayer in playedCards)
            {
                // Parcourt les cartes dans l'ordre croissant
                for (int i = 1; i < 16; i++)
                {
                    // Dés qu'une carte n'est pas dans la liste des cartes joués, c'est la plus petit carte restante
                    if (!mainPlayer.contient(new CartePoints(i)))
                    {
                        worstCardPlayers.Add(new CartePoints(i));
                        break;
                    }
                }
            }
            return worstCardPlayers;
        }


        /* IA PlayMemory : Se rappelle chaque carte jouee par chaque joueur,
        * se rappelle aussi chaque carte du talon déjà jouée
        * en déduit des informations comme :
        * - a-t-elle la meilleure carte ?
        * - a-t-elle la pire carte ?
        * Suit la même règle d'importance que playInOrder ???
        */
      
        private CartePoints playMemory(CarteVS carteTournee, List<Main> playedCards, List<CarteVS> turnedCards)
        {
            /*
             * SOURIS
             * Si je peux gagner car j'ai n>0 meilleures carte de toutes
             * Alors je regarde si cela vaut le coup
             * (exemple : si j'ai les n meilleures cartes du jeu, j'en joue une si la carte souris est une des n meilleures restantes)
             * Si je suis sur de perdre je joue ma pire
             * 
             * VAUTOUR
             * Si j'ai n>0 cartes meilleures que la meilleure carte d'un joueur
             * alors je regarde si cela vaut le coup
             * (exemple : si c'est un des n plus grand vautours, je joue une de mes cartes meilleures que le joueur [la plus petite] )
             * (sinon je peux établir un order sur les cartes restantes inférieures à mes "meilleures cartes que le joueur")
             */
            
            // Dans le cas ou aucune carte n'a été jouée, on ne peut appliquer de stratégie relative au cartes jouées on joue un coup "classique" 
            if (main.cartes.Count == 15)
            {
                return playInOrder(carteTournee, turnedCards);
            }

            int playOrder = Order(carteTournee,turnedCards);
            // On récupére les meilleures/pires cartes de tout les joueurs
            // On récupére également notre pire carte et notre meilleure carte, la main étant triée par ordre croissant.
            
            List<Main> playedCardsOther = otherPlayedCards(playedCards);
            CartePoints myBestCard = main.cartes.ElementAt(main.cartes.Count - 1);
            CartePoints myWorstCard = main.cartes.ElementAt(0);
            List<CartePoints> bestCardsOthers = bestCardPlayers(playedCardsOther);
            List<CartePoints> worstCardsOther = worstCardPlayers(playedCardsOther);
            // On intiialite la meilleure des meilleures et la pire des pires à l'elem 0
            CartePoints worstBestCardOthers = bestCardsOthers.ElementAt(0);
            CartePoints bestBestCardOthers = bestCardsOthers.ElementAt(0);

            foreach (CartePoints card in bestCardsOthers)
            {
                // On récupére la meilleure des meilleures cartes des joueurs
                if (card.getVal() > bestBestCardOthers.getVal())
                {
                    bestBestCardOthers = card;
                }
                // On récupére la pire des meilleure carte des joueurs
                else if (card.getVal() < worstBestCardOthers.getVal())
                {
                    worstBestCardOthers = card;
                }


            }

         
            CartePoints worstWorstCardOthers = worstCardsOther.ElementAt(worstCardsOther.Count-1);
            foreach (CartePoints card in worstCardsOther)
            {
                // On récupére la pire des pires carte des joueurs
                if (card.getVal() < worstWorstCardOthers.getVal())
                {
                    worstWorstCardOthers = card;
                }
            }
         
            switch(carteTournee.isSouris())
            {
                // Dans le cas ou une carte souris est jouée
                case true:
                              
                    // On ne peux pas gagner la manche si notre meilleure carte à une moins grande valeur que la pire cartes des joueurs 
                    bool iCantWinMouse = myBestCard.getVal() < worstWorstCardOthers.getVal();
                    
                    if(iCantWinMouse)
                    {
                        // Dans ce cas, on se débarasse de notre plus petite carte pour conserver les plus importantes.
                        return playCarte(main.cartes.ElementAt(0).getVal());
                    }
                    else 
                    {
                        // Sinon si l'on a des chances de gagner
                        List<CartePoints> cardBetterThan = listCardBetterThan(bestBestCardOthers);
                        int nbCardMoreProritaryThan = cardVSBetterThan(carteTournee);
                        // On regarde le nombre de carte plus importante que la carte VS actuellement retourné
                        // On compare au nombre de carte de notre amin meilleure que la meilleure des cartes adverses  
                        // Si on posséde plus de carte, on peux jouer une carte ayant une moins grande valeur, on sera quand même sur de récupérer la carte
                        // Sinon on applique la stratégie de play in order
                        if((cardBetterThan.Count>=nbCardMoreProritaryThan) && (cardBetterThan.Count != 0))
                        {
                            myPlayedCards.cartes.Add(cardBetterThan.ElementAt(0));
                             return playCarte(cardBetterThan.ElementAt(0).getVal());
                        }
                        else
                        {
                            return playInOrder(carteTournee,turnedCards);
                        }
                    }


                case false:
                   

                    bool iCantAvoidVultur = myBestCard.getVal() < worstWorstCardOthers.getVal();

                    if(iCantAvoidVultur)
                    {
                        return playCarte(main.cartes.ElementAt(0).getVal());
                    }
                    else 
                    {
                        List<CartePoints> cardBetterThan = listCardBetterThan(worstBestCardOthers);
                        int nbCardMorePriorityThan = cardVSBetterThan(carteTournee);
                        if((cardBetterThan.Count>=nbCardMorePriorityThan) && (cardBetterThan.Count != 0))
                        {
                            myPlayedCards.cartes.Add(cardBetterThan.ElementAt(0));
                            return playCarte(cardBetterThan.ElementAt(0).getVal());
                        }
                        else
                        {
                            return playInOrder(carteTournee, turnedCards);
                        }                        
                    }
                            

                default:
                    break;
 
            }
            /* ATTENTION NE PAS OUBLIER LE CAS OU L'HISTORIQUE EST VIDE ! */
            return new CartePoints(-1);
            
        }
    }
}
