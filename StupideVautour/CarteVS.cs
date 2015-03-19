using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupideVautour
{
    class CarteVS
    {
        int val;

        public CarteVS(int i)
        {
            val = i;
        }

        public bool isSouris()
        {
            return val > 0;
        }
    }
}
