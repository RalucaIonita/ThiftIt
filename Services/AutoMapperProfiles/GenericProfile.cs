using AutoMapper;
using DataLayer.Entities;
using Services.Dtos;

namespace Services.AutoMapperProfiles
{
    public class GenericProfile : Profile
    {
        public GenericProfile()
        {
            CreateMap<UserDto, User>().ForMember(d => d.UserName, s => s.MapFrom(x => x.Email));
            CreateMap<User, UserDto>();

            CreateMap<Product, ProductDto>()
                .ForMember(p => p.SellerName,
                x => x.MapFrom(e => (e.Seller.FirstName + " " + e.Seller.LastName)))
                .ForMember(p => p.SellerEmail, 
                    x => x.MapFrom(e => e.Seller.Email))
                .ForMember(p => p.SellerPhoneNumber, 
                    x => x.MapFrom(e => e.Seller.PhoneNumber));
            CreateMap<ProductDto, Product>();

            CreateMap<Cart, CartDto>();
            CreateMap<CartDto, Cart>();
        }
    }
}
