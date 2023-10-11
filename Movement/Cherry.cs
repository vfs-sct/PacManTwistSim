using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movement
{
    internal class Cherry:Item
    {
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
