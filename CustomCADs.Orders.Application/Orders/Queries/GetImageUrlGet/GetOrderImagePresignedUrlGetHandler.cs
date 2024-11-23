using CustomCADs.Orders.Application.Orders.Queries.GetById;
using CustomCADs.Orders.Domain.Common.Exceptions.Orders;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Storage;

namespace CustomCADs.Orders.Application.Orders.Queries.GetImageUrlGet;

public class GetOrderImagePresignedUrlGetHandler(IOrderReads reads, IStorageService storage)
    : IQueryHandler<GetOrderImagePresignedUrlGetQuery, GetOrderImagePresignedUrlGetDto>
{
    public async Task<GetOrderImagePresignedUrlGetDto> Handle(GetOrderImagePresignedUrlGetQuery req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.Id);

        string imageUrl = await storage.GetPresignedGetUrlAsync(
            key: order.Image.Key,
            contentType: order.Image.ContentType
        ).ConfigureAwait(false);

        GetOrderImagePresignedUrlGetDto response = new(imageUrl);
        return response;
    }
}
