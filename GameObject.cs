using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    abstract class GameObject
    {
        // TODO
        public Position position = new();
        public char appearance;


        public abstract void Update();
    }
}
