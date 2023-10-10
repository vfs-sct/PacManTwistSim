using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movement
{
    internal class BigDot:Item
    {

        public BigDot()
        {
            Symbol = '&';
            Points = 50;
            Type = "dot";
        }

        

        public void PowerUp(List<Character> characters)
        {
            foreach (var chara in characters)
            {
                if (chara.Type == "ghost")
                {
                    Ghost gChara = (Ghost) chara;
                    gChara.Weakness = true;
                }
            }


        }
    }
}
