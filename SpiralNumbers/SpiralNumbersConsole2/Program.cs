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

            Range(0, Height)
                .Select(j => Range(0, Width)
                    .Select(i => matrix[i, j].ToString().PadLeft(digitsLength))
                    .JoinStrings(" "))
                .Execute(WriteLine);
        }

        static int[,] GetPoints()
        {
            var matrix = new int[Width, Height];
            if (matrix.Length == 0) return matrix;
            var delta = (X: 1, Y: 0);

            bool IsValid((int X, int Y) _p) => IsInRange(_p) && matrix[_p.X, _p.Y] == 0;

            (int X, int Y) NextPoint((int X, int Y) _p)
            {
                var temp = Add(_p, delta);
                if (IsValid(temp)) return temp;

                delta = TurnRight(delta);
                return Add(_p, delta);
            }

            var p = (X: 0, Y: 0);
            matrix[p.X, p.Y] = 1;

            for (var i = 2; i <= matrix.Length; i++)
            {
                p = NextPoint(p);
                matrix[p.X, p.Y] = i;
            }
            return matrix;
        }

        static bool IsInRange((int X, int Y) p) => 0 <= p.X && p.X < Width && 0 <= p.Y && p.Y < Height;
        static (int X, int Y) Add((int X, int Y) v1, (int X, int Y) v2) => (X: v1.X + v2.X, Y: v1.Y + v2.Y);
        static (int X, int Y) TurnRight((int X, int Y) v) => (X: -v.Y, Y: v.X);

        static string JoinStrings(this IEnumerable<string> source, string separator) => string.Join(separator, source);
        static void Execute<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (var item in source) action(item);
        }
    }
}
