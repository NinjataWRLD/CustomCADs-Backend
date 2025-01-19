namespace CustomCADs.Shared.Speedy.API.Dtos.CodAdditionalServiceContractInfo;

public record CodAdditionalServiceContractInfoDto(
    bool MoneyTransferAllowed,
    bool CodFiscalReceiptAllowed,
    bool HasCODAnnex,
    CodInternationalAnnexContractInfoDto[] InternationalCODAnnexes
);
