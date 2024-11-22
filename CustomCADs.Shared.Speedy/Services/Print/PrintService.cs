using CustomCADs.Shared.Speedy.API.Endpoints.PrintEndpoints;
using CustomCADs.Shared.Speedy.API.Endpoints.PrintEndpoints.Enums;
using CustomCADs.Shared.Speedy.Services.Models;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Parcel;
using CustomCADs.Shared.Speedy.Services.Print.Models;
using CustomCADs.Shared.Speedy.Services.Shipment;
using CustomCADs.Shared.Speedy.Services.Shipment.Models;

namespace CustomCADs.Shared.Speedy.Services.Print;

public class PrintService(IPrintEndpoints endpoints)
{
    public async Task<byte[]> PrintAsync(
        AccountModel account,
        PrintModel model,
        CancellationToken ct = default)
    {
        var response = await endpoints.PrintAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            PrinterName: model.PrinterName,
            Format: model.Format,
            PaperSize: model.PaperSize,
            Dpi: model.Dpi,
            AdditionalWaybillSenderCopy: model.AdditionalWaybillSenderCopy,
            Parcels: [.. model.Parcels.Select(p => p.ToDto())]
        ), ct).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();
        using MemoryStream stream = new();
        await response.Content.CopyToAsync(stream, ct).ConfigureAwait(false);
        return stream.ToArray();
    }

    public async Task<(byte[] Data, LabelInfoModel[] PrintLabelsInfo)> ExtendedPrintAsync(
        AccountModel account,
        PrintModel model,
        CancellationToken ct = default)
    {
        var response = await endpoints.ExtendedPrintAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            PrinterName: model.PrinterName,
            Format: model.Format,
            PaperSize: model.PaperSize,
            Dpi: model.Dpi,
            AdditionalWaybillSenderCopy: model.AdditionalWaybillSenderCopy,
            Parcels: [.. model.Parcels.Select(p => p.ToDto())]
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
