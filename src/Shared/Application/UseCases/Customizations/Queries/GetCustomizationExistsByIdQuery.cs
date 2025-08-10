namespace CustomCADs.Shared.Application.UseCases.Customizations.Queries;

public record GetCustomizationExistsByIdQuery(
	CustomizationId Id
) : IQuery<bool>;
