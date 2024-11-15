﻿using Refit;

namespace CustomCADs.Shared.Speedy.Services.PrintService;

using ExtendedPrint;
using LabelInfo;
using Print;
using PrintVoucher;

public interface IPrintService
{
    [Post("")]
    Task<HttpResponseMessage> PrintAsync(PrintRequest request, CancellationToken ct = default);

    [Post("extended")]
    Task<ExtendedPrintResponse> ExtendedPrintAsync(ExtendedPrintRequest request, CancellationToken ct = default);

    [Post("labelInfo")]
    Task<LabelInfoResponse> LabelInfoAsync(LabelInfoRequest request, CancellationToken ct = default);

    [Post("voucher")]
    Task<HttpResponseMessage> PrintVoucherAsync(PrintVoucherRequest request, CancellationToken ct = default);
}
