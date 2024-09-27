using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public static class InvoiceErrors
    {
        public static readonly Error AlreadyReleased = new("Invoice.AlreadyReleased", "Invoice has already been released.");
        public static readonly Error NumberNotValid = new("Invoice.NumberNotValid", "Invoice number is invalid."); public static readonly Error NumberMayNotBeChangedBelated = new("Invoice.NumberMayNotBeChangedBelated", "Once an invoice number has been assigned, it cannot be changed.");
        public static readonly Error NoPositionsToRelease = new("Invoice.NoPositionsToRelease", "No items available for release.");
        public static readonly Error NotReleasedYet = new("Invoice.NotReleasedYet", "Cannot withdraw release as it has not been released yet.");
        public static readonly Error ItemNotFound = new("Invoice.ItemNotFound", "Item not found.");
        public static readonly Error ItemDetailsEmpty = new("Invoice.DetailsEmpty", "Details must not be empty.");
        public static readonly Error ItemAlreadyAtEnd = new("Invoice.ItemAlreadyAtEnd", "Already at the end of the list.");
        public static readonly Error ItemAlreadyAtBeginning = new("Invoice.ItemAlreadyAtStart", "Already at the beginning of the list.");
    }
}
