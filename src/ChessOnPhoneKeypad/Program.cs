using System;
using System.Collections.Generic;
using ChessOnPhoneKeypad.Services;
using System.Linq;

namespace ChessOnPhoneKeypad
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Cannot start with 0 or 1
            var valuesToIgnore = new [] { 0, 1 };
            
            // Length of valid phone number
            var lengthOfPhoneNumber = 7;

            // Standard phone keypad layout
            ILayout layout = new PhoneKeypadLayout();

            // Standard chess pieces
            var chessPieces = Enum.GetValues(typeof(StandardChessPieces)).Cast<StandardChessPieces>().ToList();

            // Call the counter with the above configuration
            var countByChessPieces = new CounterService(layout, chessPieces, valuesToIgnore, lengthOfPhoneNumber).Count();

            // Print the results
            foreach (var count in countByChessPieces)
            {
                Console.WriteLine($" - {count.Item1} : {count.Item2}");
            }
        }
    }
}
