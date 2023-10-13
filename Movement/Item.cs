using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movement
{
    //Class that represents an item in the map, that inherits from the Character class
    internal class Item:Character
    {
        //Points that the item can give to the player when obtained
        int points;

        //Getter and Setter for the points
        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        //Power up method that activates when the Pacman obtains a big dot, applies the weakness status to the ghosts and changes their symbol for 10 seconds
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
