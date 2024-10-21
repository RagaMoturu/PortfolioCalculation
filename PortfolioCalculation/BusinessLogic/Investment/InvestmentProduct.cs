using PortfolioCalculation.Models;
using System;
using System.Collections.Generic;

namespace PortfolioCalculation.BusinessLogic.Investment
{
    public abstract class InvestmentProduct
    {
        public string InvestmentId { get; set; }
        
        public abstract decimal CalculateValue(DateTime date, Dictionary<string, List<Transaction>> transactions);
    }
}
