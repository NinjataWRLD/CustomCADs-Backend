﻿using CustomCADs.Shared.Speedy.API.Dtos.CalculationContent;
using CustomCADs.Shared.Speedy.API.Dtos.CalculationRecipient;
using CustomCADs.Shared.Speedy.API.Dtos.CalculationResult;
using CustomCADs.Shared.Speedy.API.Dtos.CalculationSender;
using CustomCADs.Shared.Speedy.API.Dtos.CalculationService;
using CustomCADs.Shared.Speedy.Services.Models.Calculation;
using CustomCADs.Shared.Speedy.Services.Models.Calculation.Recipient;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Content;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Payment;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Recipient;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Sender;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices;
using CustomCADs.Shared.Speedy.Services.Services.Models;
using CustomCADs.Shared.Speedy.Services.Shipment;
using CustomCADs.Shared.Speedy.Services.Shipment.Models;

namespace CustomCADs.Shared.Speedy.Services.Calculation;

using static Constants;

internal static class Mapper
{
    internal static CalculationContentDto ToCalculation(this ShipmentContentModel model)
        => new(
            ParcelsCount: model.ParcelsCount,
            TotalWeight: model.TotalWeight,
            Documents: model.Documents,
            Palletized: model.Palletized,
            Parcels: [.. model.Parcels?.Select(p => p.ToDto())]
        );
    
    internal static CalculationServiceDto ToCalculation(this ShipmentServiceModel model)
        => new(
            ServiceIds: [model.ServiceId],
            PickupDate: model.PickupDate?.ToString(DateTimeFormat),
            AutoAdjustPickupDate: model.AutoAdjustPickupDate,
            AdditionalServices: model.AdditionalServices?.ToDto(),
            DeferredDays: model.DeferredDays,
            SaturdayDelivery: model.SaturdayDelivery
        );
    
    internal static CalculationSenderDto ToCalculation(this ShipmentSenderModel model, CalculationAddressLocationModel? location = null)
        => new(
            AddressLocation: location?.ToDto(),
            ClientId: model.ClientId,
            PrivatePerson: model.PrivatePerson,
            DropoffOfficeId: model.DropoffOfficeId,
            DropoffGeoPUDOId: model.DropoffGeoPUDOId
        );
    
    internal static CalculationRecipientDto ToCalculation(this ShipmentRecipientModel model, CalculationAddressLocationModel? location = null)
        => new(
            AddressLocation: location?.ToDto(),
            ClientId: model.ClientId,
            PrivatePerson: model.PrivatePerson,
            PickupOfficeId: model.PickupOfficeId,
            PickupGeoPUDOId: model.PickupGeoPUDOId
        );
    
    internal static (string Service, ShipmentAdditionalServicesModel? AdditionalServices, ShipmentPriceModel Price, DateOnly PickupDate, DateTime DeliveryDeadline) ToModel(this CalculationResultDto dto, CourierServiceModel[] services)
        => (
            Service: services.Single(s => s.Id == dto.ServiceId).NameEn,
            AdditionalServices: dto.AdditionalServices?.ToModel(),
            Price: dto.Price.ToModel(),
            PickupDate: DateOnly.Parse(dto.PickupDate),
            DeliveryDeadline: DateTime.Parse(dto.DeliveryDeadline)
        );
}
