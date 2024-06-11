using API.DTOs;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class OrdersController(IOrderService orderService, IMapper mapper) : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDto)
    {
        var email = User.RetrieveEmailFromPrincipal();
        var address = mapper.Map<AddressDTO, Address>(orderDto.ShipToAddress);
        var order = await orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
        if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));
        return Ok(order);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetOrdersForUser()
    {
        var email = User.RetrieveEmailFromPrincipal();
        var orders = await orderService.GetOrdersForUserAsync(email);
        return Ok(mapper.Map<IReadOnlyList<OrderToReturnDTO>>(orders));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderToReturnDTO>> GetOrderByIdForUser(int id)
    {
        var email = User.RetrieveEmailFromPrincipal();
        var order = await orderService.GetOrderByIdAsync(id, email);
        if (order == null) return NotFound(new ApiResponse(404));
        return mapper.Map<OrderToReturnDTO>(order);
    }

    [HttpGet("deliveryMethods")]
    public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
    {
        return Ok(await orderService.GetDeliveryMethodsAsync());
    }
}