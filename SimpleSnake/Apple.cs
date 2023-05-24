using System;
using System.Collections.Generic;
using System.Drawing;

namespace SimpleSnake
{
    public static class Apple
    {
        public static Point Coordinates { get ; private set; }

        public static void Spawn()
        {
            var random = new Random();
            var isSpawned = false;

            while (!isSpawned)
            {
                int x = random.Next(1, Game.BoardHeight);
                int y = random.Next(1, Game.BoardWidth);

                if (!Snake.Body.Contains(new Point(x, y)))
                {
                    Coordinates = new Point(x, y);
                    isSpawned = true;
                }
            }
        }
    }
}
