using CustomCADs.Shared.Speedy.API.Dtos.ShipmentSenderAndRecipient.ShipmentSender;
using CustomCADs.Shared.Speedy.Services;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Sender;

public static class Mapper
{
    public static ShipmentSenderDto ToDto(this ShipmentSenderModel model)
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

    public static ShipmentSenderModel ToModel(this ShipmentSenderDto dto)
        => new(
            Phone1: dto.Phone1.ToModel(),
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
