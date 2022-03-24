using System;

namespace CurrencyConverter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var currencyCodes = CurrencyConverter.GetCurrencyTagArray();
            Console.WriteLine(string.Join(", ", currencyCodes));

            Console.WriteLine("Insert currency code you want to exchange from:");
            string fromCurrency = Console.ReadLine();

            Console.WriteLine("Insert currency code you want to exchange to:");
            string toCurrency = Console.ReadLine();

            Console.WriteLine("\nInsert currency amount:");
            float amount = Utility.ParseNumber();

            float exchangeRate = CurrencyConverter.GetExchangeRate(fromCurrency, toCurrency, amount);

            Console.WriteLine("FROM " + amount + " " + fromCurrency.ToUpper() + " TO " + toCurrency.ToUpper() + " = " + exchangeRate);

            Console.ReadLine();
        }
    }
}