namespace CustomCADs.Shared.Speedy.Services.Client.Models;

public record CodAdditionalServiceContractInfoModel(
    bool MoneyTransferAllowed,
    bool CodFiscalReceiptAllowed,
    bool HasCodAnnex,
    (int CountryId, string CountryName)[] InternationalCodAnnexes
);
