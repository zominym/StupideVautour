using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupideVautour
{
    

    class Joueur
    {
        List<CarteVS> cartesRecup;
        List<CartePoints> cartesMain;
        int ID;
        string name;


        public Joueur(int id,string s)
        {
            ID = id;
            name = s;
            for (int i = 0 ; i <=15 ; i++)
            {
                cartesMain.Add(new CartePoints(i));
            }
        }

        public int Play(CartePoints carte)
        {
            return 0;
        }
    }
}
