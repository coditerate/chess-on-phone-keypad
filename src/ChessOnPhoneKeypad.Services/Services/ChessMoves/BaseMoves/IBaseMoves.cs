using ChessOnPhoneKeypad.Services.Enums;
using ChessOnPhoneKeypad.Services.Models;
using System.Collections.Generic;

namespace ChessOnPhoneKeypad.Services.Services.ChessMoves.BaseMoves
{
    public interface IBaseMoves
    {
        IEnumerable<PositionConfiguration> NextPossiblePositions(StandardChessPiece chessPiece, (int, int)[] paths, bool recursive = false);
    }
}
