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

        public virtual CartePoints play(CarteVS c, List<Main> playedCards, List<CarteVS> turnedCards)
        {
            Console.WriteLine("ERROR : Joueur.play() is called, should be Humainlay() or IA.play()");
            return null;
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
        

        /// <summary>
        /// Affiche la main d'un joueur
        /// </summary>
        public void afficheMain()
        {
            String temp = "";
            for (int i = 0; i < main.count();i++)
            {
                temp = temp + main.cartes.ElementAt(i).getVal() + "   ";
            }
            Program.print(temp+"\n");
        }


        /// <summary>
        /// Joue une carte depuis la mian d'un joueur en vérifiant si le joueur la posséde
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public CartePoints playCarte(int val)
        {
            CartePoints carteAJouer = new CartePoints(val);
            if (main.remove(carteAJouer) == carteAJouer.getVal())
            { return carteAJouer; }
            else { throw new System.ArgumentException("Tentative de suppression d'une carte non présente dans le jeu"); }
        }


        /// <summary>
        /// Ajoute une carte VS aux cartes VS récupérées par le joueur
        /// </summary>
        /// <param name="carte"></param>
        public void giveCard(CarteVS carte)
        {
            pot.Add(carte);
            points += carte.getVal();
        }
    }
}
