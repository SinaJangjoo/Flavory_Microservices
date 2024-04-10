using AutoMapper;
using Flavory.Services.ShoppingCartAPI.Models;
using Flavory.Services.ShoppingCartAPI.Models.Dto;

namespace Mango.Services.ShoppingCartAPI
{

	//As long as the property name are exactly the same in both "model" and "Dto" the auto mapper do the mapping

	public class MappingConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				config.CreateMap<CartHeader,CartHeaderDto>().ReverseMap();  // <Source , Destination>();
				config.CreateMap<CartDetails,CartDetailsDto>().ReverseMap();  // <Source , Destination>();
			});
			return mappingConfig;
		}
	}
}