using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrader.Classes
{
    class StockContext : DbContext
    {
        public StockContext() : base("Server = (LocalDB)\\MSSQLLocalDB;Database=StockTraderDB;Integrated Security = True;") {}
        public DbSet<StockEntity> stocks { get; set; }
    }
}
