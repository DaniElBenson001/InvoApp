using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoApp.Models.Entities
{
    public enum ReminderType
    {
        due,
        overdue
    }
    public enum ReminderStatus
    {
        sent,
        pending,
        failed

    }
    public record class Reminder
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public ReminderType Type { get; set; }
        public DateTime? ScheduledDate {  get; set; }
        public DateTime? SentTime { get; set; }
        public ReminderStatus Status { get; set; }
        public string EmailContent { get; set; } = string.Empty;
        public DateTime CreatedAt {  get; set; }
        public DateTime LastUpdatedAt { get; set; }

    }
}
