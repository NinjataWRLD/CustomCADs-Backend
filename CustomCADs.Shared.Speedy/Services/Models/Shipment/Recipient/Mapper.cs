using CustomCADs.Shared.Speedy.API.Dtos.ShipmentSenderAndRecipient.AutoSelectNearestOfficePolicy;
using CustomCADs.Shared.Speedy.API.Dtos.ShipmentSenderAndRecipient.ShipmentRecipient;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Recipient;

public static class Mapper
{
    public static ShipmentRecipientDto ToDto(this ShipmentRecipientModel model)
    => new(
        PrivatePerson: model.PrivatePerson,
        ContactName: model.ContactName,
        Email: model.Email,
        ClientId: model.ClientId,
        ClientName: model.ClientName,
        ObjectName: model.ObjectName,
        PickupOfficeId: model.PickupOfficeId,
        PickupGeoPUDOIf: model.PickupGeoPUDOIf,
        AutoSelectNearestOffice: model.AutoSelectNearestOffice,
        AutoSelectNearestOfficePolicy: model.AutoSelectNearestOfficePolicy?.ToDto(),
        Address: model.Address?.ToDto(),
        Phone1: model.Phone1?.ToDto(),
        Phone2: model.Phone2?.ToDto(),
        Phone3: model.Phone3?.ToDto()
    );

    public static AutoSelectNearestOfficePolicyDto ToDto(this AutoSelectNearestOfficePolicyModel model)
        => new(
            UnavailableNearestOfficeAction: model.UnavailableNearestOfficeAction,
            OfficeType: model.OfficeType
        );

    public static ShipmentRecipientModel ToModel(this ShipmentRecipientDto dto)
    => new(
        PrivatePerson: dto.PrivatePerson,
        ContactName: dto.ContactName,
        Email: dto.Email,
        ClientId: dto.ClientId,
        ClientName: dto.ClientName,
        ObjectName: dto.ObjectName,
        PickupOfficeId: dto.PickupOfficeId,
        PickupGeoPUDOIf: dto.PickupGeoPUDOIf,
        AutoSelectNearestOffice: dto.AutoSelectNearestOffice,
        AutoSelectNearestOfficePolicy: dto.AutoSelectNearestOfficePolicy?.ToModel(),
        Address: dto.Address?.ToModel(),
        Phone1: dto.Phone1?.ToModel(),
        Phone2: dto.Phone2?.ToModel(),
        Phone3: dto.Phone3?.ToModel()
    );

    public static AutoSelectNearestOfficePolicyModel ToModel(this AutoSelectNearestOfficePolicyDto dto)
        => new(
            UnavailableNearestOfficeAction: dto.UnavailableNearestOfficeAction,
            OfficeType: dto.OfficeType
        );
}
