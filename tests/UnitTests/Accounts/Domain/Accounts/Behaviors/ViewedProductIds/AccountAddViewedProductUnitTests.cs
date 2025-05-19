using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.ViewedProductIds;

public class AccountAddViewedProductUnitTests : AccountsBaseUnitTests
{
	private readonly Account account = CreateAccount();
	public readonly ProductId productId = ProductId.New();

	[Fact]
	public void AddViewedProduct_ShouldNotThrowException()
	{
		account.AddViewedProduct(productId);
	}

	[Fact]
	public void AddViewedProduct_AddsViewedProduct_WhenNotYetAdded()
	{
		account.AddViewedProduct(productId);

		Assert.Contains(account.ViewedProductIds, x => x == productId);
	}

	[Fact]
	public void AddViewedProduct_DoesNotAddViewedProduct_WhenAlreadyAdded()
	{
		ProductId[] productIds = [productId, productId, ProductId.New(), productId];

		foreach (ProductId productId in productIds)
		{
			account.AddViewedProduct(productId);
		}

		int expectedCount = productIds.Distinct().Count();
		int actualCount = account.ViewedProductIds.Count;
		Assert.Equal(expectedCount, actualCount);
	}
}
