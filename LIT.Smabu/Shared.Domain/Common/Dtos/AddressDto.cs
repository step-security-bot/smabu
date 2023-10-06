﻿namespace LIT.Smabu.Domain.Shared.Common.Dtos
{
    public class AddressDto
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

        internal static AddressDto Map(Address mainAddress)
        {
            return new AddressDto
            {
                Name1 = mainAddress.Name1,
                Name2 = mainAddress.Name2,
                Street = mainAddress.Street,
                HouseNumber = mainAddress.HouseNumber,
                Additional = mainAddress.Additional,
                PostalCode = mainAddress.PostalCode,
                City = mainAddress.City,
                Country = mainAddress.Country
            };
        }
    }
}
