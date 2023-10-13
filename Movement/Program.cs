using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using static System.Formats.Asn1.AsnWriter;

namespace Movement
{
    //Main class of the program where we set up the map and needed variables
    internal class Program
    {
        public static int count = 0;
        public static Timer myTimer = new Timer(1000);
        public static Map level1Map = new Map();
        public static List<Character> characters = new List<Character>();
        public static List<Ghost> ghostArr = new List<Ghost>();
        public static PacMan myPacMan;
        public static int ghostEaten = 0;
        public static int score = 0;
        public static bool playing = true;

        //Main function of the program that initializes maps, states and reads player input
        static void Main(string[] args)
        {
            ActivateInterval(); //Activates the interval for ghost movements

            //Creates teh pacman character for the board
            PacMan pacman = new PacMan();
            characters.Add(pacman);
            myPacMan = pacman;

            asignGhost(); //Creates and assign ghosts in the board

            pacman.Spawn(level1Map); //Spawns the pacman character inside the board


            level1Map.InitializeItemMap(); //Function that initializes the item map for the game

            //Double loop that changes the spaces from the map into base dots 
            for (int i = 0; i < level1Map.itemMap.GetLength(0); i++)
            {
                for (int j = 0; j < level1Map.itemMap.GetLength(1); j++)
                {
                    BaseDot dotCell = new BaseDot();
                    if (CheckSpaceAvailable(j, i))
                    {
                        level1Map.map[i, j] = dotCell.Symbol;
                        level1Map.itemMap[i, j] = dotCell;
                    }
                }
            }

            CreateBigDotsAndCherry(); //Calls the function to add Big dots and a cherry in the map

            level1Map.PrintMap(characters, score); //Prints the initial state of the map

            //foreach (var gho in ghostArr)
            //{
            //    gho.SetRoad(level1Map);
            //}

            //Game loop that checks for game over conditions and moves the pacman with user key input, while also checking if the player obtained items and adds the points/activates power ups
            while (playing)
            {
                pacman.MoveWithInput(level1Map);
                if (level1Map.itemMap[pacman.Y, pacman.X] != null && (level1Map.itemMap[pacman.Y, pacman.X].Type == "dot" || level1Map.itemMap[pacman.Y, pacman.X].Type == "cherry"))
                {
                    score += level1Map.itemMap[pacman.Y, pacman.X].Points;
                    level1Map.itemMap[pacman.Y, pacman.X] = null;
                }
                else if (level1Map.itemMap[pacman.Y, pacman.X] != null && level1Map.itemMap[pacman.Y, pacman.X].Type == "bigDot")
                {
                    score += level1Map.itemMap[pacman.Y, pacman.X].Points;
                    level1Map.itemMap[pacman.Y, pacman.X].PowerUp(ghostArr);
                    level1Map.itemMap[pacman.Y, pacman.X] = null;
                }
                level1Map.PrintMap(characters, score);
                
                checkGameOver();
            }
        }

        private static void asignGhost()
        {
            int[][] ghostCoorArr1 = new int[2][];
            ghostCoorArr1[0] = new int[] { 13, 5 };
            ghostCoorArr1[1] = new int[] { 12, 13 };

            Ghost ghost = new Ghost(15, 11, ghostCoorArr1);
            characters.Add(ghost);
            ghostArr.Add(ghost);
            ghost.Spawn(level1Map);

            int[][] ghostCoorArr2 = new int[2][];
            ghostCoorArr2[0] = new int[] { 39, 12 };
            ghostCoorArr2[1] = new int[] { 43, 19 };

            Ghost ghost2 = new Ghost(46, 9, ghostCoorArr2);
            characters.Add(ghost2);
            ghostArr.Add(ghost2);
            ghost2.Spawn(level1Map);

            //int[][] ghostCoorArr3 = new int[2][];
            //ghostCoorArr3[0] = new int[] { 26, 4 };
            //ghostCoorArr3[1] = new int[] { 26, 2 };

            //Ghost ghost3 = new Ghost(34, 4, ghostCoorArr3);
            //characters.Add(ghost3);
            //ghostArr.Add(ghost3);
            //ghost3.Spawn(level1Map);
        }

