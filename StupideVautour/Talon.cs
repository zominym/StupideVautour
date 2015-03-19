using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupideVautour
{
    class Talon
    {
        protected int nbCartes = 15;
        List<CarteVS> cartes = new List<CarteVS>();
        public Talon()
        {
            for (int i = -5; i <= -1; i++)
            {
                cartes.Add(new CarteVS(i));
            }
            for (int i = 1; i <=10 ; i++)
            {
                cartes.Add(new CarteVS(i));
            }
        }
        public CarteVS tireCarte()
        {
            Random i = new Random();
            int val = (int)i.Next(nbCartes);
            return cartes.ElementAt(val);
            nbCartes--;
        }
    }
}
