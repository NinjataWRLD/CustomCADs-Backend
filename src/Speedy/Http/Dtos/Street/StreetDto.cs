namespace CustomCADs.Speedy.Http.Dtos.Street;

/// <param name="Id">
///     Street id.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="SiteId">
///     Site id for the street.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Type">
///     Street type (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="TypeEn">
///     Street type (English).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Name">
///     Street name (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="NameEn">
///     Street name (English).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ActualId">
///     Actual street id.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ActualType">
///     Actual street type (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ActualTypeEn">
///     Actual street type (English).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ActualName">
///     Actual street name (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ActualNameEn">
///     Actual street name (English).
///     <br />
///     Required: Yes.
/// </param>
internal record StreetDto(
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
