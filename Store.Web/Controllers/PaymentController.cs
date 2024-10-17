using Microsoft.AspNetCore.Mvc;
using Store.Service.PaymentService;
using Store.Service.Services.BasketService.Dtos;
using Stripe;

namespace Store.Web.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        const string endpointSecret = "whsec_515B3A3292732229d7b2728a6fd3f14bcd6f3c4b3a6727b489f9e448d7e06bc2";

        public PaymentController(IPaymentService paymentService,ILogger<PaymentController> logger)
        {
           _paymentService = paymentService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(CustomerBasketDto input)
            =>Ok(await _paymentService.CreateOrUpdatePaymentIntent(input));
        [HttpPost]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripEvent = EventUtility.ConstructEvent(json, Request.Headers["Strip-Signature"], endpointSecret);
                
                
                PaymentIntent paymentIntent;


                if (stripEvent.Type == "payment_intent.payment_failed")
                {
                    paymentIntent=stripEvent.Data.Object as PaymentIntent;
                    _logger.LogInformation("Payment Failed : ", paymentIntent.Id);
                    var order = await _paymentService.UpdateOrderPaymentFaileed(paymentIntent.Id);
                    _logger.LogInformation("order updated Failed : ", order.Id);

                }
                else if (stripEvent.Type == "payment_intent.succeeded")
                {
                    paymentIntent = stripEvent.Data.Object as PaymentIntent;
                    _logger.LogInformation("Payment success : ", paymentIntent.Id);
                    var order = await _paymentService.UpdateOrderPaymentSucceeded(paymentIntent.Id);
                    _logger.LogInformation("order updated success : ", order.Id);
                }
                else if (stripEvent.Type == "payment_intent.created")
                {
                    _logger.LogInformation("Payment Created : ");

                }
                else
                {
                    Console.WriteLine("unhandeled event type:{0}", stripEvent.Type);

                }
                return Ok();
            }
            catch(StripeException e)
            {
                return BadRequest(e);
            }
        }
    }
}
