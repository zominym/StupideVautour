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

        public CartePoints play(CarteVS carteTournee, List<List<CartePoints>> playedCards, List<CarteVS> turnedCards)
        {
            switch (diff)
            {
                case 0:
                    return playRandom();
                case 1:
                    return playInOrder(carteTournee.getVal());
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
            int pos = (int)i.Next(main.Count());
            temp = main.ElementAt(pos);
            main.RemoveAt(pos);
            return temp;
        }

        /* IA PlayInOrder : Chaque carte Vautour/Souris a une importance,
         * il joue la carte à points qui a la même importance
         * (souris 10 et carte 15 ont la même importance (maximale)
         * Cependant l'ordre change car il vaut mieux éviter
         * une carte vautour -5 que ramasser une carte souris 4
         */
        private CartePoints playInOrder(int valCarte)
        {
            int[] tValCarte = { 10, 9, 8, 7, 6, 5, -5, 4, -4, 3, -3, 2, -2, 1, -1 };
            for (int i = 0; i < 15; i++)
            {
                if (tValCarte[i] == valCarte) { return playCarte(15 - i); }
            }
            return playRandom();
        }

        /* IA PlayMemory : Se rappelle chaque carte jouee par chaque joueur,
         * se rappelle aussi chaque carte du talon déjà jouée
         * en déduit des informations comme :
         * - a-t-elle la meilleure carte ?
         * - a-t-elle la pire carte ?
         * Suit la même règle d'importance que playInOrder ???
         */
        private CartePoints playMemory(CarteVS carteTournee, List<List<CartePoints>> playedCards, List<CarteVS> turnedCards)
        {
            /* ATTENTION NE PAS OUBLIER LE CAS OU L'HISTORIQUE EST VIDE ! */
            return new CartePoints(10);
        }

    }
}
