using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Common
{
    public class DatePeriod : IValueObject
    {
        public DatePeriod(DateOnly from, DateOnly? to)
        {
            if (from == DateOnly.MinValue && to == DateOnly.MinValue)
            {
                throw new ArgumentException("Von und bis ungültig.");
            }
            if (to != null && from > to)
            {
                throw new ArgumentException("Von ist größer als bis.");
            }
            From = from;
            To = to;
        }

        public DateOnly From { get; }
        public DateOnly? To { get; }

        public static DatePeriod CreateFrom(DateTime from, DateTime? to)
        {
            to = to == DateTime.MinValue ? null : to;
            if (from == DateTime.MinValue)
            {
                throw new ArgumentNullException(nameof(from));
            }
            else if (to != null && from > to)
            {
                return new(DateOnly.FromDateTime(to.Value), DateOnly.FromDateTime(to.Value));
            }
            else
            {
                return new(DateOnly.FromDateTime(from), to != null ? DateOnly.FromDateTime(to.Value) : null);
            }
        }
    }
}

