using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupideVautour
{
    

    abstract class Joueur 
    {
        List<CarteVS> pot = new List<CarteVS>();
        List<CartePoints> main = new List<CartePoints>();
        protected int ID;
        protected string name;


        public Joueur(int id)
        {
            ID = id;
            for (int i = 0 ; i <=15 ; i++)
            {
                main.Add(new CartePoints(i));
            }
        }

        public abstract CartePoints play(List<CartePoints> carte);
    
    }
}
