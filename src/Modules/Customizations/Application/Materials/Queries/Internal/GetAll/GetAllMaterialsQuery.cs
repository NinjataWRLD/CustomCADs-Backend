namespace CustomCADs.Customizations.Application.Materials.Queries.Internal.GetAll;

public record GetAllMaterialsQuery
    : IQuery<ICollection<MaterialDto>>;
