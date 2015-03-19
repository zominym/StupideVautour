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
        protected int points;
        protected List<CartePoints> carteJouees = new List<CartePoints>();


        public Joueur(int id)
        {
            points = 0;
            ID = id;
            for (int i = 1; i <=15 ; i++)
            {
                main.Add(new CartePoints(i));
            }
        }
 
        public string getName()
        {
            return name;
        }

        public int getPoints()
        {
            return points;
        }

        public void setPoints(int i)
        {
            this.points = i;
        }

        public List<CartePoints> getCarteJouees()
        {
            return this.carteJouees;
        }

        public int getID()
        {
            return this.ID;
        }
        public abstract CartePoints play();
        
        public void afficheMain()
        {
            Console.WriteLine("Il reste " + main.Count() + " carte(s) dans votre main :");
            for (int i = 0; i < main.Count();i++)
            {
                Console.Write(main.ElementAt(i).getVal() + "   ");
            }
            Console.WriteLine();
        }
        public bool estDansMain(int val)
        {
            foreach (CartePoints carte in main)
            {
                if (carte.getVal() == val)
                {
                    return true;
                }
            }
            return false;

        }     

        public void giveCard(CarteVS carte)
        {
            pot.Add(carte);
            points += carte.getVal();
        }

        
    }
}
