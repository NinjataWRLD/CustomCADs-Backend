using CustomCADs.Catalog.Application.Products.Events.Application.ProductPurchased;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Shared.Application.Events.Catalog;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Events.Application;

public class UserPurchasedProductHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly UserPurchasedProductHandler handler;
	private readonly Mock<IUnitOfWork> uow = new();

	private static readonly ProductId[] ids = [];

	public UserPurchasedProductHandlerUnitTests()
	{
		handler = new(uow.Object);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		UserPurchasedProductApplicationEvent ae = new(ids);

		// Act
		await handler.Handle(ae, ct);

		// Assert
		uow.Verify(x => x.AddProductPurchasesAsync(ids, 1, ct), Times.Once());
	}
}
