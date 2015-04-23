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
        CarteVS carteTalon = null;

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
            Console.WriteLine("Joueur " + name + " a rejoint la partie.");
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

        private int Order(CarteVS carte, List<CarteVS> hist)
        {
            Talon t = new Talon(0);
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
                    //Console.WriteLine("DEBUG : PlayInOrder : pos1-- car " + c.getVal() + " > " + carte.getVal());
                }
                if (Math.Abs(c.getVal()) >= Math.Abs(carte.getVal()))
                {
                    pos2--;
                    //Console.WriteLine("DEBUG : PlayInOrder : pos2-- car " + c.getVal() + " >= " + carte.getVal());
                }
            }
            int pos = pos1;
            pos2++;
            //Console.WriteLine("DEBUG : Alea entre " + pos1 + " et " + pos2);
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
            if(pos < 0)
            {
                pos = 0;
            }
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

        private CartePoints playMoitieInf()
        {
            Random i = new Random();
            int val = (int)i.Next((main.count() / 2));
            return playCarte(val);
        }

        private  List<CartePoints> bestCardPlayers(List<Main> playedCards)
        {
            List<CartePoints> bestCardPlayers = new List<CartePoints>();
            foreach(Main mainPlayer in playedCards)
            {               
                for(int i = 15;i >0;i--)
                {
                    if(mainPlayer.contient(new CartePoints(i)))
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
                CartePoints testCarte = new CartePoints(0);
                for (int i = 1; i < 16; i++)
                {
                    testCarte.setVal(i);
                    if (mainPlayer.contient(testCarte))
                    {
                        worstCardPlayers.Add(testCarte);
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
            if (playedCards.ElementAt(0).count() == 0)
            {
                return playInOrder(carteTournee,turnedCards);
            }
            int playOrder = Order(carteTournee,turnedCards);
            switch(carteTournee.isSouris())
            {
                case true:
                    List<CartePoints> bestCards = bestCardPlayers(playedCards);
                    CartePoints bestBestCard = new CartePoints(-100);
                    foreach(CartePoints card in bestCards)
                    {
                        if (card.getVal()>bestBestCard.getVal())
                        {
                            bestBestCard = card; 
                        }
                    }
                    if(playOrder>bestBestCard.getVal())
                    {
                        for(int i = (bestBestCard.getVal()+1); i<=(playOrder);i++)
                        {
                            if(this.estDansMain(i))
                            {
                                return this.playCarte(i);
                            }
                        }
                    }
                    if(carteTournee.getVal()>=5)
                    {
                        Random i = new Random();
                        int val = (int)i.Next(2);
                        if (val == 1)
                        {
                            return playCarte(playOrder);
                        }
                        else 
                        {
                            return main.cartes.ElementAt(0);
                        }

                    }
                    else
                    {
                        return main.cartes.ElementAt(0); ;
                    }
                    break;

                case false:
                    List<CartePoints> worstCards = worstCardPlayers(playedCards);
                    CartePoints worstBestCard = new CartePoints(1000);
                    foreach (CartePoints card in worstCards)
                    {
                        if(card.getVal()<worstBestCard.getVal())
                        {
                            worstBestCard = card;
                        }
                    }
                    if(playOrder>worstBestCard.getVal())
                    {
                        for(int i = (worstBestCard.getVal()+1); i<=(playOrder);i++)
                        {
                            if(this.estDansMain(i))
                            {
                                return this.playCarte(i);
                            }
                        }
                    }
                    if(carteTournee.getVal()>=5)
                    {
                        Random i = new Random();
                        int val = (int)i.Next(2);
                        if (val == 1)
                        {
                            return playCarte(playOrder);
                        }
                        else 
                        {
                            return main.cartes.ElementAt(0);
                        }

                    }
                    else
                    {
                        return main.cartes.ElementAt(0);
                    }
                    break;

                default:
                    break;
 
            }
            /* ATTENTION NE PAS OUBLIER LE CAS OU L'HISTORIQUE EST VIDE ! */
            return new CartePoints(10);
        }
    }
}
