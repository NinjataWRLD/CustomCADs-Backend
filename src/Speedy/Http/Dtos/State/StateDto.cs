namespace CustomCADs.Speedy.Http.Dtos.State;

/// <param name="Id">
///     State id.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Name">
///     State name (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="NameEn">
///     State name (English).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="StateAlpha">
///     State alpha code.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="CountryId">
///     Country id for the state.
///     <br />
///     Required: Yes.
/// </param>
internal record StateDto(
	string Id,
	string Name,
	string NameEn,
	string StateAlpha,
	int CountryId
);
