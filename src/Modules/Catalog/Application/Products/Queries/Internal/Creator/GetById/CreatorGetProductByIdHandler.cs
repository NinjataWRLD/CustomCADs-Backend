using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetById;

public sealed class CreatorGetProductByIdHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<CreatorGetProductByIdQuery, CreatorGetProductByIdDto>
{
    public async Task<CreatorGetProductByIdDto> Handle(CreatorGetProductByIdQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Product>.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw CustomAuthorizationException<Product>.ById(req.Id);
        }

        GetUsernameByIdQuery usernameQuery = new(product.CreatorId);
        string username = await sender.SendQueryAsync(usernameQuery, ct).ConfigureAwait(false);

        GetCategoryNameByIdQuery categoryQuery = new(product.CategoryId);
        string categoryName = await sender.SendQueryAsync(categoryQuery, ct).ConfigureAwait(false);

        GetTimeZoneByIdQuery timeZoneQuery = new(product.CreatorId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        return product.ToCreatorGetByIdDto(username, categoryName, timeZone);
    }
}
