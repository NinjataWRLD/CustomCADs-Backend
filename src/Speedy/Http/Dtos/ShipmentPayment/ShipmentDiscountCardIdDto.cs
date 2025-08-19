namespace CustomCADs.Speedy.Http.Dtos.ShipmentPayment;

/// <param name="ContractId">
///     Fixed discount card contract id.
///     <br />
///     Required: Yes. Validated against the accessible contract.
/// </param>
/// <param name="CardId">
///     Fixed discount card id.
///     <br />
///     Required: Yes. Validated against the fixed discount card register.
/// </param>
internal record ShipmentDiscountCardIdDto(
	long ContractId,
	long CardId
);
