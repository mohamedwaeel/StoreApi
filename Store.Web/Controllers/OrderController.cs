
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Entities;
using Store.Service.HandleResponse;
using Store.Service.Services.OrderService;
using Store.Service.Services.OrderService.Dtos;
using System.Security.Claims;

namespace Store.Web.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService) {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<ActionResult<OrderDetailsDto>>CreatOrderAsync(OrderDto input)
        {
            var order = await _orderService.CreateOrderAsync(input);
            if (order is null)
                return BadRequest(new Response(400, "error while creating ur own order"));
            return Ok(order);

        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDetailsDto>>> GetAllOrdersForUserAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetAllOrdersForUserAsync(email);
            return Ok(orders);
        }
        [HttpGet]
        public async Task<ActionResult<OrderDetailsDto>> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetAllDeliveryMethodsAsync()
        =>Ok(await _orderService.GetAllDeliveryMethodsAsync());
    }
}
