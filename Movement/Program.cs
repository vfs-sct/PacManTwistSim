using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using static System.Formats.Asn1.AsnWriter;

namespace Movement
{
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

        static void Main(string[] args)
        {
            ActivateInterval();
            PacMan pacman = new PacMan();
            characters.Add(pacman);
            myPacMan = pacman;

            asignGhost();
            pacman.Spawn(level1Map);


            level1Map.InitializeItemMap();
            for (int i = 0; i < level1Map.itemMap.GetLength(0); i++)
            {
                for (int j = 0; j < level1Map.itemMap.GetLength(1); j++)
                {
                    BaseDot dotCell = new BaseDot();
                    if (level1Map.map[i, j] != '#' && level1Map.map[i, j] != '†' && level1Map.map[i, j] != '■')
                    {
                        level1Map.map[i, j] = dotCell.Symbol;
                        level1Map.itemMap[i, j] = dotCell;
                    }
                }
            }

            CreateBigDotsAndCherry();

            level1Map.PrintMap(characters);

            //foreach (var gho in ghostArr)
            //{
            //    gho.SetRoad(level1Map);
            //}

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
                level1Map.PrintMap(characters);
                Console.WriteLine("Score: {0}", score);
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

            //int[][] ghostCoorArr2 = new int[2][];
            //ghostCoorArr2[0] = new int[] { 68, 20 };
            //ghostCoorArr2[1] = new int[] { 75, 20 };

            //Ghost ghost2 = new Ghost(68, 15, ghostCoorArr2);
            //characters.Add(ghost2);
            //ghostArr.Add(ghost2);
            //ghost2.Spawn(level1Map);

            //int[][] ghostCoorArr3 = new int[2][];
            //ghostCoorArr3[0] = new int[] { 26, 4 };
            //ghostCoorArr3[1] = new int[] { 26, 2 };

            //Ghost ghost3 = new Ghost(34, 4, ghostCoorArr3);
            //characters.Add(ghost3);
            //ghostArr.Add(ghost3);
            //ghost3.Spawn(level1Map);
        }

        private static void checkGameOver()
        {
            foreach (var ghost in ghostArr)
            {
                if (ghost.X == myPacMan.X && ghost.Y == myPacMan.Y && ghost.Weakness == false)
                {
                    playing = false;
                    gameOver("GameOver");
                }
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
                }else if (score >= 20000)
                {
                    gameOver("Victory");
                }
            }
        }

        private static void gameOver(string context)
        {
            Console.Clear();
            stopInterval();
            Console.WriteLine(context);
        }

        private static void ActivateInterval()
        {
            myTimer.Elapsed += interval;
            myTimer.Enabled = true;
            myTimer.AutoReset = true;
            myTimer.Start();
        }

        private static void stopInterval()
        {
            myTimer.Enabled = false;
        }

        private static void interval(object sender, ElapsedEventArgs e)
        {
            //Console.Write(count++);
            //count++;
            //if (count % 2 == 0)
            //{
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

            level1Map.PrintMap(characters);
            checkGameOver();
            //}
        }

        private static void CreateBigDotsAndCherry()
        {
            int amountOfBigDots = 4;
            Random rdm = new Random();
            while (amountOfBigDots >= 0)
            {
                int randomX = rdm.Next(1, level1Map.map.GetLength(1) - 1);
                int randomY = rdm.Next(1, level1Map.map.GetLength(0) - 1);

                BigDot bigDotCell = new BigDot(randomX, randomY);
                level1Map.itemMap[randomY, randomX] = bigDotCell;
                bigDotCell.Spawn(level1Map);
                amountOfBigDots--;
            }

            int randomCherryX = rdm.Next(1, level1Map.map.GetLength(1) - 1);
            int randomCherryY = rdm.Next(1, level1Map.map.GetLength(0) - 1);
            Cherry randomCherry = new Cherry(randomCherryX, randomCherryY);
            level1Map.itemMap[randomCherryY, randomCherryX] = randomCherry;
            randomCherry.Spawn(level1Map);
        }
    }

}

