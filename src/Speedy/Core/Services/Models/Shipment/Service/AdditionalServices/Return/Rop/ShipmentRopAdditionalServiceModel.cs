namespace CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return.Rop;

public record ShipmentRopAdditionalServiceModel(
	(int ServiceId, int ParcelsCount)[] Pallets,
	bool? ThirdPartyPayer
);
