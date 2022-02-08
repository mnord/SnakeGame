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

        public List<Position> snakeBody; // Ju fler poäng, desto längre orm, lista över positioner.
        public Player()
        {
            this.appearance = '@'; // Spelarens utseende
            this.Direction = "RIGHT"; // Startposition
            this.position.X = 1; // Börjar på 1,1
            this.position.Y = 1;
            snakeBody = new List<Position>(); // Listan skapas av Player.
            snakeBody.Add(this.position); // Positionen läggs till i listan
        }

        //Vi valde att ha två update metoder, en vanlig och en svår
        public override void Update()
        {
            ChangePosition();
            
        }

        public override void UpdateHard ()
        {
            ChangePosition();
        }

        // Tog gemensam kod för båda update metoder
        public void ChangePosition () 
        {
            if (this.Direction == "RIGHT")  // Riktning hanteras med enkla strängar.
            {
                position.X++;
            }
            else if (this.Direction == "LEFT")
            {
                position.X--;
            }
            else if (this.Direction == "UP")
            {
                position.Y--;
            }
            else if (this.Direction == "DOWN")
            {
                position.Y++;
            }
            snakeBody.Add(new Position(position.X, position.Y)); // Lägger till en "ormbit"
            snakeBody.RemoveAt(0); // Tar bort den sista
        }
    }
}
