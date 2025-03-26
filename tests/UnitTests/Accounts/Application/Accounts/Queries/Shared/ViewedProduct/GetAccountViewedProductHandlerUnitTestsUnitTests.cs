using CustomCADs.Accounts.Application.Accounts.Queries.Shared.ViewedProduct;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.ViewedProduct;

using static AccountsData;

public class GetAccountViewedProductHandlerUnitTestsUnitTests : AccountsBaseUnitTests
{
    private readonly Mock<IAccountReads> reads = new();
    private static readonly AccountId id = ValidId1;
    private static readonly ProductId productId = ProductId.New();
    private readonly Account account = CreateAccount();

    public GetAccountViewedProductHandlerUnitTestsUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(account);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetAccountViewedProductQuery query = new(id, productId);
        GetAccountViewedProductHandler handler = new(reads.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Handle_ShouldReturnProperly(bool expected)
    {
        // Arrange
        if (expected) account.AddViewedProduct(productId);

        GetAccountViewedProductQuery query = new(id, productId);
        GetAccountViewedProductHandler handler = new(reads.Object);

        // Act
        bool actual = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(expected, actual);
    }
}
