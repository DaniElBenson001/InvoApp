using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoApp.Models.Entities
{
    public enum CurrencyType
    {
        ngn,
        usd,
        gbp
    }
    public record class Client
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CompanyName {  get; set; } = string.Empty;
        public string ContactName {  get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public CurrencyType Currency {  get; set; }
        public string PaymentTerms { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
        public bool IsDeleted { get; set; } = false;
        public List<Invoice>? Invoices { get; set; }
    }
}
