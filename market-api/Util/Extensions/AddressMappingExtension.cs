using Core.Models.Domain;
using market_api.DTOs.Accounts;

namespace market_api.Util.Extensions
{
    public static class AddressMappingExtension
    {
        public static CreateOrUpdateAddressDto? ToDto(this ShippingAddress address)
        {
            if (address is null) return null;

            return new CreateOrUpdateAddressDto
            {
                Line1 = address.Line1,
                Line2 = address.Line2,
                City = address.City,
                Disctrict = address.Disctrict,
                Country = address.Country,
                PostalCode = address.PostalCode
            };
        }

        public static ShippingAddress FromDto(this CreateOrUpdateAddressDto addressDto)
        {
            return addressDto is null
                ? throw new ArgumentNullException(nameof(addressDto))
                : new ShippingAddress
            {
                Line1 = addressDto.Line1,
                Line2 = addressDto.Line2,
                City = addressDto.City,
                Disctrict = addressDto.Disctrict,
                Country = addressDto.Country,
                PostalCode = addressDto.PostalCode
            };
        }

        public static void UpdateFromDto(this ShippingAddress address, CreateOrUpdateAddressDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));
            if (address is null) throw new ArgumentException(nameof(address));

            address.Line1 = dto.Line1;
            address.Line2 = dto.Line2;
            address.City = dto.City;
            address.Disctrict = dto.Disctrict;
            address.Country = dto.Country;
            address.PostalCode = dto.PostalCode;
        }
    }
}
