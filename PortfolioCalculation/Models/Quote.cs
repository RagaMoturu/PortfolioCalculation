﻿using System;

namespace PortfolioCalculation.Models
{
    public class Quote
    {
        public string ISIN { get; set; }
        public DateTime Date { get; set; }
        public decimal PricePerShare { get; set; }
    }
}