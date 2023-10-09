using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Common
{
    public class DatePeriod : IValueObject
    {
        public DatePeriod(DateOnly from, DateOnly to)
        {
            if (from == DateOnly.MinValue &&  to == DateOnly.MinValue)
            {
                throw new ArgumentException("Von und bis ungültig.");
            }
            if (to == DateOnly.MinValue)
            {
                to = from;
            }
            if (from == DateOnly.MinValue)
            {
                from = to;
            }
            if (from > to)
            {
                throw new ArgumentException("Von ist größer als bis.");
            }
            From = from;
            To = to;
        }

        public DateOnly From { get; }
        public DateOnly To { get; }

        public static DatePeriod CreateFrom(DateTime? from, DateTime? to)
        {
            if (from == null && to == null)
            {
                throw new ArgumentNullException(nameof(from) + " and " + nameof(to));
            }
            else if (from.HasValue)
            {
                return new(DateOnly.FromDateTime(from.Value), DateOnly.FromDateTime(from.Value));
            }
            else
            {
                return new(DateOnly.FromDateTime(to!.Value), DateOnly.FromDateTime(to!.Value));
            }
        }
    }
}

