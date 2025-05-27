namespace CustomCADs.Shared.UseCases.Customizations.Queries;

public record GetCustomizationWeightByIdQuery(
	CustomizationId Id
) : IQuery<double>;
