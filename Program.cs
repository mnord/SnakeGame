using System;
using System.Threading;
using System.Media;

/**
 * @author Marianne Nordlund, Ronja Österback och Matti Heinonen
 */

namespace SnakeGame
{
    class Program
    {
        /*
         * Vi la till: 
         * En HardMode där maten flyttar sig ett steg i random riktning varannan rendering
         * En dålig mat som är grön och man får minus points när ormen äter den
         * Vid -1 points förlorar man spelet
         * En svans
         * En meny 
         * Musik
         */

        /// <summary>
        /// Checks Console to see if a keyboard key has been pressed, if so returns it, otherwise NoName.
        /// </summary>
        static ConsoleKey ReadKeyIfExists() => Console.KeyAvailable ? Console.ReadKey(intercept: true).Key : ConsoleKey.NoName;
        
        // tar in en int för att välja vanligt spel eller hardmode
        static void Loop(int choice)
        {
            // Initialisera spelet
            const int frameRate = 5;
            GameWorld world = new GameWorld(62, 24); // Nytt spel med storlek X och Y
            Player player = new Player(); // En spelare skapas
            world.gameObjects.Add(player); // Och läggs till
            Food food = new Food("white"); // Food skapas, "white" ger poäng, "green" ger minuspoäng
            world.gameObjects.Add(food); 
            Food badfood = new Food("green");
            world.gameObjects.Add(badfood);
            ConsoleRenderer renderer = new ConsoleRenderer(world); // Skapar en render för spel-världen.


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

                if (world.Score < 0) // Game Over vid 0 poäng.
                {
                    running = false;
                }

                // Uppdatera världen och rendera om
                renderer.RenderBlank();
                //Kollar val av spel och kör valt spelläge
                if (choice == 1)
                {
                    world.Update();
                } else if (choice == 2)
                {
                    world.UpdateHard();
                }
                
                renderer.Render();

                // Mät hur lång tid det tog
                double frameTime = Math.Ceiling((1000.0 / frameRate) - (DateTime.Now - before).TotalMilliseconds);
                if (frameTime > 0)
                {
                    // Vänta rätt antal millisekunder innan loopens nästa varv
                    Thread.Sleep((int)frameTime);
                }
            }
            renderer.GameOver(); // Anropar Game Over, väntar 4 sek, återgår till startmenyn.
            Thread.Sleep(4000);
            RunMainMenu();
        }

        static void RunMainMenu() // En lite flashigare meny än bara lite text. ser ut såhär i spelet med.
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
  (Use the arrow keys to cycle through options and press enter to select an option.)
  Instructions to play: move snake with (w s a d) keys, avoid the green food
  Press q to quit in game
  ";

            string[] options = { "Play", "HardMode", "Exit" };
            Menu mainMenu = new Menu(prompt, options); // Menyval görs med piltangenter och enter.
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Console.Clear(); // Starta vanligt spel
                    Loop(1);
                    break;
                case 1:
                    Console.Clear(); // Starta hardmode spel
                    Loop(2);
                    break;
                case 2:
                    Environment.Exit(0); // Avsluta
                    break;
            }
        }

        static void Main(string[] args)
        {
            //Ändrar titel på konsol fönstret
            Console.Title = "Snake Game";
            //Kollar att operatvsystem är windows och startar musik
            if (OperatingSystem.IsWindows())
            {
                SoundPlayer snakePlayer = new SoundPlayer("Voxel Revolution.wav"); // Bakgrundsmusik!
                snakePlayer.Load();
                snakePlayer.PlayLooping();
            }
            
            RunMainMenu(); // Kör menyn
        }
    }
}
