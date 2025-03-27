using CustomCADs.Catalog.Application.Products.Queries.Internal.Shared.GetAll;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Shared.GetAll;

using static ProductsData;

public class GetAllProductsHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IProductReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly Product[] products = [];
    private readonly ProductQuery query;
    private readonly Result<Product> result;

    public GetAllProductsHandlerUnitTests()
    {
        query = new(
            Pagination: new(1, products.Length)
        );
        result = new(
            Count: products.Length,
            Items: products
        );

        reads.Setup(x => x.AllAsync(query, false, ct))
            .ReturnsAsync(result);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetUsernamesByIdsQuery>(), ct))
            .ReturnsAsync(products.ToDictionary(x => x.CreatorId, x => "Username123"));

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCategoryNamesByIdsQuery>(), ct))
            .ReturnsAsync(products.ToDictionary(x => x.CategoryId, x => "Cateogry123"));

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetTimeZonesByIdsQuery>(), ct))
            .ReturnsAsync(products.ToDictionary(x => x.CreatorId, x => "TimeZone123"));
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetAllProductsQuery query = new(
            Pagination: this.query.Pagination,
            CategoryId: this.query.CategoryId,
            CreatorId: this.query.CreatorId,
            Status: this.query.Status,
            Name: this.query.Name,
            Sorting: this.query.Sorting
        );
        GetAllProductsHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.AllAsync(this.query, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        GetAllProductsQuery query = new(
            Pagination: this.query.Pagination,
            CategoryId: this.query.CategoryId,
            CreatorId: this.query.CreatorId,
            Status: this.query.Status,
            Name: this.query.Name,
            Sorting: this.query.Sorting
        );
        GetAllProductsHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(It.IsAny<GetUsernamesByIdsQuery>(), ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(It.IsAny<GetCategoryNamesByIdsQuery>(), ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(It.IsAny<GetTimeZonesByIdsQuery>(), ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperCount()
    {
        // Arrange
        GetAllProductsQuery query = new(
            Pagination: this.query.Pagination,
            CategoryId: this.query.CategoryId,
            CreatorId: this.query.CreatorId,
            Status: this.query.Status,
            Name: this.query.Name,
            Sorting: this.query.Sorting
        );
        GetAllProductsHandler handler = new(reads.Object, sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(result.Count, products.Length);
    }
}
