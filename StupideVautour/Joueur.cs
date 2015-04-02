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
        protected Main main = new Main(15);
        protected int ID;
        protected string name;
        protected int points;


        public Joueur(int id)
        {
            points = 0;
            ID = id;
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

        public int getID()
        {
            return this.ID;
        }
        
        public void afficheMain()
        {
            Console.WriteLine("Il reste " + main.count() + " carte(s) dans votre main :");
            for (int i = 0; i < main.count();i++)
            {
                Console.Write(main.cartes.ElementAt(i).getVal() + "   ");
            }
            Console.WriteLine();
        }
        public bool estDansMain(int val)
        {
            foreach (CartePoints carte in main.cartes)
            {
                if (carte.getVal() == val)
                {
                    return true;
                }
            }
            return false;

        }     

        public CartePoints playCarte(int val)
        {
            CartePoints carteAJouer = new CartePoints(val);
            if (main.cartes.RemoveAll(delegate(CartePoints c)
            {
                if (c.getVal() == val) { return true; }
                else { return false; }
            }) == 1)
            { return carteAJouer; }
            else { Console.WriteLine("ERREUR : TENTATIVE DE SUPPRESSION DE CARTE INEXISTANTE DANS Joueur.playCarte(int val) !"); return null; }
        }

        public void giveCard(CarteVS carte)
        {
            pot.Add(carte);
            points += carte.getVal();
        }
    }
}
