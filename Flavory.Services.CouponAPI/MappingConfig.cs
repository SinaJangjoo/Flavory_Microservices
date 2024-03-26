using AutoMapper;
using Flavory.Services.CouponAPI.Models;
using Flavory.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI
{

	//As long as the property name are exactly the same in both "model" and "Dto" the auto mapper do the mapping

	public class MappingConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				config.CreateMap<CouponDto, Coupon>().ReverseMap();  // <Source , Destination>();
			});
			return mappingConfig;
		}
	}
}