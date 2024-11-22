using CustomCADs.Shared.Speedy.API.Endpoints.ShipmentEndpoints;
using CustomCADs.Shared.Speedy.Models;
using CustomCADs.Shared.Speedy.Models.Shipment;
using CustomCADs.Shared.Speedy.Models.Shipment.Content;
using CustomCADs.Shared.Speedy.Models.Shipment.Parcel;
using CustomCADs.Shared.Speedy.Models.Shipment.Payment;
using CustomCADs.Shared.Speedy.Models.Shipment.Primary;
using CustomCADs.Shared.Speedy.Models.Shipment.Recipient;
using CustomCADs.Shared.Speedy.Models.Shipment.Secondary;
using CustomCADs.Shared.Speedy.Models.Shipment.Sender;
using CustomCADs.Shared.Speedy.Models.Shipment.Service;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Cod;
using CustomCADs.Shared.Speedy.Services.Shipment.Models;

namespace CustomCADs.Shared.Speedy.Services.Shipment;

public class ShipmentService(IShipmentEndpoints endpoints)
{
    public async Task<WrittenShipmentModel> CreateShipmentAsync(WriteShipmentModel model, AccountModel account, CancellationToken ct = default)
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

    public async Task CancelShipmentAsync(string shipmentId, string comment, AccountModel account, CancellationToken ct = default)
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

    public async Task<CreatedShipmentParcelModel> AddParcelAsync(string shipmentId, AddParcelModel model, AccountModel account, CancellationToken ct = default)
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

    public async Task<WrittenShipmentModel> FinalizePendingShipmentAsync(string shipmentId, AccountModel account, CancellationToken ct = default)
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

    public async Task<ShipmentModel[]> ShipmentInfoAsync(string[] shipmentIds, AccountModel account, CancellationToken ct = default)
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

    public async Task<SecondaryShipmentModel[]> SecondaryShipmentAsync(string shipmentId, ShipmentType[] types, AccountModel account, CancellationToken ct = default)
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

    public async Task<WrittenShipmentModel> UpdateShipmentAsync(string shipmentId, WriteShipmentModel model, AccountModel account, CancellationToken ct = default)
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

    public async Task<string[]> FindParcelsByRefAsync(FindParcelModel model, AccountModel account, CancellationToken ct = default)
    {
        var response = await endpoints.FindParcelsByRefAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Ref: model.Ref,
            SearchInRef: model.SearchInRef,
            ShipmentsOnly: model.ShipmentsOnly,
            IncludeReturns: model.IncludeReturns,
            FromDateTime: model.FromDateTime,
            ToDateTime: model.ToDateTime
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return response.Barcodes;
    }

    public async Task HandoverToCourierAsync(ParcelHandoverRefModel[] parcels, AccountModel account, CancellationToken ct = default)
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

    public async Task HandoverToMidwayCarrierAsync(ParcelHandoverRefModel[] parcels, AccountModel account, CancellationToken ct = default)
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

    public async Task<BarcodeInformationModel> BarcodeInformationAsync(ShipmentParcelRefModel parcel, AccountModel account, CancellationToken ct = default)
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
