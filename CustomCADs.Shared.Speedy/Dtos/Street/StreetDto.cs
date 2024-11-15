namespace CustomCADs.Shared.Speedy.Dtos.Street;

public record StreetDto(
    long Id,
    long SiteId,
    string Type,
    string TypeEn,
    string Name,
    string NameEn,
    long ActualId,
    string ActualType,
    string ActualTypeEn,
    string ActualName,
    string ActualNameEn
);
