namespace CustomCADs.Shared.UseCases.Customizations.Queries;

public record GetCustomizationExistsByIdQuery(
	CustomizationId Id
) : IQuery<bool>;
