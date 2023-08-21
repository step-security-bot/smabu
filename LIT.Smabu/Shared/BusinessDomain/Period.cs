using System;
using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.BusinessDomain
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

