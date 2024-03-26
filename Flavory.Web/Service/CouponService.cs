using Flavory.Web.Models;
using Flavory.Web.Service.IService;

namespace Flavory.Web.Service
{
	public class CouponService : ICouponService
	{
		private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public Task<ResponseDto?> CreateCouponsAsync(CouponDto couponDto)
		{
			throw new NotImplementedException();
		}

		public Task<ResponseDto?> DeleteCouponsAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<ResponseDto?> GetAllCouponsAsync()
		{
			throw new NotImplementedException();
		}

		public Task<ResponseDto?> GetCouponAsync(string couponCode)
		{
			throw new NotImplementedException();
		}

		public Task<ResponseDto?> GetCouponByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<ResponseDto?> UpdateCouponsAsync(CouponDto couponDto)
		{
			throw new NotImplementedException();
		}
	}
}
