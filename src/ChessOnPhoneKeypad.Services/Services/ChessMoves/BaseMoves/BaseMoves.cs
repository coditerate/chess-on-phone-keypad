using ChessOnPhoneKeypad.Services.Enums;
using ChessOnPhoneKeypad.Services.Models;
using ChessOnPhoneKeypad.Services.Services.BoardLayout;
using System.Collections.Generic;
using System.Linq;

namespace ChessOnPhoneKeypad.Services.Services.ChessMoves.BaseMoves
{
    public class BaseMoves : IBaseMoves
    {
        private readonly IBoardLayout _layout;
        private readonly int _layoutRows;
        private readonly int _layoutColumns;
        private readonly string[] _prohibitedValues;

        public BaseMoves(IBoardLayout layout, string[] prohibitedValues)
        {
            _layout = layout;
            _layoutRows = _layout.Configuration.GetLength(0);
            _layoutColumns = _layout.Configuration.GetLength(1);
            _prohibitedValues = prohibitedValues;
        }

        // TODO - comment rewrite needed.

        /// <summary>
        /// This is the base service class for calculating all possible next positions (where a given chess piece might end up),
        /// for each position which the this chess piece could currently be in.
        /// </summary>
        /// <param name="chessPiece">the chess piece for which the next positions are being calculated</param>
        /// <param name="paths">paths are the movement graphs for any given chess piece taken in a single move</param>
        /// <param name="recursive">recursive defines if the paths can be repeated, for example, a king & queen can move vertically, horizontally & diagonally,
        /// however a king can only move 1 place, whereas the queen can go to any number of places. (</param>
        /// <returns></returns>
        public IEnumerable<PositionConfiguration> NextPossiblePositions(StandardChessPiece chessPiece, (int, int)[] paths, bool recursive = false)
        {
            var nextPositions = new List<PositionConfiguration>();

            foreach (var r in Enumerable.Range(0, _layoutRows))
            {
                foreach (var c in Enumerable.Range(0, _layoutColumns))
                {
                    var nextPosition = new PositionConfiguration
                    {
                        PositionIndex = _layout.Configuration[r, c].Item1,
                        PositionValue = _layout.Configuration[r, c].Item2
                    };

                    var nextPossiblePositions = recursive ? NextPositionsByCurrentPositionRecursive(paths, (r, c)).ToList() : NextPositionsByCurrentPosition(paths, (r, c)).ToList();

                    // Pawn is a special scenario where, it can move forward 1 place. However, it can move 2 places if it happens to be the first move (i.e., first row)
                    if (chessPiece == StandardChessPiece.Pawn && r == 0)
                    {
                        nextPossiblePositions.AddRange(NextPositionsByCurrentPosition(paths, (r + 1, c)));
                    }

                    nextPosition.NextPossiblePositions = nextPossiblePositions;
                    nextPositions.Add(nextPosition);
                }
            }

            return nextPositions;
        }

        private IEnumerable<int> NextPositionsByCurrentPosition((int, int)[] paths, (int, int) currentPosition)
        {
            var nextValues = new List<int>();

            foreach (var (pathRow, pathColumn) in paths)
            {
                var nextRow = currentPosition.Item1 + pathRow;
                var nextColumn = currentPosition.Item2 + pathColumn;

                if (nextRow >= 0 && nextRow < _layoutRows
                    && nextColumn >= 0 && nextColumn < _layoutColumns
                    && _prohibitedValues.All(vi => vi != _layout.Configuration[nextRow, nextColumn].Item2))
                    nextValues.Add(_layout.Configuration[nextRow, nextColumn].Item1);
            }

            return nextValues;
        }

        private IEnumerable<int> NextPositionsByCurrentPositionRecursive((int, int)[] paths, (int, int) currentPosition)
        {
            var nextValues = new List<int>();

            foreach (var (pathRow, pathColumn) in paths)
            {
                var nextRow = currentPosition.Item1 + pathRow;
                var nextColumn = currentPosition.Item2 + pathColumn;

                // Each increment could be the next possible position for recursive moves e.g., for bishop, queen etc.
                while (nextRow >= 0 && nextRow < _layoutRows && nextColumn >= 0 && nextColumn < _layoutColumns)
                {
                    if (_prohibitedValues.All(vi => vi != _layout.Configuration[nextRow, nextColumn].Item2))
                        nextValues.Add(_layout.Configuration[nextRow, nextColumn].Item1);

                    nextRow = nextRow + pathRow;
                    nextColumn = nextColumn + pathColumn;
                }
            }

            return nextValues;
        }
    }
}
