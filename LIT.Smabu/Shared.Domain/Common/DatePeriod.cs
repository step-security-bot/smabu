using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Common
{
    public class DatePeriod : IValueObject
    {
        public DatePeriod(DateOnly from, DateOnly to)
        {
            From = from;
            To = to;
        }

        public DateOnly From { get; }
        public DateOnly To { get; }

        public static DatePeriod CreateFrom(DateTime from, DateTime to)
        {
            return new(DateOnly.FromDateTime(from), DateOnly.FromDateTime(to));
        }
    }
}

