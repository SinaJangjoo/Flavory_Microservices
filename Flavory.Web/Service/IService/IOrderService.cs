﻿using Flavory.Web.Models;
using Flavory.Web.Models.Dto;

namespace Flavory.Web.Service.IService
{
	public interface IOrderService
    {
		Task<ResponseDto?> CreateOrder(CartDto cartDto);
		Task<ResponseDto?> CreateStripeSession(StripeRequestDto stripeRequestDto);
		Task<ResponseDto?> ValidateStripeSession(int orderHeaderId);
	}
}
