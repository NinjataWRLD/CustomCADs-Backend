using CustomCADs.Catalog.Application.Products.Queries.CreatorGetById;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.CreatorGetById;

using static ProductsData;

public class CreatorGetProductByIdHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IProductReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly Product product = CreateProduct();
    private const string TimeZone = "Europe/Sofia";

    public CreatorGetProductByIdHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
            .ReturnsAsync(product);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetTimeZoneByIdQuery>(), ct))
            .ReturnsAsync(TimeZone);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatbase()
    {
        // Arrange
        CreatorGetProductByIdQuery query = new(ValidId, ValidCreatorId);
        CreatorGetProductByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        CreatorGetProductByIdQuery query = new(ValidId, ValidCreatorId);
        CreatorGetProductByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(It.IsAny<GetUsernameByIdQuery>(), ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(It.IsAny<GetCategoryNameByIdQuery>(), ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        CreatorGetProductByIdQuery query = new(ValidId, ValidCreatorId);
        CreatorGetProductByIdHandler handler = new(reads.Object, sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(product.Id, result.Id),
            () => Assert.Equal(product.Name, result.Name),
            () => Assert.Equal(product.Description, result.Description),
            () => Assert.Equal(product.Price, result.Price),
            () => Assert.Equal(product.CadId, result.CadId),
            () => Assert.Equal(product.CategoryId, result.Category.Id)
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
            .ReturnsAsync(CreateProduct(creatorId: AccountId.New()));

        CreatorGetProductByIdQuery query = new(ValidId, ValidCreatorId);
        CreatorGetProductByIdHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<ProductAuthorizationException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenProductNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
            .ReturnsAsync(null as Product);

        CreatorGetProductByIdQuery query = new(ValidId, ValidCreatorId);
        CreatorGetProductByIdHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<ProductNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
