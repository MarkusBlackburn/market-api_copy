using AutoMapper;
using Core.Models.Domain;
using market_api.DTOs.Accounts;
using market_api.DTOs.Categories;
using market_api.DTOs.Characteristics;
using market_api.DTOs.Products;

namespace market_api.Util
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegisterDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
