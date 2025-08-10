using CustomCADs.Speedy.API.Dtos.ShipmentSenderAndRecipient.ShipmentSender;

namespace CustomCADs.Speedy.Services.Models.Shipment.Sender;

internal static class Mapper
{
	internal static ShipmentSenderDto ToDto(this ShipmentSenderModel model)
		=> new(
			Phone1: model.Phone1.ToDto(),
			Phone2: model.Phone2?.ToDto(),
			Phone3: model.Phone3?.ToDto(),
			Address: model.Address?.ToDto(),
			ContactName: model.ContactName,
			Email: model.Email,
			ClientId: model.ClientId,
			ClientName: model.ClientName,
			PrivatePerson: model.PrivatePerson,
			DropoffOfficeId: model.DropoffOfficeId,
			DropoffGeoPUDOId: model.DropoffGeoPUDOId
		);

	internal static ShipmentSenderModel ToModel(this ShipmentSenderDto dto, string phone)
		=> new(
			Phone1: dto.Phone1 is null ? new(phone) : dto.Phone1.ToModel(),
			Phone2: dto.Phone2?.ToModel(),
			Phone3: dto.Phone3?.ToModel(),
			Address: dto.Address?.ToModel(),
			ContactName: dto.ContactName,
			Email: dto.Email,
			ClientId: dto.ClientId,
			ClientName: dto.ClientName,
			PrivatePerson: dto.PrivatePerson,
			DropoffOfficeId: dto.DropoffOfficeId,
			DropoffGeoPUDOId: dto.DropoffGeoPUDOId
		);
}
