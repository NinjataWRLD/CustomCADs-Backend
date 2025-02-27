namespace CustomCADs.Shared.Speedy.API.Endpoints.ClientEndpoints.ContractInfo;

using Dtos.CodAdditionalServiceContractInfo;
using Dtos.SpecialDeliveryRequirements;

public record ContractInfoResponse(
    long Id,
    bool AdministrativeFeeAllowed,
    SpecialDeliveryRequirementsDto? SpecialDeliveryRequirements,
    CodAdditionalServiceContractInfoDto? Cod,
    ErrorDto? Error
);