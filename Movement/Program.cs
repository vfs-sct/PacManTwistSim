using System;
using System.Collections.Generic;

namespace Movement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Map level1Map = new Map();
            List<Character> characters = new List<Character>();

            PacMan pacman = new PacMan();
            characters.Add(pacman);

            pacman.Spawn(level1Map);
            level1Map.PrintMap(characters);
            bool playing = true;
            while (playing)
            {
                pacman.MoveWithInput(level1Map);
                level1Map.PrintMap(characters);
            }

        }
    }
}
