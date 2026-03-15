using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetBites.Models
{
    public class SpendingRecord
    {
        public DateTime Date { get; set; }

        public decimal TotalAmount { get; set; }

        public string StoreName { get; set; }
    }
}
