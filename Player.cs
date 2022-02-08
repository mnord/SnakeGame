using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class Player : GameObject
    {
        public string Direction { get; set; }
        

        public Player()
        {
            this.appearance = '@';
            this.Direction = "RIGHT";
            this.position.X = 1;
            this.position.Y = 1;
            
        }
        public override void Update()
        {
            //throw new NotImplementedException();
            if (this.Direction == "RIGHT")
            {    
                    position.X++;                 
            } else if (this.Direction == "LEFT")
            {
                position.X--;
            } else if (this.Direction == "UP")
            {
                position.Y--;
            } else if (this.Direction == "DOWN")
            {
                position.Y++;
            }
        }

    }
}
