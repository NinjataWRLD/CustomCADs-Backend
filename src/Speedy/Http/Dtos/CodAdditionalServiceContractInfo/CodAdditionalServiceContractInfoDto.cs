namespace CustomCADs.Speedy.Http.Dtos.CodAdditionalServiceContractInfo;

internal record CodAdditionalServiceContractInfoDto(
	bool MoneyTransferAllowed,
	bool CodFiscalReceiptAllowed,
	bool HasCODAnnex,
	CodInternationalAnnexContractInfoDto[] InternationalCODAnnexes
);
