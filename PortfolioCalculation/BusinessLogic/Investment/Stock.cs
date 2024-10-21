using PortfolioCalculation.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioCalculation.BusinessLogic.Investment
{
    public class Stock : InvestmentProduct
    {
        public decimal SharePrice { get; set; }

        public override decimal CalculateValue(DateTime date, Dictionary<string, List<Transaction>> transactions)
        {
            var stockValue = transactions[InvestmentId]
                            .Where(t => t.Type == "Shares" && t.Date <= date) 
                            .Sum(g => g.Value) * SharePrice;

            return stockValue;
        }
    }
}
