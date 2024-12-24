using CustomCADs.Shared.Speedy.API.Dtos.ParcelToPrint;
using CustomCADs.Shared.Speedy.API.Endpoints.PrintEndpoints;
using CustomCADs.Shared.Speedy.API.Endpoints.PrintEndpoints.Enums;
using CustomCADs.Shared.Speedy.Services.Models;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Content;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Parcel;
using CustomCADs.Shared.Speedy.Services.Shipment;
using CustomCADs.Shared.Speedy.Services.Shipment.Models;

namespace CustomCADs.Shared.Speedy.Services.Print;

public class PrintService(
    IPrintEndpoints endpoints,
    ShipmentService shipmentService
)
{
    public async Task<byte[]> PrintAsync(
        AccountModel account,
        string shipmentId,
        PaperSize paperSize = PaperSize.A4,
        PaperFormat format = PaperFormat.pdf,
        Dpi dpi = Dpi.dpi203,
        AdditionalWaybillSenderCopy additionalWaybillSenderCopy = AdditionalWaybillSenderCopy.NONE,
        string? printerName = null,
        CancellationToken ct = default)
    {
        var shipments = await shipmentService.ShipmentInfoAsync(
            account: account,
            shipmentIds: [shipmentId],
            ct: ct
        ).ConfigureAwait(false);
        var parcels = shipments.Single().Content.Parcels;

        var response = await endpoints.PrintAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            PrinterName: printerName,
            Format: format,
            PaperSize: paperSize,
            Dpi: dpi,
            AdditionalWaybillSenderCopy: additionalWaybillSenderCopy,
            Parcels: [.. parcels?.Select(p => new ParcelToPrintDto(new(p.Id, null, null), null))]
        ), ct).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();
        using MemoryStream stream = new();
        await response.Content.CopyToAsync(stream, ct).ConfigureAwait(false);
        return stream.ToArray();
    }

    public async Task<(byte[] Data, LabelInfoModel[] PrintLabelsInfo)> ExtendedPrintAsync(
        AccountModel account,
        string shipmentId,
        PaperSize paperSize,
        PaperFormat format = PaperFormat.pdf,
        Dpi dpi = Dpi.dpi203,
        AdditionalWaybillSenderCopy additionalWaybillSenderCopy = AdditionalWaybillSenderCopy.NONE,
        string? printerName = null,
        CancellationToken ct = default)
    {
        var shipments = await shipmentService.ShipmentInfoAsync(
            account: account,
            shipmentIds: [shipmentId],
            ct: ct
        ).ConfigureAwait(false);
        var parcels = shipments.Single().Content.Parcels;

        var response = await endpoints.ExtendedPrintAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            PrinterName: printerName,
            Format: format,
            PaperSize: paperSize,
            Dpi: dpi,
            AdditionalWaybillSenderCopy: additionalWaybillSenderCopy,
            Parcels: [.. parcels?.Select(p => new ParcelToPrintDto(new(p.Id, null, null), null))]
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return (
            response.Data,
            [.. response.PrintLabelsInfo.Select(i => i.ToModel())]
        );
    }

    public async Task<LabelInfoModel[]> LabelInfoAsync(
        AccountModel account,
        ShipmentParcelRefModel[] parcels,
        CancellationToken ct = default)
    {
        var response = await endpoints.LabelInfoAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Parcels: [.. parcels.Select(p => p.ToDto())]
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.PrintLabelsInfo.Select(i => i.ToModel())];
    }

    public async Task<byte[]> PrintVoucherAsync(
        AccountModel account,
        string[] shipmentIds,
        string? printerName = null,
        PaperFormat format = PaperFormat.pdf,
        Dpi dpi = Dpi.dpi203,
        CancellationToken ct = default)
    {
        var response = await endpoints.PrintVoucherAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            PrinterName: printerName,
            Format: format,
            Dpi: dpi,
            ShipmentIds: shipmentIds
        ), ct).ConfigureAwait(false);

        using MemoryStream stream = new();
        await response.Content.CopyToAsync(stream, ct).ConfigureAwait(false);
        return stream.ToArray();
    }
}
