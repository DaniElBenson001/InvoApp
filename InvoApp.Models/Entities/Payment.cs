using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoApp.Models.Entities
{
    public enum PaymentMethodType
    {
        creditCard,
        debitCard,
        bankTransfer,
        cash,
        check
    }
    public record class Payment
    {
        public int Id { get; set; }
        public int InvoiceId {  get; set; }
        [Precision(18, 4)]
        public decimal Amount { get; set; }
        public CurrencyType Currency {  get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethodType PaymentMethod {  get; set; }
        public string TransactionReference { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
