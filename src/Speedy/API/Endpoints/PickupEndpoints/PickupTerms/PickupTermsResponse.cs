namespace CustomCADs.Speedy.API.Endpoints.PickupEndpoints.PickupTerms;

public record PickupTermsResponse(
	string[] Cutoffs,
	ErrorDto? Error
);
