using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

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

        static void Main(string[] args)
        {
            ActivateInterval();

            PacMan pacman = new PacMan();
            characters.Add(pacman);
            myPacMan = pacman;

            asignGhost();
            pacman.Spawn(level1Map);

            level1Map.PrintMap(characters);

            foreach (var gho in ghostArr)
            {
                gho.SetRoad(level1Map);
            }

            bool playing = true;
            while (playing)
            {
                pacman.MoveWithInput(level1Map);
                level1Map.PrintMap(characters);
                checkGameOver();
            }
        }

        private static void asignGhost()
        {
            int[][] ghostCoorArr1 = new int[2][];
            ghostCoorArr1[0] = new int[] { 1, 10 };
            ghostCoorArr1[1] = new int[] { 5, 10 };

            Ghost ghost = new Ghost(1, 1, ghostCoorArr1);
            characters.Add(ghost);
            ghostArr.Add(ghost);
            ghost.Spawn(level1Map);

            int[][] ghostCoorArr2 = new int[2][];
            ghostCoorArr2[0] = new int[] { 68, 20 };
            ghostCoorArr2[1] = new int[] { 75, 20 };

            Ghost ghost2 = new Ghost(68, 15, ghostCoorArr2);
            characters.Add(ghost2);
            ghostArr.Add(ghost2);
            ghost2.Spawn(level1Map);

            int[][] ghostCoorArr3 = new int[2][];
            ghostCoorArr3[0] = new int[] { 68, 20 };
            ghostCoorArr3[1] = new int[] { 68, 15 };

            Ghost ghost3 = new Ghost(75, 20, ghostCoorArr3);
            characters.Add(ghost3);
            ghostArr.Add(ghost3);
            ghost3.Spawn(level1Map);
        }

        private static void checkGameOver()
        {
            foreach (var ghost in ghostArr)
            {
                if (ghost.X == myPacMan.X && ghost.Y == myPacMan.Y)
                {
                    gameOver();
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
                foreach (var ghost in ghostArr)
                {
                    ghost.Movement(level1Map);
                }
                level1Map.PrintMap(characters);
                checkGameOver();
            }
        }
    }

}
