using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.Common
{
    public record Person(string Salutation, string FirstAndMidName, string LastName, string Department) : IValueObject
    {
    }
}
