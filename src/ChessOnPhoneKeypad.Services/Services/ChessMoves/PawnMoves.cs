using ChessOnPhoneKeypad.Services.Enums;
using ChessOnPhoneKeypad.Services.Models;
using ChessOnPhoneKeypad.Services.Services.BoardLayout;
using System.Collections.Generic;

namespace ChessOnPhoneKeypad.Services.Services.ChessMoves
{
    public class PawnMoves : BaseMoves.BaseMoves, IMoves
    {
        private readonly (int, int)[] _paths = { (1, 0) };
        private const StandardChessPiece ChessPiece = StandardChessPiece.Pawn;

        public PawnMoves(IBoardLayout layout, string[] prohibitedValues) : base(layout, prohibitedValues) { }

        public IEnumerable<PositionConfiguration> NextPossiblePositions()
        {
            return NextPossiblePositions(ChessPiece, _paths);
        }
    }
}
