using CustomCADs.Shared.Speedy.API.Dtos.ShipmentSenderAndRecipient.ShipmentAddress;
using CustomCADs.Shared.Speedy.API.Dtos.ShipmentSenderAndRecipient.ShipmentPhoneNumber;
using CustomCADs.Shared.Speedy.Models;
using CustomCADs.Shared.Speedy.Services.Models;

namespace CustomCADs.Shared.Speedy;

public static class Mapper
{
    public static ShipmentPhoneNumberDto ToDto(this PhoneNumberModel model)
        => new(model.Number, model.Extension);
    
    public static PhoneNumberModel ToModel(this ShipmentPhoneNumberDto dto)
        => new(dto.Number, dto.Extension);

    public static ShipmentAddressDto ToDto(this ShipmentAddressModel model)
        => new(
            CountryId: model.CountryId,
            PostCode: model.PostCode,
            SiteId: model.SiteId,
            SiteType: model.SiteType,
            SiteName: model.SiteName,
            ComplexId: model.ComplexId,
            ComplexType: model.ComplexType,
            ComplexName: model.ComplexName,
            StreetId: model.StreetId,
            StreetType: model.StreetType,
            StreetName: model.StreetName,
            StreetNo: model.StreetNo,
            BlockNo: model.BlockNo,
            EntranceNo: model.EntranceNo,
            FloorNo: model.FloorNo,
            ApartmentNo: model.ApartmentNo,
            PoiId: model.PoiId,
            AddressNote: model.AddressNote,
            X: model.X,
            Y: model.Y
        );
    
    public static ShipmentAddressModel ToModel(this ShipmentAddressDto dto)
        => new(
            CountryId: dto.CountryId,
            PostCode: dto.PostCode,
            SiteId: dto.SiteId,
            SiteType: dto.SiteType,
            SiteName: dto.SiteName,
            ComplexId: dto.ComplexId,
            ComplexType: dto.ComplexType,
            ComplexName: dto.ComplexName,
            StreetId: dto.StreetId,
            StreetType: dto.StreetType,
            StreetName: dto.StreetName,
            StreetNo: dto.StreetNo,
            BlockNo: dto.BlockNo,
            EntranceNo: dto.EntranceNo,
            FloorNo: dto.FloorNo,
            ApartmentNo: dto.ApartmentNo,
            PoiId: dto.PoiId,
            AddressNote: dto.AddressNote,
            X: dto.X,
            Y: dto.Y
        );

    public static AddressModel ToModel(this AddressDto dto)
        => new(
            FullAddressString: dto.FullAddressString,
            SiteAddressString: dto.SiteAddressString,
            LocalAddressString: dto.LocalAddressString,
            ShipmentAddressModel: new(
                CountryId: dto.CountryId,
                PostCode: dto.PostCode,
                SiteId: dto.SiteId,
                SiteType: dto.SiteType,
                SiteName: dto.SiteName,
                ComplexId: dto.ComplexId,
                ComplexType: dto.ComplexType,
                ComplexName: dto.ComplexName,
                StreetId: dto.StreetId,
                StreetType: dto.StreetType,
                StreetName: dto.StreetName,
                StreetNo: dto.StreetNo,
                BlockNo: dto.BlockNo,
                EntranceNo: dto.EntranceNo,
                FloorNo: dto.FloorNo,
                ApartmentNo: dto.ApartmentNo,
                PoiId: dto.PoiId,
                AddressNote: dto.AddressNote,
                X: dto.X,
                Y: dto.Y
            )
        );
}
