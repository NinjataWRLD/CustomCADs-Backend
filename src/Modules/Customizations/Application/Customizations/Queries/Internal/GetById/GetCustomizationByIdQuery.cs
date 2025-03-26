using CustomCADs.Customizations.Application.Customizations.Dtos;

namespace CustomCADs.Customizations.Application.Customizations.Queries.Internal.GetById;

public record GetCustomizationByIdQuery(
    CustomizationId Id
) : IQuery<CustomizationDto>;
