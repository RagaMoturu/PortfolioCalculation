using PortfolioCalculation.BusinessLogic.Investment;
using PortfolioCalculation.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioCalculation.BusinessLogic
{
    public class Portfolio
    {
        public IEnumerable<PortfolioCalculation.Models.Investment> Investments { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<Quote> Quotes { get; set; }

        private Dictionary<string, decimal> sharePrices;
        private Dictionary<string, decimal> fundValues = new Dictionary<string, decimal>();

        public decimal GetPortfolioValue(DateTime date, string investorId)
        {
            var investor = Investments.Where(i => i.InvestorId == investorId).FirstOrDefault();

            if (investor == null)
            {
                Console.WriteLine("Investor does not exist.");
                return 0;
            }
            
            //Dictionary to hold transactions by InvestmentId
            var transactions = Transactions
                                .GroupBy(t => t.InvestmentId)
                                .ToDictionary(t => t.Key, t => t.ToList());

            //Dictionary to hold share prices by ISIN for a given date
            sharePrices = Quotes
                            .Where(q => q.Date <= date)
                            .GroupBy(q => q.ISIN)
                            .ToDictionary(q => q.Key, q => q.OrderByDescending(s => s.Date).FirstOrDefault().PricePerShare);            
          

            var portfolioValue = Investments.Where(i => i.InvestorId == investorId)
                                        .Select(i => GetInvestmentProducts(date, transactions, i)) // Get Investment Products for each investment type
                                        .Sum(i => i.CalculateValue(date, transactions)); // Add calculated values from each investment

            return portfolioValue;
        }

        private InvestmentProduct GetInvestmentProducts(DateTime date, Dictionary<string, List<Transaction>> transactions, PortfolioCalculation.Models.Investment investment)
        {
            switch (investment.InvestmentType)
            {
                case "Stock":
                    return new Stock()
                    {
                        InvestmentId = investment.InvestmentId,
                        SharePrice = sharePrices[investment.ISIN]
                    };
                case "RealEstate":
                    return new RealEstate()
                    {
                        InvestmentId = investment.InvestmentId
                    };
                case "Fonds":
                    decimal fundValue;

                    // Calculate fund value based on FondsInvestor as Investor
                    if (!fundValues.ContainsKey(investment.FondsInvestor))
                    {
                        fundValue = Investments
                                        .Where(i => i.InvestorId == investment.FondsInvestor)
                                        .Select(i => GetInvestmentProducts(date, transactions, i))
                                        .Sum(i => i.CalculateValue(date, transactions));
                        fundValues.Add(investment.FondsInvestor, fundValue);
                    }
                    else
                        fundValue = fundValues[investment.FondsInvestor];

                    return new Fund()
                    {
                        InvestmentId = investment.InvestmentId,
                        FundValue = fundValue
                    };
                default:
                    throw new NotImplementedException();
            }            
        }
    }
}
