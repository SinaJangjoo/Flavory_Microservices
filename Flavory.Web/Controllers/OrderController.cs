using Flavory.Web.Models;
using Flavory.Web.Service.IService;
using Flavory.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace Flavory.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult OrderIndex()
        {
            return View();
        }

        public async Task<IActionResult> OrderDetail(int orderId)
        {
            OrderHeaderDto orderHeaderDto = new();
            string userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            var response = await _orderService.GetOrder(orderId);
            if (response != null && response.IsSuccess)
            {
                orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
            }
            if (!User.IsInRole(SD.RoleAdmin) && userId != orderHeaderDto.UserId)  //It means the user wants to access someone's order
            {
                return NotFound();
            }
            return View(orderHeaderDto);
        }

        [HttpPost("OrderReadyForPickup")]
        public async Task<IActionResult> OrderReadyForPickup(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, SD.Status_ReadyForPickup);
            if (response != null && response.IsSuccess)
            {
                TempData["warning"] = "Order is Ready for Pickup!";
                return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
            }
            return View();
        }

        [HttpPost("CompleteOrder")]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, SD.Status_Completed);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Order completed successfully!";
                return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
            }
            return View();
        }

        [HttpPost("CancelOrder")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, SD.Status_Cancelled);
            if (response != null && response.IsSuccess)
            {
                TempData["error"] = "Order cancelled successfully!";
                return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeaderDto> list;
            string userId = "";

            if (!User.IsInRole(SD.RoleAdmin))
            {
                userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            }
            ResponseDto response = _orderService.GetAllOrders(userId).GetAwaiter().GetResult();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderHeaderDto>>(Convert.ToString(response.Result));
				switch (status)
				{
					case "approved":
						list = list.Where(u => u.Status == SD.Status_Approved);
						break;
					case "readyforpickup":
						list = list.Where(u => u.Status == SD.Status_ReadyForPickup);
						break;
					case "cancelled":
						list = list.Where(u => u.Status == SD.Status_Cancelled || u.Status == SD.Status_Refunded);
						break;
					default:
						break;
				}
			}
            else
            {
                list = new List<OrderHeaderDto>();
            }
            return Json(new { data = list });  //it will return the list that we deserialized
        }
    }
}
