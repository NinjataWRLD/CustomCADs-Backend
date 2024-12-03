namespace CustomCADs.Catalog.Endpoints.Common.Dtos;

public sealed record CountsDto(
    int Purchases,
    int Likes,
    int Views
);
