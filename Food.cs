using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Food : GameObject
    {
        private Random rand = new Random(); // randomizer för mat
        public string color;

        string[] directions = { "RIGHT", "LEFT", "UP", "DOWN" }; // variabler för "svår" mat, vilket håll den ska smita

        private bool timetoMove = true; 
        public Food(string color) // Konstruktor med indata för färgen på maten
        {
            appearance = '#';
            this.color = color;
            this.position.X = rand.Next(2, 58); // Randomizer för X, värden inom spelvärldens storlek
            this.position.Y = rand.Next(2, 24); // Samma för Y

        }

        public override void Update()
        {

        }

        public override void UpdateHard() // Matens svårare läge
        {
            
            if (timetoMove) // Om det är dags att röra sig, rör den sig
            {
                int move = rand.Next(0, 4); // Välj håll och hoppa en bit
                if (directions[move] == "RIGHT")
                {
                    position.X++;
                }
                else if (directions[move] == "LEFT")
                {
                    position.X--;
                }
                else if (directions[move] == "UP")
                {
                    position.Y--;
                }
                else if (directions[move] == "DOWN")
                {
                    position.Y++;
                }

                timetoMove = false; // Hoppar varannan
            } else
            {
                timetoMove = true;
            }
        }
    }
}
