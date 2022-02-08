using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Position // Positions-klassen som används av det mesta
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position (int x, int y)
        {
           this.X = x;
           this.Y = y;
        }
    }
}
