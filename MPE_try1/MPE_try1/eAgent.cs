using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPE_try1
{
    class ecoAgent
    {
        // ID Agenta
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        // ID strategii
        private int strategy_id;

        public int Strategy_id
        {
            get { return strategy_id; }
            set { strategy_id = value; }
        }
        // reputacja agenta
        private int reputation;

        public int Reputation
        {
            get { return reputation; }
            set { reputation = value; }
        }
        // pozycja na planszy
        private int[] position;

        public int[] Position
        {
            get { return position; }
            set { position = value; }
        }

    }
}
