namespace CustomCADs.Speedy.Http.Dtos.Country;

/// <param name="Name">
///     Nomenclature type name (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="NameEn">
///     Nomenclature type name (English).
///     <br />
///     Required: Yes.
/// </param>
internal record AddressNomenclatureTypeDto(
	string Name,
	string NameEn
);
