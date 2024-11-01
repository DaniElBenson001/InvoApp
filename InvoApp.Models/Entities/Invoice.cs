using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InvoApp.Models.Entities
{
    public enum StatusType
    {
        paid,
        pending,
        overdue
    }
    public record class Invoice
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public CurrencyType Currency { get; set; }
        [Precision(18, 4)]
        public decimal SubTotal { get; set; }
        [Precision(18, 4)]
        public decimal TaxRate { get; set; }
        [Precision(18, 4)]
        public decimal TaxAmount { get; set; }
        [Precision(18, 4)]
        public decimal DiscountPercentage { get; set; }
        [Precision(18, 4)]
        public decimal DiscountAmount { get; set; }
        [Precision(18, 4)]
        public decimal TotalAmount { get; set; }
        public StatusType Status { get; set; }
        public string Notes { get; set; } = string.Empty;
        public List<InvoiceItem> Items { get; set; }
        public List<Payment> Payments { get; set; }
        public RecurringInvoiceSettings RecurringSettings { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
