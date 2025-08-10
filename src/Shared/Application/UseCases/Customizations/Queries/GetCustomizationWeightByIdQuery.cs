namespace CustomCADs.Shared.Application.UseCases.Customizations.Queries;

public record GetCustomizationWeightByIdQuery(
	CustomizationId Id
) : IQuery<double>;
