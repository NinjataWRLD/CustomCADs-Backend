namespace CustomCADs.Orders.Application.Orders.Queries.GetImageUrl;

public record GetOrderImagePresignedUrlDto(
    string ImageKey,
    string ImageUrl
);