using System;
using System.Collections.Generic;
using System.Linq;

namespace SpiralNumbersConsole
{
    static class Program
    {
        const int Width = 16;
        const int Height = 10;

        static void Main(string[] args)
        {
            var numbers = Enumerable.Range(1, Width * Height).ToArray();
            var digitsLength = numbers.Length.ToString().Length;

            var text = GetPoints()
                .Zip(numbers, (p, n) => new { p, n })
                .GroupBy(_ => _.p.Y)
                .Select(g => g
                    .OrderBy(_ => _.p.X)
                    .Select(_ => _.n.ToString().PadLeft(digitsLength))
                    .JoinStrings(" "))
                .JoinStrings("\n");

            Console.WriteLine(text);
        }

        static IEnumerable<Int32Vector> GetPoints()
        {
            var point = new Int32Vector();
            var delta = new Int32Vector { X = 1 };

            var points = new HashSet<Int32Vector>();
            points.Add(point);

            bool IsValid(Int32Vector p) => IsInRange(p) && !points.Contains(p);

            while (true)
            {
                var p = point + delta;

                if (!IsValid(p))
                {
                    delta = TurnRight(delta);
                    p = point + delta;

                    if (!IsValid(p))
                        return points;
                }

                point = p;
                points.Add(point);
            }
        }

        static bool IsInRange(Int32Vector p) => 0 <= p.X && p.X < Width && 0 <= p.Y && p.Y < Height;
        static Int32Vector TurnRight(Int32Vector v) => new Int32Vector { X = -v.Y, Y = v.X };

        static string JoinStrings(this IEnumerable<string> source, string separator) => string.Join(separator, source);
    }

    public struct Int32Vector
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static Int32Vector operator +(Int32Vector v1, Int32Vector v2) => new Int32Vector { X = v1.X + v2.X, Y = v1.Y + v2.Y };
        public override string ToString() => $"({X}, {Y})";
    }
}
