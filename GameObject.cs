using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    abstract class GameObject // Huvudklassen som de andra ärver från
    {
        // TODO
        public Position position = new(0,0); // En GameObject ska alltid ha en position
        public char appearance; // Utseende för varje GameObject väljs av respektive


        public abstract void Update(); // Vanligt läge
        public abstract void UpdateHard(); // Svårt läge
    }
}
