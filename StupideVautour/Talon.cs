using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupideVautour
{
    class Talon
    {
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
            CarteVS temp = new CarteVS(0);
            Random i = new Random();
            int val = (int)i.Next(cartes.Count());
            temp = cartes.ElementAt(val);
            cartes.RemoveAt(val);
            return temp;
        }
    }
}