        //Function that checks the conditions for a game over
        private static void checkGameOver()
        {
            foreach (var ghost in ghostArr)
            {
                //Checks if the player touched a ghost when it's not weakened by a Big Dot
                if (ghost.X == myPacMan.X && ghost.Y == myPacMan.Y && ghost.Weakness == false)
                {
                    playing = false;
                    gameOver("GameOver");
                }
                //Add points to the score if the player touches a ghost that is in the weakened state
                else if (ghost.X == myPacMan.X && ghost.Y == myPacMan.Y && ghost.Weakness == true)
                {
                    switch (ghostEaten)
                    {
                        case 0:
                            ghostEaten++;
                            score += 200;
                            break;
                        case 1:
                            ghostEaten++;
                            score += 400;
                            break;
                        case 2:
                            ghostEaten++;
                            score += 800;
                            break;
                        case 3:
                            ghostEaten++;
                            score += 1600;
                            break;
                        default:
                            break;
                    }
                }
                //If the player achieves a certain amount of points, it wins the game
                else if (score >= 1000)
                {
                    gameOver("Victory");
                }
            }
        }

        //Function that ends the game when a condition of victory is achieved or the player dies against the ghosts
        private static void gameOver(string context)
        {
            Console.Clear();
            stopInterval();
            Console.WriteLine(context);
        }

        //Function to activa the interval where ghost will move at a specific pace around the map 
        private static void ActivateInterval()
        {
            myTimer.Elapsed += interval;
            myTimer.Enabled = true;
            myTimer.AutoReset = true;
            myTimer.Start();
        }

        //Stops the interval when the game ends
        private static void stopInterval()
        {
            myTimer.Enabled = false;
        }

        
        private static void interval(object sender, ElapsedEventArgs e)
        {
            foreach (var ghost in ghostArr)
            {
                if (ghost.X - 2 == myPacMan.X &&
                    ((ghost.Y == myPacMan.Y && level1Map.map[ghost.Y, ghost.X - 1] != '#')
                    || (ghost.Y - 2 == myPacMan.Y && level1Map.map[ghost.Y - 1, ghost.X - 1] != '#')
                    || (ghost.Y - 1 == myPacMan.Y && level1Map.map[ghost.Y - 1, ghost.X - 1] != '#')
                    || (ghost.Y + 1 == myPacMan.Y && level1Map.map[ghost.Y + 1, ghost.X - 1] != '#')
                    || (ghost.Y + 2 == myPacMan.Y && level1Map.map[ghost.Y + 1, ghost.X - 1] != '#')
                    )
                    || ghost.X - 1 == myPacMan.X &&
                    ((ghost.Y == myPacMan.Y)
                    || (ghost.Y - 2 == myPacMan.Y && level1Map.map[ghost.Y - 1, ghost.X - 1] != '#')
                    || (ghost.Y - 1 == myPacMan.Y)
                    || (ghost.Y + 1 == myPacMan.Y)
                    || (ghost.Y + 2 == myPacMan.Y && level1Map.map[ghost.Y + 1, ghost.X - 1] != '#')
                    )
                    || ghost.X == myPacMan.X &&
                    (((ghost.Y - 2 == myPacMan.Y && level1Map.map[ghost.Y - 1, ghost.X] != '#')
                    || (ghost.Y - 1 == myPacMan.Y)
                    || (ghost.Y + 1 == myPacMan.Y)
                    || (ghost.Y + 2 == myPacMan.Y && level1Map.map[ghost.Y + 1, ghost.X] != '#')
                    )
                    || ghost.X + 1 == myPacMan.X &&
                    ((ghost.Y == myPacMan.Y)
                    || (ghost.Y - 2 == myPacMan.Y && level1Map.map[ghost.Y - 1, ghost.X - 1] != '#')
                    || (ghost.Y - 1 == myPacMan.Y)
                    || (ghost.Y + 1 == myPacMan.Y)
                    || (ghost.Y + 2 == myPacMan.Y && level1Map.map[ghost.Y + 1, ghost.X - 1] != '#')
                    )
                    || ghost.X + 2 == myPacMan.X &&
                    ((ghost.Y == myPacMan.Y && level1Map.map[ghost.Y, ghost.X + 1] != '#')
                    || (ghost.Y - 2 == myPacMan.Y && level1Map.map[ghost.Y - 1, ghost.X + 1] != '#')
                    || (ghost.Y - 1 == myPacMan.Y && level1Map.map[ghost.Y - 1, ghost.X + 1] != '#')
                    || (ghost.Y + 1 == myPacMan.Y && level1Map.map[ghost.Y + 1, ghost.X + 1] != '#')
                    || (ghost.Y + 2 == myPacMan.Y && level1Map.map[ghost.Y + 1, ghost.X + 1] != '#')
                    )))
                {
                    ghost.GhostState = Ghost.State.chasing;
                    ghost.GoTo(myPacMan.X, myPacMan.Y, level1Map);

                }
                else if (ghost.Weakness)
                {
                    ghost.GhostState = Ghost.State.runningAway;
                    ghost.RunAway(myPacMan.X, myPacMan.Y, level1Map);
                }
                else if (ghost.GhostState == Ghost.State.movingInPath)
                {
                    ghost.Movement(level1Map);
                }
                else if ((ghost.DefaultPositionX != ghost.X || ghost.DefaultPositionY != ghost.Y) && ghost.GhostState != Ghost.State.movingInPath)
                {
                    ghost.GoTo(ghost.DefaultPositionX, ghost.DefaultPositionY, level1Map);

                }
                else if (ghost.DefaultPositionX == ghost.X && ghost.DefaultPositionY == ghost.Y && ghost.GhostState != Ghost.State.movingInPath)
                {
                    ghost.GhostState = Ghost.State.movingInPath;

                }
            }

            level1Map.PrintMap(characters, score);
            checkGameOver();
            //}
        }

