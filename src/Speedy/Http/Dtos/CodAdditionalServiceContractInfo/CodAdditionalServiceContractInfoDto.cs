namespace CustomCADs.Speedy.Http.Dtos.CodAdditionalServiceContractInfo;

/// <param name="MoneyTransferAllowed">
///     Flag indicating if money transfer is allowed for COD.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="CodFiscalReceiptAllowed">
///     Flag indicating if COD fiscal receipt is allowed.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="HasCODAnnex">
///     Flag indicating if COD annex is present.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="InternationalCODAnnexes">
///     List of international COD annex contract infos.
///     <br />
///     Required: Yes. See CodInternationalAnnexContractInfoDto for details.
/// </param>
internal record CodAdditionalServiceContractInfoDto(
	bool MoneyTransferAllowed,
	bool CodFiscalReceiptAllowed,
	bool HasCODAnnex,
	CodInternationalAnnexContractInfoDto[] InternationalCODAnnexes
);
