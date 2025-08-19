namespace CustomCADs.Speedy.Http.Dtos.Country;

/// <param name="Id">
///     Country id.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Name">
///     Country name (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="NameEn">
///     Country name (English).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="IsoAlpha2">
///     ISO Alpha-2 country code.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="IsoAlpha3">
///     ISO Alpha-3 country code.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="PostCodeFormats">
///     Supported postcode formats for the country.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="RequireState">
///     Flag indicating if country requires state.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="AddressType">
///     Address type for the country.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="CurrencyCode">
///     Currency code for the country.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="DefaultOfficeId">
///     Default office id for the country.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="StreetTypes">
///     Supported street types for the country.
///     <br />
///     Required: Yes. See AddressNomenclatureTypeDto for details.
/// </param>
/// <param name="ComplexTypes">
///     Supported complex types for the country.
///     <br />
///     Required: Yes. See AddressNomenclatureTypeDto for details.
/// </param>
/// <param name="SiteNomen">
///     Site nomenclature type for the country.
///     <br />
///     Required: Yes.
/// </param>
internal record CountryDto(
	int Id,
	string Name,
	string NameEn,
	string IsoAlpha2,
	string IsoAlpha3,
	string[] PostCodeFormats,
	bool RequireState,
	int AddressType,
	string CurrencyCode,
	int DefaultOfficeId,
	AddressNomenclatureTypeDto[] StreetTypes,
	AddressNomenclatureTypeDto[] ComplexTypes,
	int SiteNomen
);
