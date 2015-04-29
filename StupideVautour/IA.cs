using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupideVautour
{
    class IA : Joueur
    {

        int diff;
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

        private CartePoints playRandom()
        {
            CartePoints temp = new CartePoints(0);
            Random i = new Random();
            int pos = (int)i.Next(main.count());
            temp = main.cartes.ElementAt(pos);
            main.cartes.RemoveAt(pos);
            return temp;
        }




        /* Order : Chaque carte Vautour/Souris a une importance,
         * il retournne la carte à points qui a la même importance
         * (souris 10 et carte 15 ont la même importance [maximale]
         * Cependant l'ordre n'est pas croissant car il vaut mieux éviter
         * une carte vautour -5 que ramasser une carte souris 4
         * On rajoute un aléatoire si l'on est en début de partie,
         * un aléatoire plus léger en milieu de partie,
         * et aucun aléatoire en fin de partie.
         */
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
                }
                if (Math.Abs(c.getVal()) >= Math.Abs(carte.getVal()))
                {
                    pos2--;
                }
            }
            int pos = pos1;
            pos2++;
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
            int posBis = 0;
            if (main.cartes.Count() > 10) { posBis = pos + rand.Next(-2, 2); } // Si on est en début de partie, grand aléatoire
            if (main.cartes.Count() > 5 && main.cartes.Count() <= 10) { posBis = pos + rand.Next(-1, 1); } // Si on est en milieu de partie, petit aléatoire
            
            if(posBis < 0) { posBis = 0; }
            if(posBis >= main.cartes.Count()) { posBis = main.cartes.Count() - 1; }

            int val = main.cartes.ElementAt(pos).getVal();
            int valBis = main.cartes.ElementAt(posBis).getVal();
            if (Math.Abs(val - valBis) <= 3) { pos = posBis; } // Si la valeur de la carte non aléatoire est trop éloignée de celle de la carte aléatoire, on ne la retient pas

            return (main.cartes.ElementAt(pos).getVal());
        }


        private CartePoints playInOrder(CarteVS c, List<CarteVS> hist)
        {
            return playCarte(Order(c,hist));
        }

        private CartePoints playMoitieInf()
        {
            Random i = new Random();
            int val = (int)i.Next((main.count() / 2));
            return playCarte(val);
        }

        private  List<CartePoints> bestCardPlayers(List<Main> listPlayedCards)
        {
            List<CartePoints> bestCardPlayers = new List<CartePoints>();
            foreach(Main mainPlayer in listPlayedCards)
            {               
                for(int i = 15;i >0;i--)
                {
                    if(!mainPlayer.contient(new CartePoints(i)))
                    {
                        bestCardPlayers.Add(new CartePoints(i));
                        break;
                    }
                }
            }
            return bestCardPlayers;
        }

        private List<CartePoints> worstCardPlayers(List<Main> playedCards)
        {
            List<CartePoints> worstCardPlayers = new List<CartePoints>();
            foreach (Main mainPlayer in playedCards)
            {
                for (int i = 1; i < 16; i++)
                {
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
        * - a-t-elle la meilleure carte de tous les joueurs ? Dans le cas d'une carte souris retournée
        * - a-t-elle une carte meilleure que la meilleure d'un joueur ? Dans le cas d'une carte vautour retournée
        * Suit la même règle d'importance que playInOrder sinon.
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
            
            
            if (main.cartes.Count == 15)
            {
                return playInOrder(carteTournee, turnedCards);
            }

            int playOrder = Order(carteTournee,turnedCards);
            List<Main> playedCardsOther = otherPlayedCards(playedCards);
            CartePoints myBestCard = main.cartes.ElementAt(main.cartes.Count - 1);
            CartePoints myWorstCard = main.cartes.ElementAt(0);
            List<CartePoints> bestCardsOthers = bestCardPlayers(playedCardsOther);
           
            CartePoints worstBestCardOthers = bestCardsOthers.ElementAt(0);
            CartePoints bestBestCardOthers = bestCardsOthers.ElementAt(0);

            foreach (CartePoints card in bestCardsOthers)
            {
                if (card.getVal() > bestBestCardOthers.getVal())
                {
                    bestBestCardOthers = card;
                }
                else if (card.getVal() < worstBestCardOthers.getVal())
                {
                    worstBestCardOthers = card;
                }


            }


            List<CartePoints> worstCardsOther = worstCardPlayers(playedCardsOther);
            CartePoints worstWorstCardOthers = worstCardsOther.ElementAt(0);
            foreach (CartePoints card in worstCardsOther)
            {
                if (card.getVal() < worstWorstCardOthers.getVal())
                {
                    worstWorstCardOthers = card;
                }
            }
         
            switch(carteTournee.isSouris())
            {
                case true:
                              
                   
                    bool iCantWinMouse = myBestCard.getVal() < worstWorstCardOthers.getVal();
                    
                    if(iCantWinMouse)
                    {
                        return playCarte(main.cartes.ElementAt(0).getVal());
                    }
                    else 
                    {
                        List<CartePoints> cardBetterThan = listCardBetterThan(bestBestCardOthers);
                        int nbCardMoreProritaryThan = cardVSBetterThan(carteTournee);
                        if((cardBetterThan.Count>=nbCardMoreProritaryThan) && (cardBetterThan.Count != 0))
                        {
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
