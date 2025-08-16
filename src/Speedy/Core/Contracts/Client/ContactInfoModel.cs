using CustomCADs.Speedy.Core.Services.Client.Models;

namespace CustomCADs.Speedy.Core.Contracts.Client;

public record ContactInfoModel(
    long Id,
    bool AdministrativeFeeAllowed,
    SpecialDeliveryRequirementsModel? SpecialDeliveryRequirements,
    CodAdditionalServiceContractInfoModel? Cod
);
