using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movement
{
    internal class Item:Character
    {
        int points;

        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        public async void PowerUp(List<Ghost> characters)
        {
            foreach (var chara in characters)
            {
                chara.Weakness = true;
                chara.Symbol = '!';
                
            }

            await Task.Delay(10000);

            foreach (var chara in characters)
            {
                chara.Weakness = false;
                chara.Symbol = '†';
            }
        }
    }
}
