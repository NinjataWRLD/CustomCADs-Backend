namespace CustomCADs.Customizations.Application.Materials.Queries.GetById;

public record GetMaterialByIdQuery(
    MaterialId Id
) : IQuery<MaterialDto>;
