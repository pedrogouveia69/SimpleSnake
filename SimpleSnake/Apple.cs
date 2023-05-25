using System;
using System.Drawing;

namespace SimpleSnake
{
    /// <summary>
    /// Represents the apple in the game.
    /// </summary>
    public static class Apple
    {
        /// <summary>
        /// The coordinates of the apple.
        /// </summary>
        public static Point Coordinates { get; private set; }

        /// <summary>
        /// Static constructor for the Apple class.
        /// </summary>
        static Apple()
        {
            Spawn();
        }

        /// <summary>
        /// Spawns a new apple at a random location on the game board.
        /// </summary>
        public static void Spawn()
        {
            var random = new Random();
            var isSpawned = false;

            while (!isSpawned)
            {
                // Generate random coordinates within the game board boundaries.
                int x = random.Next(1, Game.BoardHeight);
                int y = random.Next(1, Game.BoardWidth);

                // Check if the generated coordinates do not overlap with the snake's body.
                if (!Snake.Body.Contains(new Point(x, y)))
                {
                    Coordinates = new Point(x, y);
                    isSpawned = true;
                }
            }
        }
    }

}
