using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.Domain.Common
{
    public record Person(string Salutation, string FirstAndMidName, string lastName, string Department) : IValueObject
    {
    }
}
