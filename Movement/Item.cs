using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Movement
{
    internal class Item:Character
    {
        int points;
        private static Timer powerUpTimer;

        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        public void PowerUp(List<Ghost> characters)
        {
            foreach (var chara in characters)
            {
                chara.Weakness = true;
                chara.Symbol = '!';
            }
            SetTimer();

            powerUpTimer.Stop();
            powerUpTimer.Dispose();

            foreach (var chara in characters)
            {
                chara.Weakness = false;
                chara.Symbol = '†';
            }
        }

        private static void SetTimer()
        {
            powerUpTimer = new System.Timers.Timer(10000);
            powerUpTimer.Elapsed += OnTimedEvent;
            powerUpTimer.AutoReset = true;
            powerUpTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("checking if it reaches here");
        }
    }
}
