using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupideVautour
{
    class Talon
    {
        List<CarteVS> cartes;
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
    }
}
