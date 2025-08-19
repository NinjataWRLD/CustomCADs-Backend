using CustomCADs.Speedy.Core.Models.Client;

namespace CustomCADs.Speedy.Core.Contracts.Client;

public record ContactInfoModel(
	long Id,
	bool AdministrativeFeeAllowed,
	SpecialDeliveryRequirementsModel? SpecialDeliveryRequirements,
	CodAdditionalServiceContractInfoModel? Cod
);
