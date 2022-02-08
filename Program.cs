using System;
using System.Threading;
using System.Media;


namespace SnakeGame
{
    class Program
    {
        /// <summary>
        /// Checks Console to see if a keyboard key has been pressed, if so returns it, otherwise NoName.
        /// </summary>
        static ConsoleKey ReadKeyIfExists() => Console.KeyAvailable ? Console.ReadKey(intercept: true).Key : ConsoleKey.NoName;

        static void Loop()
        {
            // Initialisera spelet
            const int frameRate = 5;
            GameWorld world = new GameWorld(62, 24);
            Player player = new Player();
            world.gameObjects.Add(player);
            Food food = new Food("white");
            world.gameObjects.Add(food);
            Food badfood = new Food("green");
            world.gameObjects.Add(badfood);
            ConsoleRenderer renderer = new ConsoleRenderer(world);

            // TODO Skapa spelare och andra objekt etc. genom korrekta anrop till vår GameWorld-instans
            // ...

            // Huvudloopen
            bool running = true;
            while (running)
            {
                // Kom ihåg vad klockan var i början
                DateTime before = DateTime.Now;

                // Hantera knapptryckningar från användaren
                ConsoleKey key = ReadKeyIfExists();
                switch (key)
                {
                    case ConsoleKey.Q:
                        running = false;
                        break;
                    case ConsoleKey.W:
                        player.Direction = "UP";
                        break;
                    case ConsoleKey.D:
                        player.Direction = "RIGHT";
                        break;
                    case ConsoleKey.S:
                        player.Direction = "DOWN";
                        break;
                    case ConsoleKey.A:
                        player.Direction = "LEFT";
                        break;

                    // TODO Lägg till logik för andra knapptryckningar
                    // ...
                }

                if (world.Score < 0)
                {
                    running = false;
                }

                // Uppdatera världen och rendera om
                renderer.RenderBlank();
                world.Update();
                renderer.Render();

                // Mät hur lång tid det tog
                double frameTime = Math.Ceiling((1000.0 / frameRate) - (DateTime.Now - before).TotalMilliseconds);
                if (frameTime > 0)
                {
                    // Vänta rätt antal millisekunder innan loopens nästa varv
                    Thread.Sleep((int)frameTime);
                }
            }
            renderer.GameOver();
            Thread.Sleep(4000);
            RunMainMenu();
        }

        static void RunMainMenu()
        {
            if (OperatingSystem.IsWindows())
            {
                Console.SetWindowSize(84, 30);
            }
            string prompt = @"

  _________                 __               ________                          
 /   _____/  ____  _____   |  | __  ____    /  _____/ _____     _____    ____  
 \_____  \  /    \ \__  \  |  |/ /_/ __ \  /   \  ___ \__  \   /     \ _/ __ \ 
 /        \|   |  \ / __ \_|    < \  ___/  \    \_\  \ / __ \_|  Y Y  \\  ___/ 
/_______  /|___|  /(____  /|__|_ \ \___  >  \______  /(____  /|__|_|  / \___  >
        \/      \/      \/      \/     \/          \/      \/       \/      \/ 

            by Marianne, Ronja och Matti


 Welcome to the Snake Game! Choose from the menu.
 (Use the arrow keys to cycle through options and press enter to select an option.)";

            string[] options = { "Play", "Exit" };
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    Loop();
                    break;
                case 1:
                    Environment.Exit(0);
                    break;
            }
        }

        static void Main(string[] args)
        {
            // Vi kan ev. ha någon meny här, men annars börjar vi bara spelet direkt
            Console.Title = "Snake Game";
            if (OperatingSystem.IsWindows())
            {
                SoundPlayer snakePlayer = new SoundPlayer("Voxel Revolution.wav");
                snakePlayer.Load();
                snakePlayer.PlayLooping();
            }
            
            
            RunMainMenu();
        }
    }
}
