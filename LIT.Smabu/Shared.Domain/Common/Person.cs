using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Common
{
    public record Person : IValueObject
    {
        public Person(string salutation, string firstAndMidName, string lastName, string department)
        {
            Salutation = salutation;
            FirstAndMidName = firstAndMidName;
            LastName = lastName;
            Department = department;
        }

        public string Salutation { get; private set; }
        public string FirstAndMidName { get; private set; }
        public string LastName { get; private set; }
        public string Department { get; private set; }
    }
}
