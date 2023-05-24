using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace SimpleSnake
{
    public static class Game
    {
        public const int BoardWidth = 36;
        public const int BoardHeight = 15;
        public static bool IsOver { get; set; }

        public static void Render()
        {
            Console.Clear();

            for (int i = 0; i <= BoardHeight; i++)
            {
                for (int j = 0; j <= BoardWidth; j++)
                {
                    if (Apple.Coordinates.X == i && Apple.Coordinates.Y == j)
                    {
                        Console.Write("A");
                    }
                    else if (Snake.Body.Contains(new Point(i, j)))
                    {
                        Console.Write("S");
                    }
                    else if (i == 0 || i == BoardHeight || j == 0 || j == BoardWidth)
                    {
                        // This part just makes the game's bounds prettier
                        if (j % 2 != 0)
                            Console.Write(' ');
                        else
                            Console.Write('*');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }
        }

        public static void WaitForInput()
        {
            var stopwatch = Stopwatch.StartNew();
            while (!Console.KeyAvailable && stopwatch.Elapsed < TimeSpan.FromMilliseconds(500))
            {
                Thread.Sleep(10); // Sleep for a short interval to avoid excessive CPU usage
            }

            if (Console.KeyAvailable)
            {
                var keyInfo = Console.ReadKey(true);
                Snake.Move(keyInfo.Key);
            }
            else
            {
                Snake.Move(Snake.LastMove);
            }
        }

    }
}
