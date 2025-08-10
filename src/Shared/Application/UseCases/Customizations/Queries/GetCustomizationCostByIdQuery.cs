namespace CustomCADs.Shared.Application.UseCases.Customizations.Queries;

public record GetCustomizationCostByIdQuery(
	CustomizationId Id
) : IQuery<decimal>;
