namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

/// <param name="RecipientEmails">
///     List of recipient emails for the electronic return receipt.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ThirdPartyPayer">
///     Flag indicating if a third party is the payer.
///     <br />
///     Required: No.
/// </param>
internal record ShipmentElectronicReturnReceiptAdditionalServiceDto(
	string[] RecipientEmails,
	bool? ThirdPartyPayer
);
