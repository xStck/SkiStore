using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services;

public class OrderService(IUnitOfWork unitOfWork,
    IBasketRepository basketRepo) : IOrderService
{
    public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId,
        Address shippingAddress)
    {
        var basket = await basketRepo.GetBasketAsync(basketId);
        var items = new List<OrderItem>();
        foreach (var item in basket.Items)
        {
            var productItem = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
            var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
            var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
            items.Add(orderItem);
        }

        var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
        var subtotal = items.Sum(item => item.Price * item.Quantity);
        var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);
        unitOfWork.Repository<Order>().Add(order);
        var result = await unitOfWork.Complete();

        if (result <= 0) return null;

        await basketRepo.DeleteBasketAsync(basketId);
        return order;
    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);
        return await unitOfWork.Repository<Order>().ListAsync(spec);
    }

    public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
    {
        var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);
        return await unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
        return await unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
    }
}