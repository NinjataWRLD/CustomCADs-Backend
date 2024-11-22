using CustomCADs.Shared.Speedy.API.Endpoints.ShipmentEndpoints;
using CustomCADs.Shared.Speedy.Services.Models;
using CustomCADs.Shared.Speedy.Services.Models.Shipment;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Content;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Parcel;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Payment;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Primary;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Recipient;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Secondary;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Sender;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices.Cod;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Parcel;
using CustomCADs.Shared.Speedy.Services.Shipment.Models;

namespace CustomCADs.Shared.Speedy.Services.Shipment;

using static Constants;

public class ShipmentService(IShipmentEndpoints endpoints)
{
    public async Task<WrittenShipmentModel> CreateShipmentAsync(
        AccountModel account,
        WriteShipmentModel model,
        CancellationToken ct = default)
    {
        var response = await endpoints.CreateShipmentAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Recipient: model.Recipient.ToDto(),
            Service: model.Service.ToDto(),
            Content: model.Content.ToDto(),
            Payment: model.Payment.ToDto(),
            Sender: model.Sender?.ToDto(),
            Id: model.Id,
            ShipmentNote: model.ShipmentNote,
            Ref1: model.Ref1,
            Ref2: model.Ref2,
            ConsolidationRef: model.ConsolidationRef,
            RequireUnsuccessfulDeliveryStickerImage: model.RequireUnsuccessfulDeliveryStickerImage
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return new(
            Id: response.Id,
            Parcels: [.. response.Parcels.Select(p => p.ToModel())],
            Price: response.Price.ToModel(),
            PickupDate: DateOnly.Parse(response.PickupDate),
            DeliveryDeadline: DateTime.Parse(response.DeliveryDeadline)
        );
    }

    public async Task CancelShipmentAsync(
        AccountModel account,
        string shipmentId,
        string comment,
        CancellationToken ct = default)
    {
        var response = await endpoints.CancelShipmentAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            ShipmentId: shipmentId,
            Comment: comment
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
    }

    public async Task<CreatedShipmentParcelModel> AddParcelAsync(
        AccountModel account,
        string shipmentId,
        AddParcelModel model,
        CancellationToken ct = default)
    {
        var response = await endpoints.AddParcelShipmentAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            ShipmentId: shipmentId,
            CodAmount: model.CodAmount,
            DeclaredValueAmount: model.DeclaredValueAmount,
            Parcel: model.Parcel.ToDto(),
            CodFiscalReceiptItems: [.. model.CodFiscalReceiptItems.Select(i => i.ToDto())]
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return response.Parcel.ToModel();
    }

    public async Task<WrittenShipmentModel> FinalizePendingShipmentAsync(
        AccountModel account,
        string shipmentId,
        CancellationToken ct = default)
    {
        var response = await endpoints.FinalizePendingShipmentAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            ShipmentId: shipmentId
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return new(
            Id: response.Id,
            Parcels: [.. response.Parcels.Select(p => p.ToModel())],
            Price: response.Price.ToModel(),
            PickupDate: DateOnly.Parse(response.PickupDate),
            DeliveryDeadline: DateTime.Parse(response.DeliveryDeadline)
        );
    }

    public async Task<ShipmentModel[]> ShipmentInfoAsync(
        AccountModel account,
        string[] shipmentIds,
        CancellationToken ct = default)
    {
        var response = await endpoints.ShipmentInfoAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            ShipmentIds: shipmentIds
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Shipments.Select(d => d.ToModel())];
    }

    public async Task<SecondaryShipmentModel[]> SecondaryShipmentAsync(
        AccountModel account,
        string shipmentId,
        ShipmentType[] types,
        CancellationToken ct = default)
    {
        var response = await endpoints.SecondaryShipmentAsync(shipmentId, new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Types: types
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Shipments.Select(d => d.ToModel())];
    }

    public async Task<WrittenShipmentModel> UpdateShipmentAsync(
        AccountModel account,
        string shipmentId,
        WriteShipmentModel model,
        CancellationToken ct = default)
    {
        var response = await endpoints.UpdateShipmentAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Id: shipmentId,
            Recipient: model.Recipient.ToDto(),
            Service: model.Service.ToDto(),
            Content: model.Content.ToDto(),
            Payment: model.Payment.ToDto(),
            Sender: model.Sender?.ToDto(),
            ShipmentNote: model.ShipmentNote,
            Ref1: model.Ref1,
            Ref2: model.Ref2,
            ConsolidationRef: model.ConsolidationRef,
            RequireUnsuccessfulDeliveryStickerImage: model.RequireUnsuccessfulDeliveryStickerImage
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return new(
            Id: response.Id,
            Parcels: [.. response.Parcels.Select(p => p.ToModel())],
            Price: response.Price.ToModel(),
            PickupDate: DateOnly.Parse(response.PickupDate),
            DeliveryDeadline: DateTime.Parse(response.DeliveryDeadline)
        );
    }

    public async Task<string[]> FindParcelsByRefAsync(
        AccountModel account,
        string @ref,
        int searchInRef,
        bool? shipmentsOnly = null,
        bool? includeReturns = null,
        DateTime? fromDateTime = null,
        DateTime? toDateTime = null,
        CancellationToken ct = default)
    {
        var response = await endpoints.FindParcelsByRefAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Ref: @ref,
            SearchInRef: searchInRef,
            ShipmentsOnly: shipmentsOnly,
            IncludeReturns: includeReturns,
            FromDateTime: fromDateTime?.ToString(DateTimeFormat),
            ToDateTime: toDateTime?.ToString(DateTimeFormat)
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return response.Barcodes;
    }

    public async Task HandoverToCourierAsync(
        AccountModel account, 
        ParcelHandoverRefModel[] parcels, 
        CancellationToken ct = default)
    {
        var response = await endpoints.HandoverToCourierAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Parcels: [.. parcels.Select(p => p.ToDto())]
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
    }

    public async Task HandoverToMidwayCarrierAsync(
        AccountModel account, 
        ParcelHandoverRefModel[] parcels, 
        CancellationToken ct = default)
    {
        var response = await endpoints.HandoverToMidwayCarrierAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Parcels: [.. parcels.Select(p => p.ToDto())]
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
    }

    public async Task<BarcodeInformationModel> BarcodeInformationAsync(
        AccountModel account, 
        ShipmentParcelRefModel parcel, 
        CancellationToken ct = default)
    {
        var response = await endpoints.BarcodeInformationAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Parcel: parcel.ToDto()
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return new(
            LabelInfo: response.LabelInfo.ToModel(),
            PrimaryShipment: response.PrimaryShipment.ToModel(),
            PrimaryParcelId: response.PrimaryParcelId,
            ReturnShipmentId: response.ReturnShipmentId,
            ReturnParcelId: response.ReturnParcelId,
            RedirectShipmentId: response.RedirectShipmentId,
            RedirectParcelId: response.RedirectParcelId,
            InitialShipmentId: response.InitialShipmentId,
            InitialParcelId: response.InitialParcelId
        );
    }
}
