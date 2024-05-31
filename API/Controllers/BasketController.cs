using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BasketController(IBasketRepository basketRepository) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
    {
        var basket = await basketRepository.GetBasketAsync(id);
        return Ok(basket ?? new CustomerBasket(id));
    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
    {
        var updatedBasket = await basketRepository.UpdateBasketAsync(basket);
        return Ok(updatedBasket);
    }

    [HttpDelete]
    public async Task DeleteBasketAsync(string id)
    {
        await basketRepository.DeleteBasketAsync(id);
    }
}