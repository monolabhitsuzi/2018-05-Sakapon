using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
using static System.Linq.Enumerable;

namespace SpiralNumbersConsole2
{
    static class Program
    {
        const int Width = 16;
        const int Height = 10;

        static void Main(string[] args)
        {
            var matrix = GetPoints();
            var digitsLength = matrix.Length.ToString().Length;

            var text = Range(0, Height)
                .Select(j => Range(0, Width)
                    .Select(i => matrix[i, j].ToString().PadLeft(digitsLength))
                    .JoinStrings(" "))
                .JoinStrings("\n");

            WriteLine(text);
        }

        static int[,] GetPoints()
        {
            var p = new Int32Vector();
            var delta = new Int32Vector { X = 1 };

            var matrix = new int[Width, Height];
            if (matrix.Length == 0) return matrix;
            matrix[p.X, p.Y] = 1;

            bool IsValid(Int32Vector _p) => IsInRange(_p) && matrix[_p.X, _p.Y] == 0;

            for (int i = 2; i <= matrix.Length; i++)
            {
                var temp = p + delta;

                if (!IsValid(temp))
                {
                    delta = TurnRight(delta);
                    temp = p + delta;
                }

                p = temp;
                matrix[p.X, p.Y] = i;
            }
            return matrix;
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
