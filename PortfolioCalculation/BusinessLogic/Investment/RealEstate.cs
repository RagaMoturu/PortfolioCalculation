using PortfolioCalculation.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioCalculation.BusinessLogic.Investment
{
    public class RealEstate : InvestmentProduct
    {
        public override decimal CalculateValue(DateTime date, Dictionary<string, List<Transaction>> transactions)
        {
            var realEstateValue = transactions[InvestmentId]
                                  .Where(t => (t.Type == "Estate" || t.Type == "Building") && t.Date <= date)
                                  .Sum(t => t.Value);

            return realEstateValue;
        }
    }
}
