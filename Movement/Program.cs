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
        public static Ghost myGhost;
        public static PacMan myPacMan;

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

            level1Map.PrintMap(characters);

            int[][] coorArr = new int[2][];
            coorArr[0] = new int[] { 1, 10 };
            coorArr[1] = new int[] { 5, 10 };

            myGhost.SetRoad(level1Map, coorArr);

            bool playing = true;
            while (playing)
            {
                pacman.MoveWithInput(level1Map);
                level1Map.PrintMap(characters);
                checkGameOver();
            }
        }

        private static void checkGameOver()
        {
            if(myGhost.X == myPacMan.X && myGhost.Y == myPacMan.Y)
            {
                gameOver();
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
