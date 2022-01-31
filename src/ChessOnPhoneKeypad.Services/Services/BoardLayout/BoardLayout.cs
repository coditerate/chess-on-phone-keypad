using System;
using System.Linq;

namespace ChessOnPhoneKeypad.Services.Services.BoardLayout
{
    public class BoardLayout : IBoardLayout
    {
        public BoardLayout(int rows, int columns, string[] values)
        {
            if (values.Length != rows * columns)
            {
                throw new ArgumentException($"Number of rows ({ rows }) and columns ({ columns }) specified incompatible with number of values ({ values.Length }) provided.");
            }

            Configuration = new (int, string)[rows, columns];
            var counter = 0;

            foreach (var r in Enumerable.Range(0, rows))
            {
                foreach (var c in Enumerable.Range(0, columns))
                {
                    Configuration[r, c] = new(counter, values[counter]);

                    counter++;
                }
            }
        }

        public (int, string)[,] Configuration { get; set; }
    }
}
