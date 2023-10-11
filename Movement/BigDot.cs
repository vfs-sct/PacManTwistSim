using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Movement
{
    internal class BigDot:Item
    {

        public BigDot()
        {
            Symbol = '&';
            Points = 50;
            Type = "bigDot";
        }
    }
}
