using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Carts;
using CustomCADs.Shared.ApplicationEvents.Customs;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;
using CustomCADs.Shared.Infrastructure.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace CustomCADs.Presentation;

[Route("api/[controller]")]
[ApiController]
public class StripeController(IEventRaiser raiser, IOptions<PaymentSettings> options) : ControllerBase
{
	[HttpPost("webhook")]
	public async Task<IActionResult> Webhook()
	{
		Event stripeEvent;
		try
		{
			stripeEvent = EventUtility.ConstructEvent(
				json: await new StreamReader(HttpContext.Request.Body).ReadToEndAsync().ConfigureAwait(false),
				stripeSignatureHeader: Request.Headers["Stripe-Signature"],
				secret: options.Value.WebhookSecret
			);
		}
		catch (StripeException e)
		{
			return BadRequest($"Signature verification failed: {e.Message}");
		}

		if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
		{
			if (stripeEvent.Data.Object is not PaymentIntent intent)
			{
				return BadRequest("Invalid PaymentIntent object.");
			}

			AccountId buyerId = AccountId.New(intent.Metadata["buyerId"]);
			if (buyerId.IsEmpty())
			{
				return BadRequest("Invalid BuyerId");
			}

			string rewardType = intent.Metadata["rewardType"];
			switch (rewardType)
			{
				case "cart":
					{
						PurchasedCartId? rewardId = PurchasedCartId.New(intent.Metadata["rewardId"]);
						if (rewardId is null)
						{
							return BadRequest("Missing RewardId");
						}

						await raiser.RaiseApplicationEventAsync(
								@event: new CartPaymentCompletedApplicationEvent(
									Id: rewardId.Value,
									BuyerId: buyerId
								)
							).ConfigureAwait(false);
						break;
					}

				case "custom":
					{
						CustomId? rewardId = CustomId.New(intent.Metadata["rewardId"]);
						if (rewardId is null)
						{
							return BadRequest("Missing RewardId");
						}

						await raiser.RaiseApplicationEventAsync(
								@event: new CustomPaymentCompletedApplicationEvent(
									Id: rewardId.Value,
									BuyerId: buyerId
								)
							).ConfigureAwait(false);
						break;
					}
			}
		}
		else { /* Log unexpected type: stripeEvent.Type */ }

		return Ok();
	}
}
