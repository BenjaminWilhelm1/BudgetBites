using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetBites.Models
{
    public class GroceryItem
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public string Category { get; set; }

        public decimal UnitPrice { get; set; }

        public bool IsPurchased { get; set; }
    }
}
