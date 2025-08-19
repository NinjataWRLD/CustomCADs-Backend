namespace CustomCADs.Speedy.Http.Dtos.CodAdditionalServiceContractInfo;

/// <param name="CountryId">
///     Country id for the international COD annex.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="CountryName">
///     Country name for the international COD annex.
///     <br />
///     Required: Yes.
/// </param>
internal record CodInternationalAnnexContractInfoDto(
	int CountryId,
	string CountryName
);
