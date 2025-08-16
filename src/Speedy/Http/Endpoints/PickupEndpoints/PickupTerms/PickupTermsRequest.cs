namespace CustomCADs.Speedy.Http.Endpoints.PickupEndpoints.PickupTerms;

using Dtos.CalculationSender;

internal record PickupTermsRequest(
	string UserName,
	string Password,
	int ServiceId,
	string? Language,
	long? ClientSystemId,
	string? StartingDate,
	CalculationSenderDto? Sender,
	bool? SenderHasPayment
);
