
namespace CustomCADs.Speedy.Http.Dtos.ShipmentSenderAndRecipient.AutoSelectNearestOfficePolicy;

/// <param name="UnavailableNearestOfficeAction">
///     Action to take if nearest office is unavailable.
///     <br />
///     Required: Yes. Enum value.
/// </param>
/// <param name="OfficeType">
///     Type of office to auto-select.
///     <br />
///     Required: Yes. Enum value.
/// </param>
internal record AutoSelectNearestOfficePolicyDto(
	UnavailableNearestOfficeAction UnavailableNearestOfficeAction,
	OfficeType OfficeType
);
