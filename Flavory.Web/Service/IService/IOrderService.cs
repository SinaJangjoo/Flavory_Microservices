using Flavory.Web.Models;
using Flavory.Web.Models.Dto;

namespace Flavory.Web.Service.IService
{
	public interface IOrderService
    {
		Task<ResponseDto?> CreateOrder(CartDto cartDto);
		Task<ResponseDto?> CreateStripeSession(StripeRequestDto stripeRequestDto);
		Task<ResponseDto?> ValidateStripeSession(int orderHeaderId);
		Task<ResponseDto?> GetAllOrders(string? userId);
		Task<ResponseDto?> GetOrder(int orderId);
		Task<ResponseDto?> UpdateOrderStatus(int orderId, string newStatus);
	}
}
