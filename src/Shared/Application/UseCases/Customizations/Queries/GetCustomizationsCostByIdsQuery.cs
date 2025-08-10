namespace CustomCADs.Shared.Application.UseCases.Customizations.Queries;

public record GetCustomizationsCostByIdsQuery(
	CustomizationId[] Ids
) : IQuery<Dictionary<CustomizationId, decimal>>;
