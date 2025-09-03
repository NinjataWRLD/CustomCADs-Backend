using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Carts;
using CustomCADs.Shared.Application.Events.Customs;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Carts;
using CustomCADs.Shared.Domain.TypedIds.Customs;
using CustomCADs.Shared.Endpoints.Attributes;
using CustomCADs.Shared.Infrastructure.Payment;
using Microsoft.Extensions.Options;
using Stripe;

namespace CustomCADs.Presentation;

using static Shared.Endpoints.EndpointsConstants;

public static class StripeWebhook
{
	public static void MapStripeWebhook(this IEndpointRouteBuilder app)
	{
		app.MapPost($"api/{Paths.Stripe}/webhook", async (HttpContext context, IEventRaiser raiser, IOptions<PaymentSettings> options) =>
		{
			Event stripeEvent;
			try
			{
				stripeEvent = EventUtility.ConstructEvent(
					json: await new StreamReader(context.Request.Body).ReadToEndAsync().ConfigureAwait(false),
					stripeSignatureHeader: context.Request.Headers["Stripe-Signature"],
					secret: options.Value.WebhookSecret
				);
			}
			catch (StripeException e)
			{
				return Results.BadRequest($"Signature verification failed: {e.Message}");
			}

			if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
			{
				if (stripeEvent.Data.Object is not PaymentIntent intent)
				{
					return Results.BadRequest("Invalid PaymentIntent object.");
				}

				AccountId buyerId = AccountId.New(intent.Metadata["buyerId"]);
				if (buyerId.IsEmpty())
				{
					return Results.BadRequest("Invalid BuyerId");
				}

				string rewardType = intent.Metadata["rewardType"];
				switch (rewardType)
				{
					case "cart":
						{
							PurchasedCartId? rewardId = PurchasedCartId.New(intent.Metadata["rewardId"]);
							if (rewardId is null)
							{
								return Results.BadRequest("Missing RewardId");
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
								return Results.BadRequest("Missing RewardId");
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

			return Results.Ok();
		})
		.WithTags(Tags[Paths.Stripe])
		.WithSummary("Stripe Webhook")
		.WithDescription("Not meant for the client to use")
		.WithMetadata(new SkipIdempotencyAttribute());
	}
}
