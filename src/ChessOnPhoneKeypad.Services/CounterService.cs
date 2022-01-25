using System.Collections.Generic;
using System.Linq;

namespace ChessOnPhoneKeypad.Services
{
    public class CounterService
    {
        private readonly ILayout _layout;
        private readonly IEnumerable<StandardChessPieces> _chessPieces;
        private readonly int[] _valuesToIgnore;
        private readonly int _lengthOfPhoneNumber;

        public CounterService(ILayout layout, IEnumerable<StandardChessPieces> chessPieces, int[] valuesToIgnore, int lengthOfPhoneNumber)
        {
            _layout = layout;
            _chessPieces = chessPieces;
            _valuesToIgnore = valuesToIgnore;
            _lengthOfPhoneNumber = lengthOfPhoneNumber;
        }

        /// <summary>
        /// This method will give you the total number of valid phone numbers, traced out on a given layout
        /// for each chess piece in the given list of all chess pieces.
        /// 'valuesToIgnore' - values to ignore i.e., valid phone number cannot start with these numbers.
        /// 'lengthOfPhoneNumber' - number of the digits a valid phone number needs to have.
        /// </summary>
        /// <returns></returns>

        public List<(StandardChessPieces, double)> Count()
        {
            var countByChessPiece = new List<(StandardChessPieces, double)>();

            foreach (var chessPiece in _chessPieces)
            {
                var currentRow = new double[_layout.NumberOfDigits];

                for (var i = 0; i < _layout.NumberOfDigits; i++)
                {
                    if (_valuesToIgnore.All(vi => vi != i)) currentRow[i] = 1;
                }

                foreach (var _ in Enumerable.Range(1, _lengthOfPhoneNumber - 1))
                {
                    var previousRow = currentRow;
                    currentRow = new double[_layout.NumberOfDigits];

                    foreach (var (currentPosition, possibleNextPositions) in _layout.PossibleNextMovePositions(chessPiece))
                    {
                        foreach (var nextPosition in possibleNextPositions)
                        {
                            currentRow[nextPosition] += previousRow[currentPosition];
                        }
                    }
                }

                countByChessPiece.Add((chessPiece, currentRow.Sum()));
            }

            return countByChessPiece;
        }
    }
}
