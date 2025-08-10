namespace CustomCADs.Speedy.Services.Location.Complex;

public record ComplexModel(
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
