using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Movement
{
    //Class that represents a big dot in the map, that inherits from the Item class
    internal class BigDot:Item
    {
        //Constructor for the class, assings the symbol, points, type and coordinates
        public BigDot(int x, int y)
        {
            X = x;
            Y = y;
            Symbol = 'O';
            Points = 50;
            Type = "bigDot";
        }
    }
}
