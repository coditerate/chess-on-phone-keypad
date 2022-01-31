using ChessOnPhoneKeypad.Services.Enums;
using ChessOnPhoneKeypad.Services.Models;
using ChessOnPhoneKeypad.Services.Services.BoardLayout;
using System.Collections.Generic;

namespace ChessOnPhoneKeypad.Services.Services.ChessMoves
{
    public class KnightMoves : BaseMoves.BaseMoves, IMoves
    {
        private readonly (int, int)[] _paths = { (2, 1), (2, -1), (-2, 1), (-2, -1), (1, 2), (1, -2), (-1, 2), (-1, -2) };
        private const StandardChessPiece ChessPiece = StandardChessPiece.Knight;

        public KnightMoves(IBoardLayout layout, string[] prohibitedValues) : base(layout, prohibitedValues) { }

        public IEnumerable<PositionConfiguration> NextPossiblePositions()
        {
            return NextPossiblePositions(ChessPiece, _paths);
        }
    }
}
