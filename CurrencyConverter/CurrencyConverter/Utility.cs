using System;

namespace CurrencyConverter
{
    internal class Utility
    {
        public static int ParseNumber()
        {
            bool isValid = int.TryParse(Console.ReadLine(), out int value);

            if (isValid)
            {
                return value;
            }
            else
            {
                Console.WriteLine("bad input!");
                return ParseNumber();
            }
        }
    }
}