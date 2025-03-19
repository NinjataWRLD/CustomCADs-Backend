using CustomCADs.Catalog.Application.Products.SharedCommandHandlers;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.UseCases.Products.Commands;

namespace CustomCADs.UnitTests.Catalog.Application.Products.SharedCommands;

using static ProductsData;

public class AddProductPurchaseHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IProductReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly ProductId[] ids = [ValidId, ValidId, ValidId];
    private readonly ProductQuery query;
    private readonly Result<Product> result;
    private readonly Product[] products = [
        CreateProduct(ValidName1, ValidDescription1, ValidPrice1),
        CreateProduct(ValidName2, ValidDescription2, ValidPrice2)
    ];

    public AddProductPurchaseHandlerUnitTests()
    {
        this.query = new(
            Ids: ids,
            Pagination: new(Limit: ids.Length)
        );
        this.result = new(
            Count: products.Length,
            Items: products
        );
        reads.Setup(x => x.AllAsync(query, true, ct))
            .ReturnsAsync(result);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        AddProductPurchaseCommand command = new(ids);
        AddProductPurchaseHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.AllAsync(query, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase()
    {
        // Arrange
        AddProductPurchaseCommand command = new(ids);
        AddProductPurchaseHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPopulateProperly()
    {
        // Arrange
        AddProductPurchaseCommand command = new(ids);
        AddProductPurchaseHandler handler = new(reads.Object, uow.Object);

        // Act
        int[] oldPurchases = [.. products.Select(x => x.Counts.Purchases)];
        await handler.Handle(command, ct);
        int[] newPurchases = [.. products.Select(x => x.Counts.Purchases)];

        // Assert
        bool[] results = [.. newPurchases.Select((x, i) => x - 1 == oldPurchases[i])];
        Assert.True(results.All(t => t));
    }
}
