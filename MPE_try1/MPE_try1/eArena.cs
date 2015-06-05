using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPE_try1
{
    public class eArena
    {
        private int eArenaID;

        public int EArenaID
        {
            get { return eArenaID; }
            set { eArenaID = value; }
        }

        public eArena()
        {
            eArenaID = 0;
        }
    }
}
