﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupideVautour
{
    class IA : Joueur
    {

        int lvDif;
        CarteVS carteTalon = null;
        public IA(int i)
        :base(i)
        {
            switch (ID)
            {
                case 1: name = "BOT_Alan"; break;
                case 2: name = "BOT_Bert"; break;
                case 3: name = "BOT_Chen"; break;
                case 4: name = "BOT_Dean"; break;
                default: name = "BOT_ERROR"; break;
            }
            Console.WriteLine("Ordinateur " + name + " a rejoint la partie.");
        }
        public override CartePoints play()
        {
            CartePoints carteAJouee = main.ElementAt(10);
            main.RemoveAt(10);
            carteJouees.Add(carteAJouee);
            return carteAJouee;
        }
    }

    
}
