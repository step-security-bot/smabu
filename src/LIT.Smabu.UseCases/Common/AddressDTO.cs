using LIT.Smabu.Domain.Common;
using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Common
{
    public class AddressDTO : IDTO
    {
        public string DisplayName => $"{Name1}, {Street}, {PostalCode} {City} - {Country}";
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }


        public static AddressDTO From(Address address)
        {
            return new AddressDTO()
            {
                Name1 = address.Name1,
                Name2 = address.Name2,
                Street = address.Street,
                HouseNumber = address.HouseNumber,
                PostalCode = address.PostalCode,
                City = address.City,
                Country = address.Country,
            };
        }
    }
}
