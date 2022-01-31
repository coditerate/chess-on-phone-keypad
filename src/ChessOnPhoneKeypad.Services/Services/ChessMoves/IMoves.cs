using ChessOnPhoneKeypad.Services.Models;
using System.Collections.Generic;

namespace ChessOnPhoneKeypad.Services.Services.ChessMoves
{
    public interface IMoves
    {
        IEnumerable<PositionConfiguration> NextPossiblePositions();
    }
}
