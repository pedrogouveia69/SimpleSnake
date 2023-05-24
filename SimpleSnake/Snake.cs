using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace SimpleSnake
{
    public static class Snake
    {
        public static List<Point> Body { get; private set; }
        private static Point Head { get => Body[0]; }
        private static Point Tail { get => Body[^1]; }
        public static ConsoleKey LastMove { get; private set; }
        private static Dictionary<ConsoleKey, ConsoleKey> OppositeDirections { get; } = new Dictionary<ConsoleKey, ConsoleKey>
        {
            { ConsoleKey.RightArrow, ConsoleKey.LeftArrow },
            { ConsoleKey.LeftArrow, ConsoleKey.RightArrow },
            { ConsoleKey.UpArrow, ConsoleKey.DownArrow },
            { ConsoleKey.DownArrow, ConsoleKey.UpArrow }
        };

        static Snake()
        {
            Body = new List<Point>();

            for (int i = 12; i >= 5; i--)
            {
                Body.Add(new Point(7, i));
            }

            LastMove = ConsoleKey.RightArrow;
        }

        public static void Move(ConsoleKey key)
        {
            if (IsOppositeDirection(key))
            {
                return;
            }

            var (deltaX, deltaY) = GetMovementOffsets(key);

            var newHead = new Point(Head.X + deltaX, Head.Y + deltaY);

            if (IsCollisionDetected(newHead))
            {
                Game.IsOver = true;
                return;
            }

            Body.Insert(0, newHead);

            if (newHead != Apple.Coordinates)
            {
                Body.RemoveAt(Body.Count - 1);
            }
            else
            {
                Apple.Spawn();
            }

            LastMove = key;
        }

        private static bool IsOppositeDirection(ConsoleKey key)
        {
            return OppositeDirections.ContainsKey(key) && OppositeDirections[key] == LastMove;
        }

        private static (int, int) GetMovementOffsets(ConsoleKey key)
        {
            return key switch
            {
                ConsoleKey.RightArrow => (0, 1),
                ConsoleKey.LeftArrow => (0, -1),
                ConsoleKey.UpArrow => (-1, 0),
                ConsoleKey.DownArrow => (1, 0),
                _ => (0, 0)
            };
        }

        private static bool IsCollisionDetected(Point newHead)
        {
            return Body.Contains(newHead) || IsOutOfBounds(newHead);
        }

        private static bool IsOutOfBounds(Point point)
        {
            return point.X < 1 || point.X >= Game.BoardHeight || point.Y < 1 || point.Y >= Game.BoardWidth;
        }
    }
}
