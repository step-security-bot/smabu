using System;
using System.Text.Json.Serialization;

namespace LIT.Smabu.Shared.Entities.Business
{
    public class Period : IValueObject
    {
        public Period(DateOnly from, DateOnly to)
        {
            From = from;
            To = to;
        }

        public DateOnly From { get; }
        public DateOnly To { get; }

        public static Period CreateFrom(DateTime from, DateTime to)
        {
            return new(DateOnly.FromDateTime(from), DateOnly.FromDateTime(to));
        }
    }
}

