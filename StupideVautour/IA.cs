using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupideVautour
{
    class IA : Joueur
    {

        int lvDif;
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
            this.lvDif = 0;
            Console.WriteLine("Joueur " + name + " a rejoint la partie.");
        }

        public void setDifficulty(int i)
        {
            this.lvDif = i;
        }
        public override CartePoints play()
        {
            switch (lvDif)
            {
                case 0:
                    return new CartePoints(10);
                case 1:
                    return PlayFacile();
                default:
                    return null;
            }
           
        }

        private CartePoints PlayFacile()
        {
            CartePoints temp = new CartePoints(0);
            Random i = new Random();
            int val = (int)i.Next(main.Count());
            temp = main.ElementAt(val);
            main.RemoveAt(val);
            return temp;
        }
    }

    
}
