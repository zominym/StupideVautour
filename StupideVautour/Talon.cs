using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupideVautour
{
    class Talon
    {
        public List<CarteVS> cartes = new List<CarteVS>();


        /// <summary>
        /// Initialise un talon contenant les cartes souris de 1 à 10 et vautour de -1 à -5         /// 
        /// </summary>
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

        public Talon(int p){}


        /// <summary>
        /// Tire une carte du talon de manière aléatoire
        /// </summary>
        /// <returns></returns>
        public CarteVS tireCarte()
        {
            CarteVS temp = new CarteVS(0);
            Random i = new Random();
            int val = (int)i.Next(cartes.Count());
            temp = cartes.ElementAt(val);
            cartes.RemoveAt(val);
            return temp;
        }

        public List<CarteVS> getCartes()
        {
            return cartes;
        }

        /// <summary>
        /// Supprime une carte du talon
        /// </summary>
        /// <param name="c"></param>
        public void remove(CarteVS c)
        {
            CarteVS toRemove = null;
            foreach (CarteVS cc in cartes)
            {
                if (cc.getVal() == c.getVal())
                {
                    toRemove = cc;
                    break;
                }
            }
            cartes.Remove(toRemove);
        }
    }
}
