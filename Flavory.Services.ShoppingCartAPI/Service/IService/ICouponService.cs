using Flavory.Services.ShoppingCartAPI.Models.Dto;

namespace Flavory.Services.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        //This will be responsible to load all the products from ProductAPI

        Task<CouponDto> GetCoupon(string couponCode);

    }
}
