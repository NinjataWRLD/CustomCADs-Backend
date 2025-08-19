namespace CustomCADs.Speedy.Http.Dtos.Complex;

/// <param name="Id">
///     Complex id.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="SiteId">
///     Site id for the complex.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Type">
///     Complex type (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="TypeEn">
///     Complex type (English).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Name">
///     Complex name (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="NameEn">
///     Complex name (English).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ActualId">
///     Actual complex id.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ActualType">
///     Actual complex type (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ActualTypeEn">
///     Actual complex type (English).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ActualName">
///     Actual complex name (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ActualNameEn">
///     Actual complex name (English).
///     <br />
///     Required: Yes.
/// </param>
internal record ComplexDto(
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
