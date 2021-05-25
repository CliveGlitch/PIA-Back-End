using System;
using System.Collections.Generic;

#nullable disable

namespace Northwind_Database.DataAccess
{
    public partial class ProductsAboveAveragePrice
    {
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
