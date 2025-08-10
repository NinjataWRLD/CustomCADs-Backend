namespace CustomCADs.Shared.Application.UseCases.Customizations.Queries;

public record GetCustomizationsWeightByIdsQuery(
	CustomizationId[] Ids
) : IQuery<Dictionary<CustomizationId, double>>;
