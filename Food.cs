using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Food : GameObject
    {
        private Random rand = new Random();
        public string color;

        public Food(string color)
        {
            appearance = '#';
            this.color = color;
            this.position.X = rand.Next(2, 58);
            this.position.Y = rand.Next(2, 24);

        }

        public override void Update()
        {

        }
    }
}
