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
            String temp = "";
            for (int i = 0; i < main.count();i++)
            {
                temp = temp + main.cartes.ElementAt(i).getVal() + "   ";
            }
            Program.print(temp+"\n");
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
            if (main.remove(carteAJouer) == carteAJouer.getVal())
            { return carteAJouer; }
            else { Program.print("ERREUR : TENTATIVE DE SUPPRESSION DE CARTE INEXISTANTE DANS Joueur.playCarte(int val) !"); return null; }
        }

        public void giveCard(CarteVS carte)
        {
            pot.Add(carte);
            points += carte.getVal();
        }
    }
}
