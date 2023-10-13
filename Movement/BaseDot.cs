using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movement
{
    internal class BaseDot:Item
    {
        
        public BaseDot() 
        {
            Symbol = '·';
            Points = 10;
            Type = "dot";
        }
    }
}
