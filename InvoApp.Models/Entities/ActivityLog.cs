using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoApp.Models.Entities
{
    public enum ActivityType
    {
        created,
        updated,
        deleted,
        login,
        logout,
        paymentReceived,
        invoiceSent,
        reminderSent
    }
    public enum EntityType
    {
        invoice,
        client,
        payment
    }
    public record class ActivityLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ActivityType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public EntityType EntityType { get; set; }
        public int EntityId { get; set; }
        public DateTime CratedAt { get; set; }
    }
}
