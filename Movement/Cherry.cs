using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movement
{
    //Class that represents a cherry in the map, that inherits from the Item class
    internal class Cherry:Item
    {
        //Constructor for the class, assings the symbol, points, type and coordinates
        public Cherry(int x, int y) 
        {
            X = x;
            Y = y;
            Symbol = '@';
            Points = 100;
            Type = "cherry";
        }
    }
}
