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
        public static int score = 0;
        public static Ghost myGhost;
        public static PacMan myPacMan;
        public static int ghostEaten = 0;

        static void Main(string[] args)
        {
            ActivateInterval();
            PacMan pacman = new PacMan();
            characters.Add(pacman);
            myPacMan = pacman;

            Ghost ghost = new Ghost(1,1);
            characters.Add(ghost);
            myGhost = ghost;


            pacman.Spawn(level1Map);
            ghost.Spawn(level1Map);

            level1Map.InitializeItemMap();
            for (int i = 0; i < level1Map.itemMap.GetLength(0); i++) 
            { 
                for (int j = 0; j < level1Map.itemMap.GetLength(1); j++)
                {
                    BaseDot dotCell = new BaseDot();
                    if (level1Map.map[i,j] != '#' && level1Map.map[i, j] != '†' && level1Map.map[i, j] != '■')
                    {
                        level1Map.map[i, j] = dotCell.Symbol;
                        level1Map.itemMap[i, j] = dotCell;
                    }
                }
            }

            level1Map.PrintMap(characters);

            int[][] coorArr = new int[2][];
            coorArr[0] = new int[] { 1, 10 };
            coorArr[1] = new int[] { 5, 10 };

            myGhost.SetRoad(level1Map, coorArr);

            bool playing = true;
            while (playing)
            {
                pacman.MoveWithInput(level1Map);
                if (level1Map.itemMap[pacman.Y, pacman.X] != null && level1Map.itemMap[pacman.Y, pacman.X].Type == "dot")
                {
                    score += level1Map.itemMap[pacman.Y, pacman.X].Points;
                    level1Map.itemMap[pacman.Y, pacman.X] = null;
                }
                level1Map.PrintMap(characters);
                Console.WriteLine("Score: {0}", score);
                checkGameOver();
            }
        }

        private static void checkGameOver()
        {
            if(myGhost.X == myPacMan.X && myGhost.Y == myPacMan.Y && myGhost.Weakness != false)
            {
                gameOver();
            }
            else if(myGhost.X == myPacMan.X && myGhost.Y == myPacMan.Y && myGhost.Weakness == true)
            {
                switch(ghostEaten)
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
        }

        private static void gameOver()
        {
            Console.Clear();
            stopInterval();
            Console.WriteLine("GameOver");
        }

        private static void ActivateInterval()
        {
            myTimer.Elapsed += interval;
            myTimer.Enabled = true;
            myTimer.AutoReset = true;
            myTimer.Start();
        }

        private static void stopInterval() {
            myTimer.Enabled = false;
        }

        private static void interval(object sender, ElapsedEventArgs e)
        {
            //Console.Write(count++);
            count++;
            if (count % 2 == 0)
            {
                myGhost.Movement(level1Map);
                level1Map.PrintMap(characters);
                checkGameOver();
            }
        }
    }

}
