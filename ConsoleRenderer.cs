using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    class ConsoleRenderer
    {
        private GameWorld world;
        //private Player player;
        //private Food food;

        public ConsoleRenderer(GameWorld gameWorld)
        {
            // TODO Konfigurera Console-fönstret enligt världens storlek
            //Kollar så att Operationssystem är windows
            if (OperatingSystem.IsWindows())
            {
                Console.SetWindowSize(gameWorld.Width + 1, gameWorld.Height + 3);
            }
            world = gameWorld;
           
        }

        public void Render()
        {
            // TODO Rendera spelvärlden (och poängräkningen)

            // Använd Console.SetCursorPosition(int x, int y) and Console.Write(char)

            foreach (var gobject in world.gameObjects)
            {
                if (gobject is Player)
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
                }

                Console.SetCursorPosition(gobject.position.X, gobject.position.Y);
                //Ändrar färg på den "dåliga maten"
                if (gobject is Food && (gobject as Food).color == "green")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(gobject.appearance);
                } else
                {
                    Console.ResetColor();
                    Console.Write(gobject.appearance);
                }
                
                
            }

            Console.SetCursorPosition(1, world.Height + 1);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Score: {world.Score}");
            Console.ResetColor();

        }

        public void RenderBlank()
        {
            Console.SetCursorPosition(world.gameObjects[0].position.X, world.gameObjects[0].position.Y);
            Console.Write(' ');

        }

        public void GameOver()
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
