namespace CustomCADs.Shared.Speedy.Services.PickupService.PickupTerms;

public record PickupTermsResponse(
    string[] Cutoffs,
    ErrorDto? Error
);
