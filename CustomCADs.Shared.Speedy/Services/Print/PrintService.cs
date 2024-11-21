using CustomCADs.Shared.Speedy.API.Endpoints.PrintEndpoints;
using CustomCADs.Shared.Speedy.Models;
using CustomCADs.Shared.Speedy.Models.Shipment.Parcel;
using CustomCADs.Shared.Speedy.Services.Print.Models;
using CustomCADs.Shared.Speedy.Services.Shipment;
using CustomCADs.Shared.Speedy.Services.Shipment.Models;

namespace CustomCADs.Shared.Speedy.Services.Print;

public class PrintService(IPrintEndpoints print)
{
    public async Task<byte[]> PrintAsync(PrintModel model, AccountModel account, CancellationToken ct = default)
    {
        var response = await print.PrintAsync(new(
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
    
    public async Task<(byte[] Data, LabelInfoModel[] PrintLabelsInfo)> ExtendedPrintAsync(PrintModel model, AccountModel account, CancellationToken ct = default)
    {
        var response = await print.ExtendedPrintAsync(new(
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
    
    public async Task<LabelInfoModel[]> LabelInfoAsync(ShipmentParcelRefModel[] parcels, AccountModel account, CancellationToken ct = default)
    {
        var response = await print.LabelInfoAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Parcels: [.. parcels.Select(p => p.ToDto())]
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.PrintLabelsInfo.Select(i => i.ToModel())];
    }

    public async Task<byte[]> PrintVoucherAsync(PrintVoucherModel model, AccountModel account, CancellationToken ct = default)
    {
        var response = await print.PrintVoucherAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            PrinterName: model.PrinterName,
            Format: model.Format, 
            Dpi: model.Dpi,
            ShipmentIds: model.ShipmentIds
        ), ct).ConfigureAwait(false);

        using MemoryStream stream = new();
        await response.Content.CopyToAsync(stream, ct).ConfigureAwait(false);
        return stream.ToArray();
    }
}
