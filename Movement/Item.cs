using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movement
{
    internal class Item:Character
    {
        int points;

        public int Points
        {
            get { return points; }
            set { points = value; }
        }
    }
}
