using ChessOnPhoneKeypad.Services.Enums;
using ChessOnPhoneKeypad.Services.Models;
using ChessOnPhoneKeypad.Services.Services.BoardLayout;
using System.Collections.Generic;

namespace ChessOnPhoneKeypad.Services.Services.ChessMoves
{
    public class KingMoves : BaseMoves.BaseMoves, IMoves
    {
        private readonly (int, int)[] _paths = { (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1), (-1, 0), (-1, 1), (0, 1) };
        private const StandardChessPiece ChessPiece = StandardChessPiece.King;

        public KingMoves(IBoardLayout layout, string[] prohibitedValues) : base(layout, prohibitedValues) { }

        public IEnumerable<PositionConfiguration> NextPossiblePositions()
        {
            return NextPossiblePositions(ChessPiece, _paths);
        }
    }
}
