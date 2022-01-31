using ChessOnPhoneKeypad.Services.Enums;
using ChessOnPhoneKeypad.Services.Services.BoardLayout;
using ChessOnPhoneKeypad.Services.Services.Counter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessOnPhoneKeypad
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 4x3 layout
            IBoardLayout layout = new BoardLayout(4, 3, new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "*", "0", "#" });
            
            // 6x2 layout
            //IBoardLayout layout = new BoardLayout(2, 6, new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "*", "0", "#" });

            // Cannot start with 0 or 1
            var cannotContain = new[] { "*", "#" };

            // Cannot start with 0 or 1
            var cannotStartWith = new[] { "1", "*", "0", "#" };

            // Length of valid phone number
            var lengthOfPhoneNumber = 7;

            // Standard chess pieces
            var chessPieces = Enum.GetValues(typeof(StandardChessPiece)).Cast<StandardChessPiece>().ToList();
            
            // Call the counter with the above configuration
            var countByChessPieces = new CounterService(layout, chessPieces).Count(cannotStartWith, cannotContain, lengthOfPhoneNumber);

            // Print the results
            foreach (var count in countByChessPieces)
            {
                Console.WriteLine($" - {count.Item1} : {count.Item2}");
            }
        }
    }
}
