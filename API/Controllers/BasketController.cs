using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BasketController(IBasketRepository basketRepository, IMapper mapper) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
    {
        var basket = await basketRepository.GetBasketAsync(id);
        return Ok(basket ?? new CustomerBasket(id));
    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO basket)
    {
        var customerBasket = mapper.Map<CustomerBasketDTO, CustomerBasket>(basket);
        var updatedBasket = await basketRepository.UpdateBasketAsync(customerBasket);
        return Ok(updatedBasket);
    }

    [HttpDelete]
    public async Task DeleteBasketAsync(string id)
    {
        await basketRepository.DeleteBasketAsync(id);
    }
}