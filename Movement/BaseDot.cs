using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movement
{
    //Class that represents a small dot in the map, that inherits from the Item class
    internal class BaseDot:Item
    {
        //Constructor for the class, assings the symbol, points and type
        public BaseDot() 
        {
            Symbol = '·';
            Points = 10;
            Type = "dot";
        }
    }
}
