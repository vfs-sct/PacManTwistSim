using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movement
{
    //Class that represents the character of pacman
    internal class PacMan:Character
    {
        //Constructor that assigns a default position for pacman, it's type and symbol
        public PacMan()
        {
            X = 2;
            Y = 2;
            Type = "pacman";
            Symbol = '■';
        }

        //Method that moves pacman with user input and moves the character around the map
        public void MoveWithInput(Map levelMap)
        {
            ConsoleKey input = Console.ReadKey().Key;
            if (input == ConsoleKey.LeftArrow)
            {
                if (CheckWall(Y, X -1, levelMap))
                {
                    X--;
                }
            }
            else if (input == ConsoleKey.RightArrow)
            {
                if (CheckWall(Y, X + 1, levelMap))
                {
                    X++;
                }
            }
            else if (input == ConsoleKey.UpArrow)
            {
                if (CheckWall(Y - 1, X , levelMap))
                {
                    Y--;
                }
            }
            else if (input == ConsoleKey.DownArrow)
            {
                if (CheckWall(Y + 1, X, levelMap))
                { 
                    Y++;
                }
            }
            Spawn(levelMap);
        }

        //Checks if there is a wall nearby of pacman and the direction is heading towards, preventing it from going through it
        private bool CheckWall(int coordinateX, int coordinateY, Map levelMap)
        {
            if (levelMap.map[coordinateX, coordinateY] != '#')
            {
                levelMap.map[Y, X] = ' ';
                return true;
            }
            else { return false; }
        }

    }


}
