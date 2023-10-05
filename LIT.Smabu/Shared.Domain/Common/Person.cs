using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Shared.Domain.Common
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
