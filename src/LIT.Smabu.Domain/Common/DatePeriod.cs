using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.Domain.Common
{
    public record DatePeriod : IValueObject
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

        public override string ToString()
        {
            var result = From.ToShortDateString();
            if (To != null)
            {
                result += "-" + To.Value.ToShortDateString();
            }
            else
            {
                result += "-???";
            }
            return result;
        }

        public string ToStringInMonths()
        {
            if (To == null)
            {
                return $"{this.From.Month:00)}.{this.From.Year}-??.????";
            }
            else if (this.From.Month == this.To?.Month)
            {
                return $"{this.To?.Month:00}.{this.To?.Year}";
            }
            else
            {
                return $"{this.From.Month:00}.{this.From.Year}-{this.To?.Month:00}.{this.To?.Year}";
            }
        }

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

