using System;

namespace SimpleSnake
{
    class Program
    {
        static void Main(string[] args)
        {
            Apple.Spawn();

            while (!Game.IsOver)
            {
                Game.Render();
                Game.WaitForInput();
            }

            Console.WriteLine("GAME OVER!");
            Environment.Exit(0);
        }
    }
}
