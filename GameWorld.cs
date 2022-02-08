using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    class GameWorld
    {
        public int Height { get; }
        public int Width { get; }
        public int Score { get; set; }

        public List<GameObject> gameObjects = new List<GameObject>();
        

        public GameWorld(int w, int h) // Skapa en värld, storlek som input
        {
            Height = h;
            Width = w;
            Score = 0;
            
        }

        public void Update()
        {
            // TODO
            
            foreach (var gameobject in gameObjects.ToList()) // Alla GameObject i listan gås igenom, deras Update() anropas. 
            {
                gameobject.Update(); // Alla gameobjects updates.

                CheckPositionFood(gameobject);
            }
        }

        public void UpdateHard()
        {
            foreach (var gameobject in gameObjects.ToList()) // Alla GameObject i listan gås igenom, deras UpdateHard() anropas. 
            {
                gameobject.UpdateHard(); // Alla gameobjects updates.

                CheckPositionFood(gameobject);
            }
        }

        public void CheckPositionFood(GameObject objectItem)
        {
            if (objectItem is Player)
            {
                foreach (var obj in gameObjects.ToList())
                {

                    if (obj is Food)
                    {
                        if (objectItem.position.X == obj.position.X && objectItem.position.Y == obj.position.Y) // Jämför positionerna
                        {
                            if ((obj as Food).color == "green") // Spelaren åt en "green" aka dålig mat
                            {
                                Score--;                        // Minuspoäng
                                gameObjects.Remove(obj);        // Maten som åts försvinner ur listan
                                (objectItem as Player).snakeBody.RemoveAt(0); //Ormen blir mindre
                                Food badfood = new Food("green"); // Och en ny skapas och läggs till
                                gameObjects.Add(badfood);

                            }
                            else
                            {
                                Score++;                        // Pluspoäng, spelaren åt dvs en "white" aka bra mat som försvinner ur listan
                                gameObjects.Remove(obj);
                                (objectItem as Player).snakeBody.Add(new Position(objectItem.position.X, objectItem.position.Y)); // Ormen växer
                                Food food = new Food("white"); // Ny mat för att ersätta det som försvann
                                gameObjects.Add(food);
                                Food greenFood = new Food("green"); // En ny dålig mat också, för svårighetsgraden
                                gameObjects.Add(greenFood);
                            }
                        }
                    }
                }
            }
        }

    }
}
