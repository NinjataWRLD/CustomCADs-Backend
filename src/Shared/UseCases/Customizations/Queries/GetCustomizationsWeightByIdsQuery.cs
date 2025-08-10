namespace CustomCADs.Shared.UseCases.Customizations.Queries;

public record GetCustomizationsWeightByIdsQuery(
	CustomizationId[] Ids
) : IQuery<Dictionary<CustomizationId, double>>;
