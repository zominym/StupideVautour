using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StupideVautour
{
    class Main
    {
        public List<CartePoints> cartes = new List<CartePoints>();

        public Main() { }

        public Main(int n)
        {
            for (int i = 1; i <= n; i++)
            {
                add(new CartePoints(i));
            }
        }

        public void add(CartePoints c)
        {
            cartes.Add(c);
        }

        public int remove(CartePoints rem)
        {
            foreach (CartePoints c in cartes)
            {
                if (c.getVal() == rem.getVal())
                {
                    cartes.Remove(c);
                    return rem.getVal();
                }
            }
            return -1;
        }

        public int count()
        {
            return cartes.Count();
        }

        public void removeAt(int i)
        {
            cartes.RemoveAt(i);
        }

        public bool contient(CartePoints rem)
        {
            foreach (CartePoints c in cartes)
            {
                if (c.getVal() == rem.getVal())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
