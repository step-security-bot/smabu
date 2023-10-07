using LIT.Smabu.Domain.Shared.Common;

namespace LIT.Smabu.Shared.Common
{
    public class AddressDTO
    {
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Additional { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Address ToValueObject()
        {
            return new Address(Name1, Name2, Street, HouseNumber, Additional, PostalCode, City, Country);
        }
    }
}