        //Method to add big dots and a cherry to the map
        private static void CreateBigDotsAndCherry()
        {
            //An amount of 4 Big dots will be added to the map
            int amountOfBigDots = 4;
            Random rdm = new Random();

            while (amountOfBigDots >= 0)
            {
                //Create random coordinates to place the big dots around the map
                int randomX = rdm.Next(1, level1Map.map.GetLength(1) - 1);
                int randomY = rdm.Next(1, level1Map.map.GetLength(0) - 1);

                //If the coordinates created are either a wall, ghost or pacman itself, don't add the big dot and try again
                if (CheckSpaceAvailable(randomX, randomY))
                {
                BigDot bigDotCell = new BigDot(randomX, randomY);
                level1Map.itemMap[randomY, randomX] = bigDotCell;
                bigDotCell.Spawn(level1Map);
                amountOfBigDots--;
                }
            }

            //Here we create random coordinates for the cherry, currently we try to add it to the map but if the coordinate is a wall, ghost or map, we don't add it
            int randomCherryX = rdm.Next(1, level1Map.map.GetLength(1) - 1);
            int randomCherryY = rdm.Next(1, level1Map.map.GetLength(0) - 1);
            if (CheckSpaceAvailable(randomCherryX, randomCherryY))
            {
                Cherry randomCherry = new Cherry(randomCherryX, randomCherryY);
                level1Map.itemMap[randomCherryY, randomCherryX] = randomCherry;
                randomCherry.Spawn(level1Map);
            }
        }

        //Function that checks if the selected coordinate is not a wall, pacman or ghost
        private static bool CheckSpaceAvailable(int coordinateX, int coordinateY)
        {
            if (level1Map.map[coordinateY, coordinateX] != '#' && level1Map.map[coordinateY, coordinateX] != '†' && level1Map.map[coordinateY, coordinateX] != '■')
                return true;
            else
                return false;
        }
    }

}

