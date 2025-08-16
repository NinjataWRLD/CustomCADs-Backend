using CustomCADs.Speedy.Core.Services.Models;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Parcel;
using CustomCADs.Speedy.Core.Services.Shipment.Models;

namespace CustomCADs.Speedy.Core.Contracts.Print;

internal interface IPrintService
{
	Task<(byte[] Data, LabelInfoModel[] PrintLabelsInfo)> ExtendedPrintAsync(AccountModel account, string shipmentId, PaperSize paperSize, PaperFormat format = PaperFormat.pdf, Dpi dpi = Dpi.dpi203, AdditionalWaybillSenderCopy additionalWaybillSenderCopy = AdditionalWaybillSenderCopy.NONE, string? printerName = null, CancellationToken ct = default);
	Task<LabelInfoModel[]> LabelInfoAsync(AccountModel account, ShipmentParcelRefModel[] parcels, CancellationToken ct = default);
	Task<byte[]> PrintAsync(AccountModel account, string shipmentId, PaperSize paperSize = PaperSize.A4, PaperFormat format = PaperFormat.pdf, Dpi dpi = Dpi.dpi203, AdditionalWaybillSenderCopy additionalWaybillSenderCopy = AdditionalWaybillSenderCopy.NONE, string? printerName = null, CancellationToken ct = default);
	Task<byte[]> PrintVoucherAsync(AccountModel account, string[] shipmentIds, string? printerName = null, PaperFormat format = PaperFormat.pdf, Dpi dpi = Dpi.dpi203, CancellationToken ct = default);
}
