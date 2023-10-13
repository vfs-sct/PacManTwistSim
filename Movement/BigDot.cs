using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Movement
{
    internal class BigDot:Item
    {

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
