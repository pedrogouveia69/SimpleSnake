using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace SimpleSnake
{
    /// <summary>
    /// Represents the snake in the game.
    /// </summary>
    public static class Snake
    {
        /// <summary>
        /// The initial X-coordinate of the snake.
        /// </summary>
        private static int InitialSnakeX { get; } = Game.BoardHeight / 2;

        /// <summary>
        /// The initial Y-coordinate of the snake.
        /// </summary>
        private static int InitialSnakeY { get; } = Game.BoardWidth / 2;

        /// <summary>
        /// The initial length of the snake.
        /// </summary>
        private static int InitialSnakeLength { get; } = 6;

        /// <summary>
        /// The body of the snake, represented as a list of points.
        /// </summary>
        public static List<Point> Body { get; private set; }

        /// <summary>
        /// The head of the snake, which is the first point in the body.
        /// </summary>
        private static Point Head { get => Body[0]; }

        /// <summary>
        /// The tail of the snake, which is the last point in the body.
        /// </summary>
        private static Point Tail { get => Body[^1]; }

        /// <summary>
        /// The last move made by the snake.
        /// </summary>
        private static ConsoleKey LastMove { get; set; }

        /// <summary>
        /// The valid key inputs for controlling the snake.
        /// </summary>
        private static List<ConsoleKey> ValidKeyInputs { get; } = new List<ConsoleKey>
    {
        ConsoleKey.RightArrow,
        ConsoleKey.LeftArrow,
        ConsoleKey.UpArrow,
        ConsoleKey.DownArrow
    };

        /// <summary>
        /// A dictionary mapping opposite directions to each other.
        /// </summary>
        private static Dictionary<ConsoleKey, ConsoleKey> OppositeDirections { get; } = new Dictionary<ConsoleKey, ConsoleKey>
    {
        { ConsoleKey.RightArrow, ConsoleKey.LeftArrow },
        { ConsoleKey.UpArrow, ConsoleKey.DownArrow }
    };

        /// <summary>
        /// Static constructor for the Snake class.
        /// </summary>
        static Snake()
        {
            Body = new List<Point>();

            // Initialize the snake's body with the initial coordinates and length.
            for (int i = InitialSnakeY; i > InitialSnakeY - InitialSnakeLength; i--)
            {
                Body.Add(new Point(InitialSnakeX, i));
            }

            LastMove = ConsoleKey.RightArrow;
        }

        /// <summary>
        /// Waits for the user input to control the snake's movement.
        /// </summary>
        public static void WaitForInput()
        {
            var stopwatch = Stopwatch.StartNew();

            // Wait for user input or a maximum of 500 milliseconds.
            while (!Console.KeyAvailable && stopwatch.Elapsed < TimeSpan.FromMilliseconds(500))
            {
                Thread.Sleep(10); // Sleep for a short interval to avoid excessive CPU usage
            }

            if (Console.KeyAvailable)
            {
                var keyInfo = Console.ReadKey(true);

                if (ValidKeyInputs.Contains(keyInfo.Key) && !IsOppositeDirection(keyInfo.Key))
                {
                    Move(keyInfo.Key);
                }
                else
                {
                    Move(LastMove);
                }
            }
            else
            {
                Move(LastMove);
            }
        }

        /// <summary>
        /// Moves the snake in the specified direction.
        /// </summary>
        /// <param name="key">The direction to move the snake.</param>
        public static void Move(ConsoleKey key)
        {
            var (offsetX, offsetY) = GetMovementOffsets(key);

            // Calculate the new position to move the snake's head.
            var moveTo = new Point(Head.X + offsetX, Head.Y + offsetY);

            if (IsCollisionDetected(moveTo))
            {
                // Game over if collision is detected with either the snake's body or the game boundaries.
                Game.IsOver = true;
                return;
            }

            Body.Insert(0, moveTo); // Insert the new head position to the snake's body.

            if (moveTo != Apple.Coordinates)
            {
                // Remove the tail if the snake did not eat the apple.
                Body.RemoveAt(Body.IndexOf(Tail));
            }
            else
            {
                Apple.Spawn(); // Spawn a new apple if the snake ate the previous one.
            }

            LastMove = key; // Update the last move of the snake.
        }

        /// <summary>
        /// Returns the movement offsets based on the specified direction.
        /// </summary>
        /// <param name="key">The direction key.</param>
        /// <returns>A tuple containing the X and Y offsets for movement.</returns>
        private static (int, int) GetMovementOffsets(ConsoleKey key)
        {
            return key switch
            {
                ConsoleKey.RightArrow => (0, 1),
                ConsoleKey.LeftArrow => (0, -1),
                ConsoleKey.UpArrow => (-1, 0),
                ConsoleKey.DownArrow => (1, 0)
            };
        }

        /// <summary>
        /// Checks if the specified key is in the opposite direction of the last move.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if the key is in the opposite direction; otherwise, false.</returns>
        private static bool IsOppositeDirection(ConsoleKey key)
        {
            if (OppositeDirections.ContainsKey(key))
            {
                return OppositeDirections[key] == LastMove;
            }
            else
            {
                foreach (var pair in OppositeDirections)
                {
                    if (pair.Value == key)
                    {
                        return pair.Key == LastMove;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if a collision is detected at the specified point.
        /// </summary>
        /// <param name="point">The point to check for collision.</param>
        /// <returns>True if a collision is detected; otherwise, false.</returns>
        private static bool IsCollisionDetected(Point point)
        {
            return Body.Contains(point) || IsOutOfBounds(point);
        }

        /// <summary>
        /// Checks if the specified point is out of the game boundaries.
        /// </summary>
        /// <param name="point">The point to check.</param>
        /// <returns>True if the point is out of bounds; otherwise, false.</returns>
        private static bool IsOutOfBounds(Point point)
        {
            return point.X < 1 || point.X >= Game.BoardHeight || point.Y < 1 || point.Y >= Game.BoardWidth;
        }
    }

}
