using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoApp.Models.Entities
{
    public record class InvoiceItem
    {
        public int Id {  get; set; }
        public int InvoiceId { get; set; }
        public string Description { get; set; } = string.Empty;
        [Precision(18, 4)]
        public decimal Quantity { get; set; }
        [Precision(18, 4)]
        public decimal Rate { get; set; }
                [Precision(18, 4)]
        public decimal Amount { get; set; }
        public string TaxRate { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
