using ChessOnPhoneKeypad.Services.Enums;
using ChessOnPhoneKeypad.Services.Services.BoardLayout;
using ChessOnPhoneKeypad.Services.Services.ChessMoves;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessOnPhoneKeypad.Services.Services.Counter
{
    public class CounterService
    {
        private readonly IBoardLayout _layout;
        private readonly IEnumerable<StandardChessPiece> _chessPieces;
        private IMoves _moves;

        public CounterService(IBoardLayout layout, IEnumerable<StandardChessPiece> chessPieces)
        {
            _layout = layout;
            _chessPieces = chessPieces;
        }

        /// <summary>
        /// This method will give you the total number of valid phone numbers, traced out on a given layout
        /// for each chess piece in the given list of all chess pieces.
        /// 'cannotStartWith' - values to ignore i.e., valid phone number cannot start with these numbers.
        /// 'lengthOfPhoneNumber' - number of the digits a valid phone number needs to have.
        /// </summary>
        /// <param name="cannotStartWith">list of items which a valid phone number cannot start with</param>
        /// <param name="cannotContain">list of items which cannot be part of a valid phone number</param>
        /// <param name="lengthOfPhoneNumber">length of phone number i.e., the paths to traverse for each chess piece</param>
        /// <returns></returns>
        public List<(StandardChessPiece, double)> Count(string[] cannotStartWith, string[] cannotContain, int lengthOfPhoneNumber)
        {
            var countByChessPiece = new List<(StandardChessPiece, double)>();

            var rows = _layout.Configuration.GetLength(0);
            var columns = _layout.Configuration.GetLength(1);
            var totalPositions = _layout.Configuration.Length;

            foreach (var chessPiece in _chessPieces)
            {
                _moves = InstantiateMoves(chessPiece, cannotContain);
                var currentRow = new double[totalPositions];

                foreach (var r in Enumerable.Range(0, rows))
                {
                    foreach (var c in Enumerable.Range(0, columns))
                    {
                        if (cannotStartWith.All(vi => vi != _layout.Configuration[r, c].Item2))
                            currentRow[_layout.Configuration[r, c].Item1] = 1;
                    }
                }

                foreach (var _ in Enumerable.Range(1, lengthOfPhoneNumber - 1))
                {
                    var previousRow = currentRow;
                    currentRow = new double[totalPositions];

                    var nextPositions = _moves.NextPossiblePositions();

                    foreach (var position in nextPositions)
                    {
                        foreach (var nextPosition in position.NextPossiblePositions)
                        {
                            currentRow[nextPosition] += previousRow[position.PositionIndex];
                        }
                    }
                }

                countByChessPiece.Add((chessPiece, currentRow.Sum()));
            }

            return countByChessPiece;
        }

        private IMoves InstantiateMoves(StandardChessPiece chessPiece, string[] cannotContain)
        {
            switch (chessPiece)
            {
                case StandardChessPiece.King: return new KingMoves(_layout, cannotContain);
                case StandardChessPiece.Queen: return new QueenMoves(_layout, cannotContain);
                case StandardChessPiece.Bishop: return new BishopMoves(_layout, cannotContain);
                case StandardChessPiece.Knight: return new KnightMoves(_layout, cannotContain);
                case StandardChessPiece.Rook: return new RookMoves(_layout, cannotContain);
                case StandardChessPiece.Pawn: return new PawnMoves(_layout, cannotContain);
                default: throw new ArgumentOutOfRangeException(nameof(chessPiece), chessPiece, null);
            }
        }
    }
}
