﻿using System;

namespace PortfolioCalculation.Models
{
    public class Transaction
    {
        public string InvestmentId { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
