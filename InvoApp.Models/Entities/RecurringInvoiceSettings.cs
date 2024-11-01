using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoApp.Models.Entities
{
    public record class RecurringInvoiceSettings
    {
        public int Id { get; set; }
        public int InvoiceId { get; set;}
        public string Frequency { get; set; } = string.Empty;
        public int IntervalCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime NextRecurrenceDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
