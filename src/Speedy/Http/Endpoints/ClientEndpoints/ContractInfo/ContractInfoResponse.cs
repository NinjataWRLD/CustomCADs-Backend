namespace CustomCADs.Speedy.Http.Endpoints.ClientEndpoints.ContractInfo;

using Dtos.CodAdditionalServiceContractInfo;
using Dtos.SpecialDeliveryRequirements;

internal record ContractInfoResponse(
	long Id,
	bool AdministrativeFeeAllowed,
	SpecialDeliveryRequirementsDto? SpecialDeliveryRequirements,
	CodAdditionalServiceContractInfoDto? Cod,
	ErrorDto? Error
);
