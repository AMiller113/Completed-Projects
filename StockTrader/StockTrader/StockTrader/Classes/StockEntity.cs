using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrader.Classes
{
    class StockEntity
    {
        public string Company { get; set; }
        [Key]
        public string Ticker { get; set; }
        public decimal? Dividend { get; set; }
        public decimal? Yield { get; set; }
        public int Volume { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal NetChange { get; set; }
        public int SharesOwned { get; set; }
    }
}
