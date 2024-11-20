namespace CustomCADs.Orders.Application.Orders.Queries.GetImageUrlPut;

public record GetOrderImagePresignedUrlPutQuery(
    string ImageKey,
    string ContentType,
    string FileName
) : IQuery<GetOrderImagePresignedUrlPutDto>;