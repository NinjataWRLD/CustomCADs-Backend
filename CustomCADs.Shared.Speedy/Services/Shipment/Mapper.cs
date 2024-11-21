using CustomCADs.Shared.Speedy.API.Dtos.ParcelToPrint;
using CustomCADs.Shared.Speedy.API.Dtos.ShipmentParcels;
using CustomCADs.Shared.Speedy.API.Dtos.ShipmentPrice;
using CustomCADs.Shared.Speedy.Services.Shipment.Models;
using System.Xml;

namespace CustomCADs.Shared.Speedy.Services.Shipment;

using static Constants;

public static class Mapper
{
    public static ParcelHandoverDto ToDto(this ParcelHandoverRefModel model)
        => new(
            Id: model.Parcel.Id,
            ExternalCarrierParcelNumber: model.Parcel.ExternalCarrierParcelNumber,
            FullBarcode: model.Parcel.FullBarcode,
            DateTime: model.DateTime.ToString(DateTimeFormat)
        );
    
    public static CreatedShipmentParcelModel ToModel(this CreatedShipmentParcelDto dto)
        => new(
            SeqNo: dto.SeqNo,
            Id: dto.Id,
            ExternalCarrierId: dto.ExternalCarrierId,
            ExternalCarrierParcelNumber: dto.ExternalCarrierParcelNumber
        );

    public static ShipmentPriceModel ToModel(this ShipmentPriceDto dto)
        => new(
            Amount: dto.Amount,
            Vat: dto.Vat,
            Total: dto.Total,
            Currency: dto.Currency,
            Details: dto.Details.ToDictionary(kv => kv.Key, kv => kv.Value.ToModel()),
            AmountLocal: dto.AmountLocal,
            VatLocal: dto.VatLocal,
            TotalLocal: dto.TotalLocal,
            CurrencyLocal: dto.CurrencyLocal,
            DetailsLocal: dto.DetailsLocal.ToDictionary(kv => kv.Key, kv => kv.Value.ToModel()),
            CurrencyExchangeRateUnit: dto.CurrencyExchangeRateUnit,
            CurrencyExchangeRate: dto.CurrencyExchangeRate,
            ReturnAmounts: dto.ReturnAmounts?.ToModel()
        );

    public static ShipmentPriceAmountModel ToModel(this ShipmentPriceAmountDto dto)
        => new(
            Amount: dto.Amount,
            VatPercent: dto.VatPercent,
            Percent: dto.Percent
        );

    public static ReturnAmountsModel ToModel(this ReturnAmountsDto dto)
        => new(
            MoneyTransfer: dto.MoneyTransfer?.ToModel()
        );

    public static MoneyTransferPremiumModel ToModel(this MoneyTransferPremiumDto dto)
        => new(
            Amount: dto.Amount,
            AmountLocal: dto.AmountLocal,
            Payer: dto.Payer
        );

    public static LabelInfoModel ToModel(this LabelInfoDto dto)
        => new(
            ParcelId: dto.ParcelId,
            FullBarcode: dto.FullBarcode,
            ExportPriority: dto.ExportPriority,
            HubId: dto.HubId,
            OfficeId: dto.OfficeId,
            OfficeName: dto.OfficeName,
            DeadlineDay: dto.DeadlineDay,
            DeadlineMonth: dto.DeadlineMonth,
            TourId: dto.TourId
        );
}
