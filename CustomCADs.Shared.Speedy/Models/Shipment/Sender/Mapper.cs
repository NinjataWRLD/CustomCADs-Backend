using CustomCADs.Shared.Speedy.API.Dtos.ShipmentSenderAndRecipient.ShipmentSender;

namespace CustomCADs.Shared.Speedy.Models.Shipment.Sender;

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
}
