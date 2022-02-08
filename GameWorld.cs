using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    class GameWorld
    {
        // public int Height { get; set; }
        public int Height { get; }
        public int Width { get; }
        public int Score { get; set; }

        public List<GameObject> gameObjects = new List<GameObject>();
        

        public GameWorld(int w, int h)
        {
            Height = h;
            Width = w;
            Score = 0;
            
        }

        public void Update()
        {
            // TODO
            
            foreach (var gameobject in gameObjects.ToList())
            {
                gameobject.Update();
        
                if (gameobject is Player)
                {
                    foreach (var obj in gameObjects.ToList())
                    {

                        if (obj is Food)
                        {
                            if(gameobject.position.X == obj.position.X && gameobject.position.Y == obj.position.Y)
                            {
                                if ((obj as Food).color == "green")
                                {
                                    Score--;
                                    gameObjects.Remove(obj);
                                    Food badfood = new Food("green");
                                    gameObjects.Add(badfood);
                                }
                                else
                                {
                                    Score++;
                                    gameObjects.Remove(obj);
                                    Food food = new Food("white");
                                    gameObjects.Add(food);
                                }
                            }
                        } 
                        
                    }
                } 

            }
        }
    }
}
