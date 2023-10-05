using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movement
{
    internal class Ghost:Character
    {
        public Ghost(int x, int y)
        {
            X = x;
            Y = y;
            Type = "ghost";
            Symbol = '†';
        }

        public void MoveWithDirection(string direction,Map levelMap)
        {
            if (direction == "left")
            {
                if (levelMap.map[Y, X - 1] != '#')
                {
                    levelMap.map[Y, X] = ' ';
                    X--;
                }
                Spawn(levelMap);

            }
            else if (direction == "right")
            {
                if (levelMap.map[Y, X + 1] != '#')
                {
                    levelMap.map[Y, X] = ' ';
                    X++;
                }
                Spawn(levelMap);
            }
            else if (direction == "up")
            {
                if (levelMap.map[Y - 1, X] != '#')
                {
                    levelMap.map[Y, X] = ' ';
                    Y--;
                }
                Spawn(levelMap);
            }
            else if (direction == "down")
            {
                if (levelMap.map[Y + 1, X] != '#')
                {
                    levelMap.map[Y, X] = ' ';
                    Y++;
                }
                Spawn(levelMap);
            }
        }
    }
}
