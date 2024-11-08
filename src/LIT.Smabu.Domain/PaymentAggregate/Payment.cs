using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.PaymentAggregate
{
    public class Payment : AggregateRoot<PaymentId>
    {
        public override PaymentId Id { get; }
        public PaymentDirection Direction { get; }
        public DateTime AccountingDate { get; private set; }
        public string Details { get; private set; }
        public string Payer { get; private set; }
        public string Payee { get; private set; }
        public CustomerId? CustomerId { get; private set; }
        public InvoiceId? InvoiceId { get; private set; }
        public string ReferenceNr { get; private set; }
        public DateTime? ReferenceDate { get; private set; }
        public decimal AmountDue { get; private set; }
        public decimal AmountPaid { get; private set; }
        public DateTime? PaidAt { get; private set; }
        public Currency Currency { get; private set; }
        public PaymentStatus Status { get; private set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0290:Primären Konstruktor verwenden")]
        public Payment(PaymentId id, PaymentDirection direction, DateTime accountingDate, string details, string payer, string payee,
            CustomerId? customerId, InvoiceId? invoiceId, string documentNr, DateTime? documentDate,
            decimal amountDue, decimal amountPaid, DateTime? paidAt, Currency currency, PaymentStatus status)
        {
            Id = id;
            Direction = direction;
            Details = details;
            Payer = payer;
            Payee = payee;
            CustomerId = customerId;
            InvoiceId = invoiceId;
            ReferenceNr = documentNr;
            ReferenceDate = documentDate;
            AccountingDate = accountingDate;
            AmountDue = amountDue;
            AmountPaid = amountPaid;
            PaidAt = paidAt;
            Currency = currency;
            Status = status;
        }

        public static Payment CreateIncoming(PaymentId id, string details, string payer, string payee,
            CustomerId customerId, InvoiceId invoiceId, string documentNr, DateTime? documentDate, DateTime accountingDate, decimal amountDue)
        {
            return new Payment(id, PaymentDirection.Incoming, accountingDate, details, payer, payee, customerId, invoiceId,
                documentNr, documentDate, amountDue, 0, null, Common.Currency.EUR, PaymentStatus.Pending);
        }

        public static Payment CreateOutgoing(PaymentId id, string details, string payer, string payee,
            string documentNr, DateTime? documentDate, DateTime accountingDate, decimal amountDue)
        {
            return new Payment(id, PaymentDirection.Outgoing, accountingDate, details, payer, payee, null, null,
                documentNr, documentDate, amountDue, 0, null, Common.Currency.EUR, PaymentStatus.Pending);
        }

        public Result Update(string details, string payer, string payee, string referenceNr, DateTime? referenceDate,
            decimal amountDue, PaymentStatus status)
        {
            if (status == PaymentStatus.Paid && Status == PaymentStatus.Paid)
            {
                return PaymentErrors.PaymentAlreadyPaid;
            }
            if (status == null)
            {
                return PaymentErrors.StatusMustNotBeNull;
            }

            Details = details;
            Payer = payer;
            Payee = payee;
            ReferenceNr = referenceNr;
            ReferenceDate = referenceDate;
            AmountDue = amountDue;
            Status = status;

            return Result.Success();
        }

        public Result Complete(decimal amountPaid, DateTime paidAt)
        {
            if (Status == PaymentStatus.Paid)
            {
                return PaymentErrors.PaymentAlreadyPaid;
            }

            AmountPaid = amountPaid;
            PaidAt = paidAt;
            Status = PaymentStatus.Paid;

            return Result.Success();
        }

        public override Result Delete()
        {
            if (Status == PaymentStatus.Paid)
            {
                return PaymentErrors.PaymentAlreadyPaid;
            }
            return base.Delete();
        }
    }
}
