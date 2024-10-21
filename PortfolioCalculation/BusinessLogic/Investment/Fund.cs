using PortfolioCalculation.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioCalculation.BusinessLogic.Investment
{
    public class Fund : InvestmentProduct
    {
        public decimal FundValue { get; set; }

        public override decimal CalculateValue(DateTime date, Dictionary<string, List<Transaction>> transactions)
        {
            var fundsValue = transactions[InvestmentId]
                        .Where(t => t.Type == "Percentage" && t.Date <= date)
                        .Sum(t => t.Value) * FundValue;

            return fundsValue;
        }
    }
}
