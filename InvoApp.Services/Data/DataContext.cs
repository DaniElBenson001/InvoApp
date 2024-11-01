using InvoApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoApp.Services.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserSettings> Settings { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<ActivityLog> ActivityLog { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoceItems { get; set; }
        public DbSet<RecurringInvoiceSettings> RecurringInvoiceSettings { get; set; }
        public DbSet<Client> Clients {  get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }



}
