using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    class ConsoleRenderer
    {
        private GameWorld world;

        public ConsoleRenderer(GameWorld gameWorld)
        {
            //Kollar så att Operationssystem är windows
            if (OperatingSystem.IsWindows())
            {
                Console.SetWindowSize(gameWorld.Width + 1, gameWorld.Height + 3);
            }
            world = gameWorld;
            Console.CursorVisible = false; // Gömmer inmatnings-blinket

        }

        public void Render()
        {
            // TODO Rendera spelvärlden (och poängräkningen)

            // Använd Console.SetCursorPosition(int x, int y) and Console.Write(char)


            foreach (var gobject in world.gameObjects) // Ser till att spelaren håller sig på spelplanen
            {


                if (gobject.position.X >= world.Width)
                {
                    gobject.position.X = 1;
                }
                else if (gobject.position.Y >= world.Height)
                {
                    gobject.position.Y = 1;
                }
                else if (gobject.position.X <= 0)
                {
                    gobject.position.X = world.Width;
                }
                else if (gobject.position.Y <= 0)
                {
                    gobject.position.Y = world.Height;
                }


                if (gobject is Player)
                {
                    foreach (Position pos in (gobject as Player).snakeBody) // Skriver ut varje "ormbit" ur ormbits-listan.
                    {
                        Console.SetCursorPosition(pos.X, pos.Y);
                        Console.Write(gobject.appearance);
                    }
                }




                Console.SetCursorPosition(gobject.position.X, gobject.position.Y);
                //Ändrar färg på den "dåliga maten" och skriver ut rätt färg
                if (gobject is Food && (gobject as Food).color == "green")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(gobject.appearance);
                }
                else if (gobject is Food && (gobject as Food).color == "white")
                {
                    Console.ResetColor();
                    Console.Write((gobject as Food).appearance);
                }
            }

            Console.SetCursorPosition(1, world.Height + 1); // Poängen skrivs ut
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Score: {world.Score}");
            Console.ResetColor();

        }

        public void RenderBlank() // Lite snitsigare än Console.Clear() 
        {
            foreach (var obj in world.gameObjects)
            {
                if (obj is Player)
                {
                    foreach (Position pos in (obj as Player).snakeBody) // Ormens alla delar hanteras
                    {
                        Console.SetCursorPosition(pos.X, pos.Y);
                        Console.Write(' ');
                    }
                }
                Console.SetCursorPosition(obj.position.X, obj.position.Y);
                Console.Write(' ');
            }

        }

        public void GameOver() // Metod som anropas vid -1 poäng, game over med snyggare stylad text
        {
            if (world.Score < 0)
            {
                world.Score = 0;
            }
            Console.Clear();
            Console.SetCursorPosition(2, 2);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"

  ________                        ________                     
 /  _____/_____    _____   ____   \_____  \___  __ ___________ 
/   \  ___\__  \  /     \_/ __ \   /   |   \  \/ // __ \_  __ \
\    \_\  \/ __ \|  Y Y  \  ___/  /    |    \   /\  ___/|  | \/
 \______  (____  /__|_|  /\___  > \_______  /\_/  \___  >__|   
        \/     \/      \/     \/          \/          \/       


                Your Score: " + world.Score);
            Console.ResetColor();

        }
    }
}
