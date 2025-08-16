namespace CustomCADs.Speedy.Http.Endpoints.PickupEndpoints.PickupTerms;

internal record PickupTermsResponse(
	string[] Cutoffs,
	ErrorDto? Error
);
