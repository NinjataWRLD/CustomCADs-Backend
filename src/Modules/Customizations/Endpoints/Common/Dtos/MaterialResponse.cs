namespace CustomCADs.Customizations.Endpoints.Common.Dtos;

public sealed record MaterialResponse(
    int Id,
    string Name,
    decimal Density,
    decimal Cost
);
