using LIT.Smabu.Business.Service.Contratcs;
using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Business.Service.Common.Mappers
{
    public class AddressMapper : IMapper<AddressDTO, Address>, IMapper<Address, AddressDTO>
    {
        public Address Map(AddressDTO source)
        {
            return new Address(source.Name1, source.Name2, source.Street, source.HouseNumber, 
                source.Additional, source.PostalCode, source.City, source.Country);
        }

        public AddressDTO Map(Address source)
        {
            return new AddressDTO()
            {
                Name1 = source.Name1,
                Name2 = source.Name2,
                Street = source.Street,
                HouseNumber = source.HouseNumber,   
                Additional = source.Additional,
                PostalCode = source.PostalCode,
                City = source.City,
                Country = source.Country,
            };
        }
    }
}
