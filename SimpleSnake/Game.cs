using System;
using System.Drawing;

namespace SimpleSnake
{
    /// <summary>
    /// Represents the game state and rendering logic.
    /// </summary>
    public static class Game
    {
        /// <summary>
        /// The height of the game board.
        /// </summary>
        public const int BoardHeight = 16;

        /// <summary>
        /// The width of the game board, which is twice the height.
        /// </summary>
        public const int BoardWidth = BoardHeight * 2;

        /// <summary>
        /// Indicates whether the game is over or not.
        /// </summary>
        public static bool IsOver { get; set; }

        /// <summary>
        /// Renders the game board on the console.
        /// </summary>
        public static void Render()
        {
            Console.Clear();

            for (int i = 0; i <= BoardHeight; i++)
            {
                for (int j = 0; j <= BoardWidth; j++)
                {
                    if (Apple.Coordinates.X == i && Apple.Coordinates.Y == j)
                    {
                        Console.Write("A"); // Display the apple character at the apple coordinates.
                    }
                    else if (Snake.Body.Contains(new Point(i, j)))
                    {
                        Console.Write("S"); // Display the snake character at the snake's body coordinates.
                    }
                    else if (i == 0 || i == BoardHeight || j == 0 || j == BoardWidth)
                    {
                        // Display the game board boundaries.
                        // Alternate between displaying spaces and asterisks for aesthetic purposes.
                        if (j % 2 != 0)
                            Console.Write(' ');
                        else
                            Console.Write('*');
                    }
                    else
                    {
                        Console.Write(' '); // Display empty space for other positions on the game board.
                    }
                }
                Console.WriteLine();
            }
        }
    }

}
