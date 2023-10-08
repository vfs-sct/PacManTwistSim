using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movement
{
    internal class Character
    {
        int x, y;
        char symbol;
        string type;

        public void Spawn(Map levelMap)
        {
            //Console.Clear();
            levelMap.SetPosition(Y, X, symbol);
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public char Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }
    }
}
