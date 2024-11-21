namespace CustomCADs.Shared.Speedy.Services.Client.Models;

public record ContractModel(
    long Id,
    bool AdministrativeFeeAllowed,
    SpecialDeliveryRequirementsModel? SpecialDeliveryRequirements,
    CodAdditionalServiceContractInfoModel? Cod
);
