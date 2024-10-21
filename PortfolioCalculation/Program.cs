using LINQtoCSV;
using PortfolioCalculation.BusinessLogic;
using PortfolioCalculation.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace PortfolioCalculation
{
    class Program
    {
        private static IEnumerable<Investment> investments;
        private static IEnumerable<Transaction> transactions;
        private static IEnumerable<Quote> quotes;

        static void Main(string[] args)
        {
            ProcessFiles();

            var portfolio = new Portfolio()
            {
                Investments = investments,
                Transactions = transactions,
                Quotes = quotes
            };

            Console.WriteLine("\nEnter Date and InvestorId in the format dd.mm.yyy;InvestorId:");
            var line = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(line))
            {
                var input = line.Split(';');
                var date = DateTime.Parse(input[0]);
                var investorId = input[1];

                var portfoliosValue = portfolio.GetPortfolioValue(date, investorId);

                Console.WriteLine($"Portfolio Value: {portfoliosValue}");

                Console.WriteLine("\nEnter Date and InvestorId in the format dd.mm.yyy;InvestorId:");
                line = Console.ReadLine();
            }
        }

        private static void ProcessFiles()
        {
            Console.WriteLine("Processing files, please wait...");

            var csvContext = new CsvContext();
            var fileDescription = new CsvFileDescription
            {
                SeparatorChar = ';',
                FirstLineHasColumnNames = true
            };

            var contentPath = $@"{Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."))}\Content";

            investments = csvContext.Read<Investment>($@"{contentPath}\Investments.csv", fileDescription);
            transactions = csvContext.Read<Transaction>($@"{contentPath}\Transactions.csv", fileDescription);
            quotes = csvContext.Read<Quote>($@"{contentPath}\Quotes.csv", fileDescription);
        }
    }
}
