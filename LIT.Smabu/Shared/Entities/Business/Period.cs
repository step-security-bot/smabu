using System;

namespace LIT.Smabu.Shared.Entities.Business
{
    public class Period : IValueObject
    {
        public Period(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }

        public DateTime From { get; }
        public DateTime To { get; }
    }
}

