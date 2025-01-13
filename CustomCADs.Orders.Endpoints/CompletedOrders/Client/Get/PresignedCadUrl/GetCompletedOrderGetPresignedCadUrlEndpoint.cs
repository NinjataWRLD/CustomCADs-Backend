﻿using CustomCADs.Orders.Application.CompletedOrders.Queries.GetCadUrlGet;

namespace CustomCADs.Orders.Endpoints.CompletedOrders.Client.Get.PresignedCadUrl;

public sealed class GetCompletedOrderGetPresignedCadUrlEndpoint(IRequestSender sender)
    : Endpoint<GetCompletedOrderGetPresignedCadUrlRequest, GetCompletedOrderGetPresignedCadUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/download/cad");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("12. Download Cad")
            .WithDescription("Download the Cad for your Completed Order by specifying its Id")
        );
    }

    public override async Task HandleAsync(GetCompletedOrderGetPresignedCadUrlRequest req, CancellationToken ct)
    {
        GetCompletedOrderCadPresignedUrlGetQuery query = new(
            Id: CompletedOrderId.New(req.Id),
            BuyerId: User.GetAccountId()
        );
        var cadDto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetCompletedOrderGetPresignedCadUrlResponse response = new(cadDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
