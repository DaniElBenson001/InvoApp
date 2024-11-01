using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoApp.Models.Entities
{
    public enum NotificationType
    {
        paymentReceived,
        paymentOverdue,
        invoiceSent,
        invoiceDueSoon,
        recurringInvoiceGenerated,
        reminderSent,
        clientInfoUpdated,
        securityAlert,
        systemNotification
    }
    public enum NotificationChannel
    {
        inApp,
        email,
        sms,
        both
    }
    public enum NotificationStatus
    {
        pending,
        failed,
        sent,
        read
    }
    public record class Notification
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public NotificationType Type { get; set; }
        public NotificationChannel Channel { get; set; }
        public NotificationStatus Status { get; set; }
        public DateTime? ReadAt { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}





















