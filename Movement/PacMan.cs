using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movement
{
    internal class PacMan:Character
    {
        public PacMan()
        {
            X = 2;
            Y = 2;
            Type = "pacman";
            Symbol = '■';
        }

        public void MoveWithInput(Map levelMap)
        {
            ConsoleKey input = Console.ReadKey().Key;
            if (input == ConsoleKey.LeftArrow)
            {
                if (levelMap.map[Y, X - 1] != '#')
                {
                    levelMap.map[Y, X] = ' ';
                    X--;
                }
                Spawn(levelMap);

            }
            else if (input == ConsoleKey.RightArrow)
            {
                if (levelMap.map[Y, X + 1] != '#')
                {
                    levelMap.map[Y, X] = ' ';
                    X++;
                }
                Spawn(levelMap);
            }
            else if (input == ConsoleKey.UpArrow)
            {
                if (levelMap.map[Y - 1, X] != '#')
                {
                    levelMap.map[Y, X] = ' ';
                    Y--;
                }
                Spawn(levelMap);
            }
            else if (input == ConsoleKey.DownArrow)
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
