using System.Collections.Generic;

namespace ChessOnPhoneKeypad.Services.Models
{
    public class PositionConfiguration
    {
        public int PositionIndex { get; set; }

        public string PositionValue { get; set; }

        public IEnumerable<int> NextPossiblePositions { get; set; }
    }
}
