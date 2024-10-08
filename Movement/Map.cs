﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Movement
{
    //Class that represents the map of the game
    internal class Map
    {
        //Visual representation of the game's map
        public char[,] map = {{'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
{'#',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
{'#',' ','#',' ','#','#','#','#','#','#','#','#',' ','#',' ',' ',' ',' ',' ',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
{'#',' ',' ',' ','#','#','#','#','#','#','#','#',' ','#','#','#','#','#','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
{'#',' ','#',' ','#',' ','#',' ','#','#','#','#',' ','#',' ','#','#','#','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
{'#',' ','#',' ','#',' ','#',' ','#',' ',' ',' ',' ',' ',' ','#',' ',' ',' ',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
{'#',' ','#',' ','#',' ','#',' ','#',' ','#','#','#',' ','#',' ',' ','#','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#','#','#','#','#','#','#','#','#','#','#',' ','#','#','#','#','#','#','#','#',' ','#'},
{'#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#','#',' ',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',' ','#'},
{'#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ',' ',' ',' ',' ',' ',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#','#','#','#','#','#','#','#','#',' ','#','#','#','#','#','#',' ','#',' ','#'},
{'#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#','#','#',' ','#',' ','#',' ','#',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',' ','#'},
{'#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#','#','#',' ','#',' ','#','#','#','#','#','#','#','#','#','#','#',' ','#','#','#','#','#','#',' ','#'},
{'#',' ',' ',' ','#',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ','#',' ',' ',' ','#',' ','#',' ',' ',' ','#',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
{'#',' ','#','#','#','#','#',' ','#',' ','#','#',' ','#',' ','#','#','#','#',' ','#','#','#',' ','#',' ','#','#','#','#','#',' ','#',' ','#','#','#',' ','#',' ','#',' ','#',' ','#',' ','#','#','#','#','#','#',' ','#'},
{'#',' ','#',' ',' ',' ',' ',' ','#',' ',' ','#',' ','#',' ','#',' ','#',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',' ','#',' ',' ',' ','#',' ','#',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
{'#',' ','#',' ','#',' ','#',' ','#',' ','#','#',' ','#',' ','#',' ','#',' ','#','#',' ','#',' ','#',' ','#','#','#','#','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#','#','#','#','#','#',' ','#',' ','#'},
{'#',' ','#',' ','#',' ','#',' ','#',' ',' ','#',' ','#',' ','#',' ','#',' ','#','#',' ','#',' ','#',' ',' ',' ',' ',' ',' ',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ',' ',' ',' ','#',' ','#',' ','#'},
{'#',' ','#',' ','#',' ','#',' ','#','#',' ','#',' ','#',' ','#',' ','#',' ','#','#',' ','#',' ','#',' ',' ',' ',' ',' ',' ',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#','#',' ','#',' ','#',' ','#'},
{'#',' ','#',' ','#',' ','#',' ',' ',' ',' ',' ',' ','#',' ','#',' ',' ',' ',' ',' ',' ','#',' ','#',' ','#','#','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ',' ',' ','#',' ','#',' ','#','#',' ','#',' ','#',' ','#'},
{'#',' ','#',' ','#',' ','#','#','#','#','#','#','#','#',' ','#',' ','#',' ','#','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#','#',' ','#','#',' ','#',' ','#','#',' ','#',' ','#',' ','#'},
{'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',' ','#',' ','#','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ',' ',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ','#',' ','#'},
{'#',' ','#','#','#',' ','#',' ','#','#','#','#',' ','#',' ','#',' ','#',' ','#','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#','#','#',' ','#','#','#',' ','#',' ','#','#','#','#','#','#',' ','#'},
{'#',' ','#',' ','#',' ','#',' ',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
{'#',' ','#',' ','#',' ','#',' ',' ','#',' ','#',' ',' ',' ',' ',' ','#',' ','#','#',' ',' ',' ','#',' ',' ',' ','#',' ','#',' ','#',' ','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#',' ','#'},
{'#',' ','#',' ','#',' ','#',' ',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
{'#',' ','#',' ','#',' ','#',' ','#','#',' ','#',' ','#',' ','#',' ','#',' ','#','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#',' ','#'},
{'#',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
{'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'}
        };

        //Array that contains the items present in the map
        public Item[,] itemMap;

        //Variable that checks if the map is being printed
        public bool mapPrinting = false;

        //Function that prints the map in the console with set characters and the current score
        public void PrintMap(List<Character> characters, int score)
        {
            //Checks if the process of printing the map is running, if it's not, it returns.
            if (mapPrinting != false)
            {
                return;
            }
            mapPrinting = true;

            //Double loop going by each coordinate to print the elements from the map
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    //This foreach loop checks the amount of characters and prints them in their respective color
                    foreach (Character character in characters)
                    {
                        if (character.X == j && character.Y == i)
                        {
                            if (character.Type == "pacman")
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            }
                                
                            else if (character.Type == "ghost")
                                Console.ForegroundColor = ConsoleColor.Red;
                        }
                    }
                    //Here we print the map in the console, to stop the stutering, we only change the elements in the specific coordinates
                    Console.SetCursorPosition(j, i);
                    Console.Write(map[i, j]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("\n");
            }
            //Printing of the score as last printing step
            Console.WriteLine("Score: {0}", score);
            mapPrinting = false;
        }

        //Function to set the position of an item or character in the map and assign it's symbol
        public void SetPosition(int y, int x, char symbol)
        {
            map[y, x] = symbol;
        }

        //Funciton to initialize the Item array of the same size as the map to track their coordinates
        public void InitializeItemMap()
        {
            itemMap = new Item[map.GetLength(0), map.GetLength(1)];
        }
    }
}
