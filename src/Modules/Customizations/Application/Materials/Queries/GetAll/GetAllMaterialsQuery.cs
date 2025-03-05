namespace CustomCADs.Customizations.Application.Materials.Queries.GetAll;

public record GetAllMaterialsQuery 
    : IQuery<ICollection<MaterialDto>>;
