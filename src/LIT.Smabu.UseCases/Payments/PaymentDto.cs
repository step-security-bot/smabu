using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.PaymentAggregate;

namespace LIT.Smabu.UseCases.Payments
{
    public class PaymentDto(PaymentId id, PaymentDirection direction, DateTime accountingDate, string details,
        string payer, string payee, CustomerId? customerId, InvoiceId? invoiceId, string referenceNr, DateTime? referenceDate,
        decimal amountDue, decimal amountPaid, DateTime? paidAt, Currency currency, PaymentStatus status)
    {
        public PaymentId Id { get; } = id;
        public PaymentDirection Direction { get; } = direction;
        public DateTime AccountingDate { get; } = accountingDate;
        public string Details { get; } = details;
        public string Payer { get; } = payer;
        public string Payee { get; } = payee;
        public CustomerId? CustomerId { get; } = customerId;
        public InvoiceId? InvoiceId { get; } = invoiceId;
        public string ReferenceNr { get; } = referenceNr;
        public DateTime? ReferenceDate { get; } = referenceDate;
        public decimal AmountDue { get; } = amountDue;
        public decimal AmountPaid { get; } = amountPaid;
        public DateTime? PaidAt { get; } = paidAt;
        public Currency Currency { get; } = currency;
        public PaymentStatus Status { get; } = status;

        internal static PaymentDto Create(Payment payment)
        {
            return new PaymentDto(payment.Id, payment.Direction, payment.AccountingDate, payment.Details, payment.Payer, payment.Payee, 
                payment.CustomerId, payment.InvoiceId, payment.ReferenceNr, payment.ReferenceDate, 
                payment.AmountDue, payment.AmountPaid, payment.PaidAt, payment.Currency, payment.Status);
        }
    }
}