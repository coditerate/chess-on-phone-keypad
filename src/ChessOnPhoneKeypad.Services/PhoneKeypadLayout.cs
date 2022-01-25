using System;
using System.Collections.Generic;

namespace ChessOnPhoneKeypad.Services
{
    /// <summary>
    /// This is the configuration class for phone keypad layout which gives you the number of digits (defaulted to 10) to cater for.
    /// It also gives you possible next positions where a given chess piece might end up, for each position which the this chess piece could currently be in.
    /// For example, a "king" currently at position (digit) 3, could possibly move and end up in positions 2, 5 or 6.
    /// Another example, a "knight" currently at position (digit) 5, cannot possibly move anywhere by standard chess rules for knight's moves.
    /// </summary>
    public class PhoneKeypadLayout : ILayout
    {
        public int NumberOfDigits { get; set; } = 10;

        public Dictionary<int, List<int>> PossibleNextMovePositions(StandardChessPieces chessPiece)
        {
            switch (chessPiece)
            {
                case StandardChessPieces.King: return KingMoves;
                case StandardChessPieces.Queen: return QueenMoves;
                case StandardChessPieces.Bishop: return BishopMoves;
                case StandardChessPieces.Knight: return KnightMoves;
                case StandardChessPieces.Rook: return RookMoves;
                case StandardChessPieces.Pawn: return PawnMoves;
                default: throw new ArgumentOutOfRangeException(nameof(chessPiece), chessPiece, null);
            }
        }

        private static readonly Dictionary<int, List<int>> KingMoves = new()
        {
            { 0, new List<int> { 7, 8, 9 } },
            { 1, new List<int> { 2, 4, 5 } },
            { 2, new List<int> { 1, 3, 4, 5, 6 } },
            { 3, new List<int> { 2, 5, 6 } },
            { 4, new List<int> { 1, 2, 5, 7, 8 } },
            { 5, new List<int> { 1, 2, 3, 4, 6, 7, 8, 9 } },
            { 6, new List<int> { 2, 3, 5, 8, 9 } },
            { 7, new List<int> { 0, 4, 5, 8 } },
            { 8, new List<int> { 0, 4, 5, 6, 7, 9 } },
            { 9, new List<int> { 0, 5, 6, 8 } }
        };

        private static readonly Dictionary<int, List<int>> QueenMoves = new()
        {
            { 0, new List<int> { 2, 5, 7, 8, 9 } },
            { 1, new List<int> { 2, 3, 4, 5, 7, 9 } },
            { 2, new List<int> { 0, 1, 3, 4, 5, 6, 8 } },
            { 3, new List<int> { 1, 2, 5, 6, 7, 9 } },
            { 4, new List<int> { 1, 2, 5, 6, 7, 8 } },
            { 5, new List<int> { 0, 1, 2, 3, 4, 6, 7, 8, 9 } },
            { 6, new List<int> { 2, 3, 4, 5, 8, 9 } },
            { 7, new List<int> { 0, 1, 3, 4, 5, 8, 9 } },
            { 8, new List<int> { 0, 2, 4, 5, 6, 7, 9 } },
            { 9, new List<int> { 0, 1, 3, 5, 6, 7, 8 } }
        };

        private static readonly Dictionary<int, List<int>> BishopMoves = new()
        {
            { 0, new List<int> { 7, 9 } },
            { 1, new List<int> { 5, 9 } },
            { 2, new List<int> { 4, 6 } },
            { 3, new List<int> { 5, 7 } },
            { 4, new List<int> { 2, 8 } },
            { 5, new List<int> { 1, 3, 7, 9 } },
            { 6, new List<int> { 2, 8 } },
            { 7, new List<int> { 0, 3, 5 } },
            { 8, new List<int> { 4, 6 } },
            { 9, new List<int> { 0, 1, 5 } }
        };

        private static readonly Dictionary<int, List<int>> KnightMoves = new()
        {
            { 0, new List<int> { 4, 6 } },
            { 1, new List<int> { 6, 8 } },
            { 2, new List<int> { 7, 9 } },
            { 3, new List<int> { 4, 8 } },
            { 4, new List<int> { 0, 3, 9 } },
            { 5, new List<int>() },
            { 6, new List<int> { 0, 1, 7 } },
            { 7, new List<int> { 2, 6 } },
            { 8, new List<int> { 1, 3 } },
            { 9, new List<int> { 2, 4 } }
        };

        private static readonly Dictionary<int, List<int>> RookMoves = new()
        {
            { 0, new List<int> { 2, 5, 8 } },
            { 1, new List<int> { 2, 3, 4, 7 } },
            { 2, new List<int> { 0, 1, 3, 5, 8 } },
            { 3, new List<int> { 1, 2, 6, 9 } },
            { 4, new List<int> { 1, 5, 6, 7 } },
            { 5, new List<int> { 0, 2, 4, 6, 8 } },
            { 6, new List<int> { 3, 4, 5, 9 } },
            { 7, new List<int> { 1, 4, 8, 9 } },
            { 8, new List<int> { 0, 2, 5, 7, 9 } },
            { 9, new List<int> { 3, 6, 7, 8 } }
        };

        private static readonly Dictionary<int, List<int>> PawnMoves = new()
        {
            { 0, new List<int>() },
            { 1, new List<int> { 4, 7 } },
            { 2, new List<int> { 5, 8 } },
            { 3, new List<int> { 6, 9 } },
            { 4, new List<int> { 7 } },
            { 5, new List<int> { 8 } },
            { 6, new List<int> { 9 } },
            { 7, new List<int>() },
            { 8, new List<int> { 0 } },
            { 9, new List<int>() }
        };
    }
}
