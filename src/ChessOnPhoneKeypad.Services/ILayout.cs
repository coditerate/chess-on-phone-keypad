using System.Collections.Generic;

namespace ChessOnPhoneKeypad.Services
{
    public interface ILayout
    {
        Dictionary<int, List<int>> PossibleNextMovePositions(StandardChessPieces chessPiece);

        int NumberOfDigits { get; set; }
    }
}