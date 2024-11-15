namespace CustomCADs.Shared.Speedy.Dtos.CodAdditionalServiceContractInfo;

public record CodAdditionalServiceContractInfoDto(
    bool MoneyTransferAllowed,
    bool CodFiscalReceiptAllowed,
    bool HasCODAnnex,
    CodInternationalAnnexContractInfoDto[] InternationalCODAnnexes
);
