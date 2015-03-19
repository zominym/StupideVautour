using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupideVautour
{
    

    abstract class Joueur 
    {
        protected List<CarteVS> pot = new List<CarteVS>();
        protected List<CartePoints> main = new List<CartePoints>();
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

        public int getPoints()
        {
            int i = 0;
            foreach (CarteVS carte in pot)
            {
                i += carte.getVal();
            }
            return i;
        }


        public abstract CartePoints play();
    
    }
}
