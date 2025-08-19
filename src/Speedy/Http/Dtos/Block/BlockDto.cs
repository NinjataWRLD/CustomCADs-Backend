namespace CustomCADs.Speedy.Http.Dtos.Block;

/// <param name="SiteId">
///     Site id for the block.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Name">
///     Block name (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="NameEn">
///     Block name (English).
///     <br />
///     Required: Yes.
/// </param>
internal record BlockDto(
	long SiteId,
	string Name,
	string NameEn
);
