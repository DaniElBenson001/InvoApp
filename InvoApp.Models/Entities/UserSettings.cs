using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoApp.Models.Entities
{
    public record class UserSettings
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DefaultCurrency {  get; set; } = string.Empty;
        public string DefaultTaxRate {  get; set; } = string.Empty;
        public string DefaultInvoiceTemplate {  get; set; } = string.Empty;
        public bool AutoSendReminders {  get; set; }
        public int ReminderDaysBeforeDue { get; set; }
        public int ReminderDaysAfterDue { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
