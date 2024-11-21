using CustomCADs.Shared.Speedy.API.Dtos.ShipmentSenderAndRecipient.ShipmentRecipient;

namespace CustomCADs.Shared.Speedy.Models.Shipment.Recipient;

public static class Mapper
{
    public static ShipmentRecipientDto ToDto(this ShipmentRecipientModel model)
    => new(
        Phone1: model.Phone1.ToDto(),
        ClientName: model.ClientName,
        PrivatePerson: model.PrivatePerson,
        Address: model.Address.ToDto(),
        ContactName: model.ContactName,
        Email: model.Email
    );
}
