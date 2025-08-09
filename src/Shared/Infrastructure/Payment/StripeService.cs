using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Payment.Exceptions;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;
using Stripe;

namespace CustomCADs.Shared.Infrastructure.Payment;

using static Messages;

public sealed class StripeService(PaymentIntentService service) : IPaymentService
{

	public async Task<PaymentDto> InitializeCartPayment(string paymentMethodId, AccountId buyerId, PurchasedCartId cartId, decimal price, string description, CancellationToken ct = default)
	{
		PaymentIntent intent = await service.CreateAsync(
			options: new()
			{
				Amount = Convert.ToInt64(price * 100),
				Currency = "EUR",
				PaymentMethod = paymentMethodId,
				Confirm = true,
				Description = description,
				AutomaticPaymentMethods = new()
				{
					Enabled = true,
					AllowRedirects = "never"
				},
				Metadata = new()
				{
					["buyerId"] = buyerId.ToString(),
					["rewardType"] = "cart",
					["rewardId"] = cartId.ToString(),
				},
			},
			cancellationToken: ct
		).ConfigureAwait(false);

		PaymentDto response = new(
			ClientSecret: intent.ClientSecret,
			Message: GetMessageFromStatus(intent.Status)
		);

		switch (response.Message)
		{
			case FailedPaymentCapture:
				intent = await service.CaptureAsync(intent.Id, cancellationToken: ct).ConfigureAwait(false);
				string message = GetMessageFromStatus(intent.Status);

				if (message == SuccessfulPayment)
				{
					return response with { Message = SuccessfulPayment };
				}
				throw PaymentFailedException.WithClientSecret(intent.ClientSecret, message);

			case ProcessingPayment:
				return response;

			case SuccessfulPayment:
				return response;

			default:
				throw PaymentFailedException.General(response.Message);
		}
	}

	public async Task<PaymentDto> InitializeCustomPayment(string paymentMethodId, AccountId buyerId, CustomId customId, decimal price, string description, CancellationToken ct = default)
	{
		PaymentIntent intent = await service.CreateAsync(
			options: new()
			{
				Amount = Convert.ToInt64(price * 100),
				Currency = "EUR",
				PaymentMethod = paymentMethodId,
				Confirm = true,
				Description = description,
				AutomaticPaymentMethods = new()
				{
					Enabled = true,
					AllowRedirects = "never"
				},
				Metadata = new()
				{
					["buyerId"] = buyerId.ToString(),
					["rewardType"] = "custom",
					["rewardId"] = customId.ToString(),
				},
			},
			cancellationToken: ct
		).ConfigureAwait(false);

		PaymentDto response = new(
			ClientSecret: intent.ClientSecret,
			Message: GetMessageFromStatus(intent.Status)
		);

		switch (response.Message)
		{
			case FailedPaymentCapture:
				intent = await service.CaptureAsync(intent.Id, cancellationToken: ct).ConfigureAwait(false);
				string message = GetMessageFromStatus(intent.Status);

				if (message == SuccessfulPayment)
				{
					return response with { Message = SuccessfulPayment };
				}
				throw PaymentFailedException.WithClientSecret(intent.ClientSecret, message);

			case ProcessingPayment:
			case SuccessfulPayment:
				return response;

			default:
				throw PaymentFailedException.General(response.Message);
		}
	}

	private static string GetMessageFromStatus(string status)
		=> status switch
		{
			"succeeded" => SuccessfulPayment,
			"processing" => ProcessingPayment,
			"canceled" => CanceledPayment,
			"requires_payment_method" => FailedPaymentMethod,
			"requires_action" => FailedPayment,
			"requires_capture" => FailedPaymentCapture,
			_ => string.Format(UnhandledPayment, status)
		};
}
