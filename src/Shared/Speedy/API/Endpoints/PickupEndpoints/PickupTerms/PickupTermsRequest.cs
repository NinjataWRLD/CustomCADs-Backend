namespace CustomCADs.Shared.Speedy.API.Endpoints.PickupEndpoints.PickupTerms;

using Dtos.CalculationSender;

public record PickupTermsRequest(
	string UserName,
	string Password,
	int ServiceId,
	string? Language,
	long? ClientSystemId,
	string? StartingDate,
	CalculationSenderDto? Sender,
	bool? SenderHasPayment
);
