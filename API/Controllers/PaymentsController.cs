using API.Errors;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace API.Controllers;

public class PaymentsController(IConfiguration config, IPaymentService paymentService,
    ILogger<PaymentsController> logger) : BaseApiController
{
    private readonly string _whSecret = config.GetSection("StripeSettings:WhSecret").Value;

    [Authorize]
    [HttpPost("{basketId}")]
    public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
    {
        var basket = await paymentService.CreateOrUpdatePaymentIntent(basketId);
        if (basket == null) return BadRequest(new ApiResponse(400, "Problem with yor basket"));
        return basket;
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebhook()
    {
        var json = await new StreamReader(Request.Body).ReadToEndAsync();
        var stripeEvent = EventUtility.ConstructEvent(json,
            Request.Headers["Stripe-Signature"], _whSecret);
        PaymentIntent intent;
        Order order;
        switch (stripeEvent.Type)
        {
            case "payment_intent.succeeded":
                intent = (PaymentIntent)stripeEvent.Data.Object;
                logger.LogInformation("Payment succeeded: ", intent.Id);
                order = await paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                logger.LogInformation("Order updated to payment received: ", order.Id);
                break;
            case "payment_intent.payment_failed":
                intent = (PaymentIntent)stripeEvent.Data.Object;
                logger.LogInformation("Payment failed: ", intent.Id);
                order = await paymentService.UpdateOrderPaymentFailed(intent.Id);
                logger.LogInformation("Order updated to payment failed: ", order.Id);
                break;
        }

        return new EmptyResult();
    }
}