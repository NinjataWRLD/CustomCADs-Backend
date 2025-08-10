namespace CustomCADs.Speedy.API.Endpoints.ShipmentEndpoints.AddParcel;

using Dtos.ShipmentContent.ShipmentParcel;
using Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentCodAdditionalService;

public record AddParcelRequest(
	string UserName,
	string Password,
	string ShipmentId,
	ShipmentParcelDto Parcel,
	string? Language,
	long? ClientSystemId,
	double? CodAmount,
	ShipmentCodFiscalReceiptItemDto[] CodFiscalReceiptItems,
	double DeclaredValueAmount
);
