using System;

namespace SimpleSnake
{
    /// <summary>
    /// The entry point of the program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main method of the program.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            while (!Game.IsOver)
            {
                Game.Render(); // Render the game board.
                Snake.WaitForInput(); // Wait for user input to control the snake's movement.
            }

            Console.WriteLine("GAME OVER!"); // Display the game over message.
            Environment.Exit(0); // Terminate the program.
        }
    }
}
