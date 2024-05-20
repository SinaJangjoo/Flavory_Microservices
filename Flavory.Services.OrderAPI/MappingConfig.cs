using AutoMapper;
using Flavory.Services.OrderAPI.Models;
using Flavory.Services.OrderAPI.Models.Dto;

namespace Mango.Services.OrderAPI
{

	//As long as the property name are exactly the same in both "model" and "Dto" the auto mapper do the mapping

	public class MappingConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				config.CreateMap<OrderHeaderDto,CartHeaderDto>()
				.ForMember(dest =>dest.CartTotal, u=>u.MapFrom(src=>src.OrderTotal)).ReverseMap();

				config.CreateMap<CartDetailsDto , OrderDetailsDto>()
				.ForMember(dest => dest.ProductName, u => u.MapFrom(src => src.Product.Name))
				.ForMember(dest => dest.Price, u => u.MapFrom(src => src.Product.Price));

				config.CreateMap<OrderDetailsDto, CartDetailsDto>();

				config.CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
				config.CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();

            });
			return mappingConfig;
		}
	}
}